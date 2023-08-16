using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton
{
    private static Singleton instance;

    public static Singleton Instance
    {
        get
        {
            if (null == instance)
                instance = new Singleton();

            return instance;
        }
    }

    public GameObject dev;
    public GameManager gameManager;
    public SoundManager soundManager;
    public MicrophoneManager microphoneManager;

    public GameObject canvas, textMatch, textCSR;

    public Singleton()
    {
        dev = GameObject.Find("---------------------------Dev");
        gameManager = dev.transform.GetChild(0).GetComponent<GameManager>();
        soundManager = dev.transform.GetChild(1).GetComponent<SoundManager>();
        microphoneManager = dev.transform.GetChild(2).GetComponent<MicrophoneManager>();

        canvas = GameObject.Find("Canvas");
        textMatch = canvas.transform.GetChild(0).gameObject;
        textCSR = canvas.transform.GetChild(1).gameObject;
    }

    public void InitGame()
    {

    }

    public void PauseOrContinueGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Time.timeScale = Time.timeScale == 1.0f ? 0.0f : 1.0f;
    }

    public void RestartGame(string sceneName, bool check)
    {
        if (check)
        {
            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene(sceneName);
        }
        else
            SceneManager.LoadScene(sceneName);
    }

    public void StopGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}