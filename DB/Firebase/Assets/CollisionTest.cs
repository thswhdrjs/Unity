using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
// ��� �߰�
using Firebase;
using Firebase.Database;
using Firebase.Unity;
using UnityEngine.SceneManagement;
using System;

public class CollisionTest : MonoBehaviour
{
    private FirebaseManager authMgr;
           
    public int count;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Cube")
        {
            // �����Ǵ� �Լ� : �̸��ϰ� ��й�ȣ�� ȸ������ ���� ��
            FirebaseManager.auth.SignInWithEmailAndPasswordAsync(FirebaseManager.email, FirebaseManager.pw).ContinueWith(
                task =>
                {
                    //print(task.Result.UserId);
                    if (!task.IsCanceled && !task.IsFaulted)
                    {
                        count = Convert.ToInt32(FirebaseManager.reference.Child("Test").Child("count").GetValueAsync().Result.Value);
                        authMgr.WriteUser("Test", ++count);
                        print(Convert.ToInt32(FirebaseManager.reference.Child("Test").Child("count").GetValueAsync().Result.Value));
                    }
                    else
                        Debug.Log("������Ʈ ����\n");
                });
        }
    }

    private void Start()
    {
        authMgr = GameObject.Find("FireBase").GetComponent<FirebaseManager>();
    }
}
