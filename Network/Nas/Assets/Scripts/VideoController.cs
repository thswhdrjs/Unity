using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Security;
using System.Security.Permissions;
using System;
using System.IO;

public class VideoController : MonoBehaviour
{
    private RawImage Image;
    private VideoPlayer vidio;
    private AudioSource audio;
    private string url;

    void Awake()
    {
        Image = GetComponent<RawImage>();
        vidio = gameObject.AddComponent<VideoPlayer>();
        audio = gameObject.AddComponent<AudioSource>();

        vidio.playOnAwake = false;
        audio.playOnAwake = false;
        audio.Pause();

        url = "file://Y:/liv.mp4";
        StartCoroutine(playVideo());
    }

    IEnumerator playVideo(string url)
    {
        vidio.source = VideoSource.Url;
        vidio.url = url;
        vidio.audioOutputMode = VideoAudioOutputMode.AudioSource;

        vidio.EnableAudioTrack(0, true);
        vidio.SetTargetAudioSource(0, audio);
        vidio.Prepare();

        WaitForSeconds waitTime = new WaitForSeconds(1.0f);

        while (!vidio.isPrepared)
        {
            print("동영상 준비중...");
            yield return waitTime;
        }

        print("동영상이 준비가 끝났습니다.");

        Image.texture = vidio.texture;

        vidio.Play();
        audio.Play();

        print("동영상이 재생됩니다.");

        while (vidio.isPlaying)
        {
            print("동영상 재생 시간: " + Mathf.FloorToInt((float)vidio.time));
            yield return null;
        }

        print("돟영상이 끝났습니다.");
    }
}