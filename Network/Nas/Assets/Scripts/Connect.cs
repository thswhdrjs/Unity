using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Video;
using System.Diagnostics;

public class Connect : MonoBehaviour
{
    string address, ID, PW;
    public static bool checkRegit;

    // Start is called before the first frame update
    void Start()
    {
        address = "http://isparkstg.synology.me:5005/Team/AVR/일학습/Test/";
        ID = "jgson";
        PW = "Qwer1234";
    }

    private void Update()
    {
        if (!checkRegit)
            print("Setting...");
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
                //Process.Start(@"C:\test\Connect.exe");
                ConnectNetwork(address, ID, PW);
            else if (Input.GetKeyDown(KeyCode.Escape))
                DisconnectNetwork();
        }
    }

    private void ConnectNetwork(string address, string ID, string PW)
    {
        // 네트워크 드라이브 연결
        NetDrive.csNetDrive netDrive = new NetDrive.csNetDrive();
        int getReturn = netDrive.setRemoteConnection(address, ID, PW, "Y:");

        if (getReturn == 0)
        {
            print("set Net Drive : " + "네트워크 드라이브가 성공적으로 연결되었습니다.");
            NetDrive.csNetDrive.isConnect = true;
            GameObject.Find("RawImage").AddComponent<VideoController>();
        }
        else
        {
            print("set Net Drive : " + "네트워크 드라이브를 연결할 수 없습니다.\r\n연결 정보를 확인하세요.");
            NetDrive.csNetDrive.isConnect = false;
        }
    }
     
    private void DisconnectNetwork()
    {
        // 네트워크 드라이브 연결 해제
        NetDrive.csNetDrive dataDrive = new NetDrive.csNetDrive();
        int getReturn = dataDrive.CencelRemoteServer("Y:");
        NetDrive.csNetDrive.isConnect = false;

        Destroy(GameObject.Find("RawImage").GetComponent<VideoController>());
        Destroy(GameObject.Find("RawImage").GetComponent<VideoPlayer>());
        Destroy(GameObject.Find("RawImage").GetComponent<AudioSource>());

        print("set Net Drive : " + "네트워크 드라이브가 성공적으로 해제되었습니다.");
    }
}