using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

public class Sender : MonoBehaviour
{
    public UdpClient sender;
    public string receiverIp;

    private void Start()
    {
        InitSender();
    }

    private void OnApplicationQuit()
    {
        Singleton.Instance.totalPacket.isConnect[Singleton.Instance.index] = false;
        byte[] packet = StructToByteArray(Singleton.Instance.totalPacket);

        for (int i = 0; i < 1000; i++)
            sender.BeginSend(packet, packet.Length, new AsyncCallback(SendCallback), sender);

        CloseSender();
    }

    private void OnDestroy()
    {
        CloseSender();
    }

    private void InitSender()
    {
        sender = new UdpClient();
        sender.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        sender.Connect(IPAddress.Parse(receiverIp), Singleton.Instance.port);

        if (Singleton.Instance.isInit || (Singleton.Instance.index == 0 && GetComponent<PlayerCtrl>() == null))
            gameObject.AddComponent<PlayerCtrl>().prefab = GetComponent<Receiver>().prefab;

        StartCoroutine(DoBeginSend());
    }                                                         

    private void SetSendPacket()
    {
        Singleton.Instance.totalPacket.posX[Singleton.Instance.index] = Singleton.Instance.mySelf.transform.position.x;
        Singleton.Instance.totalPacket.posY[Singleton.Instance.index] = Singleton.Instance.mySelf.transform.position.y;
        Singleton.Instance.totalPacket.posZ[Singleton.Instance.index] = Singleton.Instance.mySelf.transform.position.z;

        Singleton.Instance.totalPacket.rotX[Singleton.Instance.index] = Singleton.Instance.mySelf.transform.eulerAngles.x;
        Singleton.Instance.totalPacket.rotY[Singleton.Instance.index] = Singleton.Instance.mySelf.transform.eulerAngles.y;
        Singleton.Instance.totalPacket.rotZ[Singleton.Instance.index] = Singleton.Instance.mySelf.transform.eulerAngles.z;
    }
    
    private IEnumerator DoBeginSend()
    {
        SetSendPacket();
        byte[] packet = StructToByteArray(Singleton.Instance.totalPacket);
        sender.BeginSend(packet, packet.Length, new AsyncCallback(SendCallback), sender);

        yield return new WaitForEndOfFrame();
        StartCoroutine(DoBeginSend());
    }

    public void SendCallback(IAsyncResult ar)
    {
        UdpClient udpClient = (UdpClient)ar.AsyncState;
    }

    public void CloseSender()
    {
        if (sender != null)
        {
            sender.Close();
            sender = null;
        }
    }

    public byte[] StructToByteArray(object obj)
    {
        int size = Marshal.SizeOf(obj);
        byte[] arr = new byte[size];
        IntPtr ptr = Marshal.AllocHGlobal(size);

        Marshal.StructureToPtr(obj, ptr, true);
        Marshal.Copy(ptr, arr, 0, size);
        Marshal.FreeHGlobal(ptr);
        return arr;
    }
}