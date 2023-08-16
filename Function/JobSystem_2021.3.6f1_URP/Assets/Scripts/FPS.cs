using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    private GUIStyle style;
    private Rect rect;

    private string text;

    private float deltaTime;
    private float msec;

    private float fps;
    private float worstFps;

    private void Awake()
    {
        deltaTime = 0.0f;
        worstFps = 100f;

        int w = Screen.width, h = Screen.height;
        rect = new Rect(0, 0, w, h * 4 / 100);

        style = new GUIStyle();
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 4 / 100;
        style.normal.textColor = Color.cyan;

        StartCoroutine("WorstReset");
    }

    private void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    private void OnGUI()
    {
        msec = deltaTime * 1000.0f;
        fps = 1.0f / deltaTime;

        if (fps < worstFps)
            worstFps = fps;

        text = fps.ToString("F1") + "FPS";
        GUI.Label(rect, text, style);
    }

    private IEnumerator WorstReset()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            worstFps = 100f;
        }
    }
}
