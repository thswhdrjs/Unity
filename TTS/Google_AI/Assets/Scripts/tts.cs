using System.Collections;
using UnityEngine;

public class tts : MonoBehaviour
{
    public AudioSource audioSource;
    public string text;
    private bool check;

	// Start is called before the first frame update
	void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        if (text == "")
            return;

        if (!check)
        {
            check = true;
            StartCoroutine(DownloadTheAudio());
        }
    }

    IEnumerator DownloadTheAudio()
    {
        string url = "https://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q=" + text + "&tl=En-gb";
        WWW www = new WWW(url);
        yield return www;

        audioSource.clip = www.GetAudioClip(false, true, AudioType.MPEG);
        audioSource.Play();

        if (audioSource.time == audioSource.clip.length)
        {
            text = "";
            check = false;
        }
    }
}