using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum SOUND_TYPE
    {
        MAIN,
        BGM,
        FX,
        AMBIENT,
    }

    [Serializable]
    public class SoundVolume
    {
        public float MAIN = 1.0f;
        public float BGM = 1.0f;
        public float FX = 1.0f;
        public float AMBIENT = 1.0f;
    }

    [Header("Audio Volume")]
    public SoundVolume volume = new SoundVolume();

    [Header("Audio Sources")]
    [SerializeField] AudioSource AudioSource_Main;
    [SerializeField] AudioSource AudioSource_FX;
    [SerializeField] AudioSource AudioSource_Ambient;
    [SerializeField] AudioSource AudioSource_BGM;

    [Header("Audio Clips")]
    [SerializeField] List<AudioClip> Clips_Main;
    [SerializeField] List<AudioClip> Clips_FX;
    [SerializeField] List<AudioClip> Clips_Ambient;
    [SerializeField] List<AudioClip> Clips_BGM;

    public void PlaySound(SOUND_TYPE type, int index, float delay)
    {
        AudioSource audio = null;

        if (type == SOUND_TYPE.MAIN)
        {
            audio = AudioSource_Main;
            audio.clip = Clips_Main[index];
        }

        if (type == SOUND_TYPE.BGM)
        {
            audio = AudioSource_BGM;
            audio.clip = Clips_BGM[index];
        }

        if (type == SOUND_TYPE.FX)
        {
            audio = AudioSource_FX;
            audio.clip = Clips_FX[index];
        }

        if (type == SOUND_TYPE.AMBIENT)
        {
            audio = AudioSource_Ambient;
            audio.clip = Clips_Ambient[index];
        }

        if (audio == null)
            return;

        audio.PlayDelayed(delay);
    }

    public void PlaySound(SOUND_TYPE type, int index)
    {
        AudioSource audio = null;

        if (type == SOUND_TYPE.MAIN)
        {
            audio = AudioSource_Main;
            audio.clip = Clips_Main[index];
        }

        if (type == SOUND_TYPE.BGM)
        {
            audio = AudioSource_BGM;
            audio.clip = Clips_BGM[index];
        }

        if (type == SOUND_TYPE.FX)
        {
            audio = AudioSource_FX;
            audio.clip = Clips_FX[index];
        }

        if (type == SOUND_TYPE.AMBIENT)
        {
            audio = AudioSource_Ambient;
            audio.clip = Clips_Ambient[index];
        }

        if (audio == null)
            return;

        audio.Play();
    }

    public void StopSound(SOUND_TYPE type)
    {
        AudioSource audio = null;

        if (type == SOUND_TYPE.MAIN)
            audio = AudioSource_Main;

        if (type == SOUND_TYPE.BGM)
            audio = AudioSource_BGM;

        if (type == SOUND_TYPE.FX)
            audio = AudioSource_FX;

        if (type == SOUND_TYPE.AMBIENT)
            audio = AudioSource_Ambient;

        if (audio == null || !audio.isPlaying)
            return;

        audio.Stop();
    }

    public void StopAll()
    {
        if (AudioSource_Main != null && AudioSource_Main.isPlaying) 
            AudioSource_Main.Stop();

        if (AudioSource_BGM != null && AudioSource_BGM.isPlaying) 
            AudioSource_BGM.Stop();

        if (AudioSource_FX != null && AudioSource_FX.isPlaying) 
            AudioSource_FX.Stop();

        if (AudioSource_Ambient != null && AudioSource_Ambient.isPlaying) 
            AudioSource_Ambient.Stop();
    }

    public float GetClipLength(SOUND_TYPE type, int index)
    {
        if (type == SOUND_TYPE.MAIN)
            return Clips_Main[index].length;

        if (type == SOUND_TYPE.BGM)
            return Clips_BGM[index].length;

        if (type == SOUND_TYPE.FX)
            return Clips_FX[index].length;

        if (type == SOUND_TYPE.AMBIENT)
            return Clips_Ambient[index].length;

        return 0;
    }

    public int GetClipIndex(SOUND_TYPE type, string name)
    {
        List<AudioClip> list = new List<AudioClip>();
        int result = -1;

        if (type == SOUND_TYPE.MAIN)
            list = Clips_Main;

        if (type == SOUND_TYPE.BGM)
            list = Clips_BGM;

        if (type == SOUND_TYPE.FX)
            list = Clips_FX;

        if (type == SOUND_TYPE.AMBIENT)
            list = Clips_Ambient;

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].name.Equals(name))
            {
                result = i;
                break;
            }
        }

        return result;
    }
}