using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mic : MonoBehaviour
{
    private void Start()
    {
		print(Microphone.devices[0]);
		AudioSource aud = GetComponent<AudioSource>();
		aud.clip = Microphone.Start(Microphone.devices[0], true, 10, 44100);
		aud.Play();
	}
}