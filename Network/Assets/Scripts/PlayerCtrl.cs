using System.Collections;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private GameObject[] player;
    public GameObject prefab;

    private bool[] isCreate;

    private void Start()
    {
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        yield return new WaitForSeconds(2f);
        player = new GameObject[7];

        isCreate = new bool[7];
        isCreate[Singleton.Instance.index] = true;

        StartCoroutine(Calibration());
    }

    private IEnumerator Calibration()
    {
        if (!Singleton.Instance.totalPacket.isConnect[Singleton.Instance.index])
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(Calibration());
        }
        else
        {
            for (int i = 0; i < 7; i++)
            {
                if (i == Singleton.Instance.index)
                {
                    yield return new WaitForEndOfFrame();
                    continue;
                }
                    
                if (!Singleton.Instance.totalPacket.isConnect[i])
                {
                    if (player[i] != null)
                        DeletePlayer(i);

                    yield return new WaitForEndOfFrame();
                    continue;
                }
                
                if (!isCreate[i])
                    CreatePlayer(i);

                player[i].transform.position = new Vector3(Singleton.Instance.totalPacket.posX[i], Singleton.Instance.totalPacket.posY[i], Singleton.Instance.totalPacket.posZ[i]);
                player[i].transform.rotation = Quaternion.Euler(new Vector3(Singleton.Instance.totalPacket.rotX[i], Singleton.Instance.totalPacket.rotY[i], Singleton.Instance.totalPacket.rotZ[i]));
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForEndOfFrame();
            StartCoroutine(Calibration());
        }
    }

    private void DeletePlayer(int n)
    {
        Destroy(player[n]);
        player[n] = null;
        isCreate[n] = false;

        Destroy(GetComponent<Receiver>().sender[n]);
        GetComponent<Receiver>().sender[n] = null;

        string[] splitIp = Singleton.Instance.totalPacket.ip.Split('/');
        Singleton.Instance.totalPacket.ip = "";
        Singleton.Instance.ip[n] = "0";

        for (int i = 0; i < splitIp.Length; i++)
        {
            if (i == n)
                continue;

            if (i == 0)
                Singleton.Instance.totalPacket.ip += splitIp[i];
            else 
                Singleton.Instance.totalPacket.ip += "/" + splitIp[i];
        }

        if(GetComponent<NetworkManager>() != null)
            GetComponent<NetworkManager>().isCalibration[n] = false;

        if (n == 1)
            Singleton.Instance.isReceive = false;

    }

    private void CreatePlayer(int n)
    {
        player[n] = Instantiate(prefab);
        isCreate[n] = true;
    }
}