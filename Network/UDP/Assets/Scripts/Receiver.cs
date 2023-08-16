using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.IO;

public class Receiver : MonoBehaviour
{
    public GameObject prefab;

    public Sender[] sender = new Sender[7];
    private UdpClient receiver;

    public Packet receivePacket;
    private byte[] receiveBytes;

    private IPHostEntry ipEntry;
    private IPAddress[] addr;

    private void Start()
    {
        StartSetting();
        InitReceiver();
    }

    private void StartSetting()
    {
        ipEntry = Dns.GetHostEntry(Dns.GetHostName());
        addr = ipEntry.AddressList;

        Singleton.Instance.ip[0] = addr[1].ToString();
        Singleton.Instance.totalPacket.ip = Singleton.Instance.ip[0];

        if (!Singleton.Instance.isInit)
            Singleton.Instance.index = addr[1].ToString() == Singleton.Instance.serverIp ? 0 : 6;

        if(addr[1].ToString() == Singleton.Instance.serverIp)
            Singleton.Instance.totalPacket.isConnect[Singleton.Instance.index] = true;
    }

    private void OnApplicationQuit()
    {
        CloseReceiver();
    }

    private void OnDestroy()
    {
        CloseReceiver();
    }

    private void InitReceiver()
    {
        try
        {
            if (receiver == null)
            {
                receiver = new UdpClient(Singleton.Instance.port);
                receiver.BeginReceive(new AsyncCallback(DoBeginReceive), null);

                if (Singleton.Instance.ip[0] != Singleton.Instance.serverIp)
                {
                    sender[Singleton.Instance.index] = Singleton.Instance.udp.AddComponent<Sender>();
                    sender[Singleton.Instance.index].receiverIp = Singleton.Instance.serverIp;
                }

                gameObject.AddComponent<NetworkManager>();
            }
        }
        catch (SocketException e)
        {
            InitReceiver();
        }
    }

    private void DoBeginReceive(IAsyncResult ar)
    {
        IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, Singleton.Instance.port);

        if (receiver != null)
            receiveBytes = receiver.EndReceive(ar, ref ipEndPoint);
        else
            return;

        receiver.BeginReceive(new AsyncCallback(DoBeginReceive), null);
        DoReceive();
    }

    private void DoReceive()
    {
        receivePacket = ByteArrayToStruct<Packet>(receiveBytes);
        Singleton.Instance.isReceive = true;
    }

    public void CloseReceiver()
    {
        if (receiver != null)
        {
            receiver.Close();
            receiver = null;
        }
    }

    private T ByteArrayToStruct<T>(byte[] buffer) where T : struct
    {
        int size = Marshal.SizeOf(typeof(T));
        if (size > buffer.Length)
        {
            throw new Exception();
        }

        IntPtr ptr = Marshal.AllocHGlobal(size);
        Marshal.Copy(buffer, 0, ptr, size);
        T obj = (T)Marshal.PtrToStructure(ptr, typeof(T));
        Marshal.FreeHGlobal(ptr);
        return obj;
    }
}