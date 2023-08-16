using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    public Packet totalPacket, receivePacket;

    // Update is called once per frame
    void Update()
    {
        totalPacket = Singleton.Instance.totalPacket;
        receivePacket = Singleton.Instance.udp.GetComponent<Receiver>().receivePacket;
    }
}
