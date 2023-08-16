using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Text;
using System.IO;
using System;
using UnityEngine.UI;
using System.Linq;

public class MicrophoneManager : MonoBehaviour
{
    //���� ���� ����� ��ġ
    public string filePath;

    //Naver CSR ClientID
    public string clientID;

    //Naver CSR Client Secret
    public string clientSecret;

    //������ �����
    private AudioSource audio;

    //Naver CSR�� �޾ƿ��� ���ڿ�
    private string text;

    //text�� ������ �ܾ�
    public string matchText;

    //���ϴ� ������ �ð�
    private float point;

    //��� ���� �����
    private float sensitivity = 100;

    //��� ����
    public float loudness = 0;
    private float time;

    //���ϰ� �ִ��� Ȯ��
    public bool isSpeak;

    void Awake()
    {
        audio = GetComponent<AudioSource>() == null ? gameObject.AddComponent<AudioSource>() : GetComponent<AudioSource>();
        text = "";
        matchText = "";
    }

    void OnEnable()
    {
        //���� ����
        StartRecord();
        StartCoroutine(Speak());
    }

    private void Update()
    {
        time += Time.deltaTime;
    }

    //Speak
    private IEnumerator Speak()
    {
        loudness = GetAveragedVolume() * sensitivity;

        //��� ������ 0.1���� ũ�� ���ϰ� ����, 0.001���� ������ �� ���ϴ� ����
        if (loudness > 0.05f && !isSpeak)
        {
            point = time < 1f ? 0 : time - 1;
            isSpeak = true;
        }

        if (0 < loudness && loudness < 0.001f && isSpeak)
        {
            int lastTime = Microphone.GetPosition(null);

            if (lastTime != 0)
            {
                Microphone.End(null);

                float[] samples = new float[audio.clip.samples];
                audio.clip.GetData(samples, 0);

                float[] cutSamples = new float[lastTime];
                Array.Copy(samples, cutSamples, cutSamples.Length);

                audio.clip = AudioClip.Create("TTS_Test", cutSamples.Length + 25000, 1, 44100, false);
                
                if (audio.clip.length > 0.5f)
                {
                    audio.clip.SetData(cutSamples, 0);
                    audio.clip = TrimAudioClip(audio.clip, point, audio.clip.length);
                    SavWav.Save(filePath, audio.clip);
                    GetText();
                    print(text);
                    if (text.Contains(":"))
                        text = text.Split(':')[1];

                    if (text.Contains("\""))
                        text = text.Replace("\"", "");

                    if (text.Contains("}"))
                        text = text.Replace("}", "");

                    Singleton.Instance.textCSR.GetComponent<Text>().text = "=> " + text;

                    if (text.Contains(matchText))
                    {
                        yield return new WaitForSeconds(1.5f);
                        Singleton.Instance.gameManager.waitCheck = true;
                    }
                    else
                        StartRecord();
                }
                else
                    StartRecord();

                isSpeak = false;
                Singleton.Instance.gameManager.isSpeak = false;
            }
        }

        yield return new WaitForEndOfFrame();
        StartCoroutine(Speak());
    }

    //���� ����
    private void StartRecord()
    {
        audio.clip = Microphone.Start(null, true, 100, 44100);
        audio.loop = true;
        audio.mute = false;
        audio.volume = 0.01f;

        while (!(Microphone.GetPosition(null) > 0))
        {

        }

        audio.Play();
        time = 0;
    }

    //Average Volume
    private float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;

        audio.GetOutputData(data, 0);

        foreach (float s in data)
            a += Mathf.Abs(s);

        return a / 256;
    }

    //Naver CSR
    private void GetText()
    {
        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        byte[] fileData = new byte[fs.Length];

        fs.Read(fileData, 0, fileData.Length);
        fs.Close();

        string lang = "Kor";    // ��� �ڵ� ( Kor, Jpn, Eng, Chn )
        string url = $"https://naveropenapi.apigw.ntruss.com/recog/v1/stt?lang={lang}";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

        request.Headers.Add("X-NCP-APIGW-API-KEY-ID", clientID);
        request.Headers.Add("X-NCP-APIGW-API-KEY", clientSecret);
        request.Method = "POST";
        request.ContentType = "application/octet-stream";
        request.ContentLength = fileData.Length;

        using (Stream requestStream = request.GetRequestStream())
        {
            requestStream.Write(fileData, 0, fileData.Length);
            requestStream.Close();
        }

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream stream = response.GetResponseStream();
        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
        text = reader.ReadToEnd();

        stream.Close();
        response.Close();
        reader.Close();
    }

    private AudioClip TrimAudioClip(AudioClip originalClip, float startPosSec, float lengthSec)
    {
        var originalClipSamples = new float[originalClip.samples];
        originalClip.GetData(originalClipSamples, 0);

        //converts startPosSec & takeAmountSec from seconds to sample amount
        int newStartPosSample = (int)(startPosSec * originalClip.frequency);
        int newLengthSecSample = (int)(lengthSec * originalClip.frequency);

        //gets the trimmed version of the orignalClipSamples
        var newClipSamples = originalClipSamples.Skip(newStartPosSample).Take(newLengthSecSample).ToArray();

        //generates a new empty clip and sets its data according to the newClipSamples
        AudioClip resClip = AudioClip.Create(originalClip.name, newClipSamples.Length, originalClip.channels, originalClip.frequency, false);
        resClip.SetData(newClipSamples, 0);

        return resClip;
    }
}
