using System;
using UnityEngine;
using FrostweepGames.Plugins.GoogleCloud.SpeechRecognition;
using System.Collections;
using UnityEngine.Android;

public class GoogleSTT : MonoBehaviour
{
	public enum Record
	{
		Start,
		Stop
	}

	public enum Talk
	{
		Start,
		Stop
	}

	public bool recording;

	public string sttText;

	private Record isRecord;
	private Talk isTalk;

	private GCSpeechRecognition _speechRecognition;

	private void Start()
	{
		recording = false;
		isRecord = Record.Stop;
		isTalk = Talk.Stop;

		_speechRecognition = GCSpeechRecognition.Instance;
		MicrophoneDevicesDropdownOnValueChangedEventHandler(0);

		_speechRecognition.RecognizeSuccessEvent += RecognizeSuccessEventHandler;
		_speechRecognition.LongRunningRecognizeSuccessEvent += LongRunningRecognizeSuccessEventHandler;
		_speechRecognition.FinishedRecordEvent += FinishedRecordEventHandler;
		_speechRecognition.BeginTalkigEvent += BeginTalkigEventHandler;
		_speechRecognition.EndTalkigEvent += EndTalkigEventHandler;

		//StartCoroutine(STT(Func));
		//StartCoroutine(STT(() => { GameObject.Find("Text").GetComponent<TextMesh>().text = sttText; }));
	}

    //private void Func()
    //{
    //	GameObject.Find("Text").GetComponent<TextMesh>().text = sttText;
    //}

    private void Update()
    {
		if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
			if(!recording)
            {
				GameObject.Find("Text").GetComponent<TextMesh>().text = "Start....";
				StartCoroutine(STT(() => { GameObject.Find("Text").GetComponent<TextMesh>().text = sttText; }));
			}
				
			recording = recording ? false : true;
		}
	}

    private IEnumerator STT(Action func)
    {
		isRecord = recording ? Record.Start : Record.Stop;

		if (isRecord == Record.Start && isTalk == Talk.Stop)
        {
			StartRecord();

			while (isRecord != Record.Stop)
            {
				if (!recording)
					break;

				yield return new WaitForEndOfFrame();
			}
				
			func();
		}

		yield return new WaitForEndOfFrame();

		if(recording)
			StartCoroutine(STT(func));
	}

    private void OnDestroy()
	{
		_speechRecognition.RecognizeSuccessEvent -= RecognizeSuccessEventHandler;
		_speechRecognition.LongRunningRecognizeSuccessEvent -= LongRunningRecognizeSuccessEventHandler;
		_speechRecognition.FinishedRecordEvent -= FinishedRecordEventHandler;
		_speechRecognition.EndTalkigEvent -= EndTalkigEventHandler;
	}

	private void MicrophoneDevicesDropdownOnValueChangedEventHandler(int value)
	{
		if (!_speechRecognition.HasConnectedMicrophoneDevices())
			return;

		_speechRecognition.SetMicrophoneDevice(_speechRecognition.GetMicrophoneDevices()[value]);
	}

	private void StartRecord()
	{
		sttText = string.Empty;
		_speechRecognition.StartRecord(true);
	}

	private void StopRecord()
	{
		_speechRecognition.StopRecord();
	}

	private void BeginTalkigEventHandler()
	{
		isTalk = Talk.Start;
	}

	private void EndTalkigEventHandler(AudioClip clip, float[] raw)
	{
		isTalk = Talk.Stop;
		FinishedRecordEventHandler(clip, raw);
		StopRecord();
	}

	private void FinishedRecordEventHandler(AudioClip clip, float[] raw)
	{
		if (clip == null)
			return;
		 
		RecognitionConfig config = RecognitionConfig.GetDefault();
		config.languageCode = Enumerators.LanguageCode.ko_KR.Parse();
		config.audioChannelCount = clip.channels;
		// configure other parameters of the config if need

		GeneralRecognitionRequest recognitionRequest = new GeneralRecognitionRequest()
		{
			audio = new RecognitionAudioContent()
			{
				content = raw.ToBase64()
			},
			//audio = new RecognitionAudioUri() // for Google Cloud Storage object
			//{
			//	uri = "gs://bucketName/object_name"
			//},
			config = config
		};

		_speechRecognition.Recognize(recognitionRequest);
	}

	private void RecognizeSuccessEventHandler(RecognitionResponse recognitionResponse)
	{
		InsertRecognitionResponseInfo(recognitionResponse);
	}

	private void LongRunningRecognizeSuccessEventHandler(Operation operation)
	{
		if (operation.error != null || !string.IsNullOrEmpty(operation.error.message))
			return;

		sttText = "Long Running Recognize Success.\n Operation name: " + operation.name;

		if (operation != null && operation.response != null && operation.response.results.Length > 0)
		{
			sttText = "Long Running Recognize Success.";
			sttText += "\n" + operation.response.results[0].alternatives[0].transcript;

			string other = "\nDetected alternatives:\n";

			foreach (var result in operation.response.results)
			{
				foreach (var alternative in result.alternatives)
				{
					if (operation.response.results[0].alternatives[0] != alternative)
						other += alternative.transcript + ", ";
				}
			}

			sttText += other;
		}
		else
			sttText = "Long Running Recognize Success. Words not detected.";
	}

	private void InsertRecognitionResponseInfo(RecognitionResponse recognitionResponse)
	{
		if (recognitionResponse == null || recognitionResponse.results.Length == 0)
		{
			sttText = "\nWords not detected.";
			return;
		}

		sttText = recognitionResponse.results[0].alternatives[0].transcript;

		string other = "";

		foreach (var result in recognitionResponse.results)
		{
			foreach (var alternative in result.alternatives)
			{
				if (recognitionResponse.results[0].alternatives[0] != alternative)
					other += alternative.transcript + ", ";
			}
		}

		sttText += " " + other;
		isRecord = Record.Stop;
	}
}