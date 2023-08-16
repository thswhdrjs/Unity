using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton
{
    private static Singleton instance;

    public static Singleton Instance
    {
        get
        {
            if(null == instance)
                instance = new Singleton();

            return instance;
        }
    }

    public GameObject udp;
    public GameObject mySelf;

    public Packet totalPacket;

    public string[] ip;
    public string serverIp;

    public int port, index;

    public bool isReceive, isInit;

    public Singleton()
    {
        udp = GameObject.Find("UDP");
        mySelf = GameObject.Find("Myself");

        totalPacket = new Packet();

        totalPacket.isConnect = new bool[7];

        totalPacket.posX = new float[7];
        totalPacket.posY = new float[7];
        totalPacket.posZ = new float[7];

        totalPacket.rotX = new float[7];
        totalPacket.rotY = new float[7];
        totalPacket.rotZ = new float[7];

        totalPacket.ip = "0";
        ip = new string[7];

        for (int i = 0; i < 7; i++)
            ip[i] = "0";

        serverIp = "192.168.50.37";
        port = 10001;
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