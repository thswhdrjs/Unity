using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public bool[] isCalibration = new bool[7];
    private bool isInit;

    private void Start()
    {
        if (Singleton.Instance.isInit)
            isInit = true;

        StartCoroutine(Receive());
    }

    private IEnumerator Receive()
    {
        //Receiver 없으면 다시 시작
        if (!Singleton.Instance.isReceive)
        {
            yield return new WaitForSeconds(2f);
            StartCoroutine(Receive());
        }
        else
        {
            Calibration();

            if (Singleton.Instance.index == 0)
                ServerWorks();
            else
                ClientWorks();

            yield return new WaitForEndOfFrame();
            StartCoroutine(Receive());
        }
    }

    private void Calibration()
    {
        for (int i = 0; i < 7; i++)
        {
            Singleton.Instance.totalPacket.posX[i] = Singleton.Instance.udp.GetComponent<Receiver>().receivePacket.posX[i];
            Singleton.Instance.totalPacket.posY[i] = Singleton.Instance.udp.GetComponent<Receiver>().receivePacket.posY[i];
            Singleton.Instance.totalPacket.posZ[i] = Singleton.Instance.udp.GetComponent<Receiver>().receivePacket.posZ[i];

            Singleton.Instance.totalPacket.rotX[i] = Singleton.Instance.udp.GetComponent<Receiver>().receivePacket.rotX[i];
            Singleton.Instance.totalPacket.rotY[i] = Singleton.Instance.udp.GetComponent<Receiver>().receivePacket.rotY[i];
            Singleton.Instance.totalPacket.rotZ[i] = Singleton.Instance.udp.GetComponent<Receiver>().receivePacket.rotZ[i];
        }
    }

    private void ServerWorks()
    {
        string[] splitIp = Singleton.Instance.totalPacket.ip.Split('/');

        for (int i = 0; i < splitIp.Length; i++)
        {
            if (splitIp[i] == Singleton.Instance.udp.GetComponent<Receiver>().receivePacket.ip)
            {
                if (Singleton.Instance.totalPacket.isConnect[i] == Singleton.Instance.udp.GetComponent<Receiver>().receivePacket.isConnect[i] == true)
                    isCalibration[i] = true;
                    
                    
                if (Singleton.Instance.totalPacket.isConnect[i] && !Singleton.Instance.udp.GetComponent<Receiver>().receivePacket.isConnect[i] && isCalibration[i])
                    Singleton.Instance.totalPacket.isConnect[i] = false;
            }
        }
        
        for(int i = 0; i < splitIp.Length; i++)
        {
            if (splitIp[i] == Singleton.Instance.udp.GetComponent<Receiver>().receivePacket.ip)
                return;
        }

        if(Singleton.Instance.isReceive)
        {
            Singleton.Instance.totalPacket.ip += "/" + Singleton.Instance.udp.GetComponent<Receiver>().receivePacket.ip;
            Singleton.Instance.ip[splitIp.Length] = Singleton.Instance.udp.GetComponent<Receiver>().receivePacket.ip;

            if (Singleton.Instance.udp.GetComponent<Receiver>().sender[splitIp.Length] == null)
            {
                Singleton.Instance.udp.GetComponent<Receiver>().sender[splitIp.Length] = Singleton.Instance.udp.AddComponent<Sender>();
                Singleton.Instance.udp.GetComponent<Receiver>().sender[splitIp.Length].receiverIp = Singleton.Instance.udp.GetComponent<Receiver>().receivePacket.ip;
                Singleton.Instance.totalPacket.isConnect[splitIp.Length] = true;
            }
        }
    }

    private void ClientWorks()
    {
        for (int i = 1; i < 7; i++)
            Singleton.Instance.totalPacket.isConnect[i] = Singleton.Instance.udp.GetComponent<Receiver>().receivePacket.isConnect[i];

        if (isInit)
            return;

        Singleton.Instance.isInit = true;
        Singleton.Instance.isReceive = false;

        Singleton.Instance.totalPacket.isConnect[0] = true;
        Singleton.Instance.index = Singleton.Instance.udp.GetComponent<Receiver>().receivePacket.ip.Split('/').Length - 1;
        Singleton.Instance.totalPacket.ip = Singleton.Instance.udp.GetComponent<Receiver>().receivePacket.ip;

        for (int i = 1; i < 7; i++)
            Singleton.Instance.totalPacket.isConnect[i] = Singleton.Instance.udp.GetComponent<Receiver>().receivePacket.isConnect[i];

        Receiver originReceiver = Singleton.Instance.udp.GetComponent<Receiver>();
        Singleton.Instance.udp.AddComponent<Receiver>().prefab = originReceiver.prefab;

        if (Singleton.Instance.udp.GetComponent<Sender>() != null)
            Destroy(Singleton.Instance.udp.GetComponent<Sender>());

        if (Singleton.Instance.udp.GetComponent<PlayerCtrl>() != null)
            Destroy(Singleton.Instance.udp.GetComponent<PlayerCtrl>());

        Singleton.Instance.udp.GetComponent<Receiver>().CloseReceiver();
        Destroy(originReceiver);
        Destroy(GetComponent<NetworkManager>());
    }
}