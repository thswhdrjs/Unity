using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

// ��� �߰�
using Firebase;
using Firebase.Database;
using Firebase.Unity;
using UnityEngine.SceneManagement;


public class FirebaseManager : MonoBehaviour
{
    [SerializeField]
    public static string email, pw;

    // ������ ������ ��ü
    public static FirebaseAuth auth;

    // �����͸� ���� ���� reference ����
    public static DatabaseReference reference;

    private User user;

    private void Awake()
    {
        // ��ü �ʱ�ȭ
        email = "test2@a.com";
        pw = "qwer1234";

        auth = FirebaseAuth.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        Login();
    }

    private void Login()
    {
        // �����Ǵ� �Լ� : �̸��ϰ� ��й�ȣ�� �α��� ���� ��
        auth.SignInWithEmailAndPasswordAsync(email, pw).ContinueWith(
            task => 
            {
                if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
                    Debug.Log(email + " �� �α��� �ϼ̽��ϴ�.");
                else
                {
                    Debug.Log("�α��ο� �����ϼ̽��ϴ�.");
                    Register();
                }
            }
        );
    }

    private void Register()
    {
        // �����Ǵ� �Լ� : �̸��ϰ� ��й�ȣ�� ȸ������ ���� ��
        auth.CreateUserWithEmailAndPasswordAsync(email, pw).ContinueWith(
            task => 
            {
                print(!task.IsFaulted);
                if (!task.IsCanceled && !task.IsFaulted)
                {
                    Debug.Log(email + "�� ȸ������\n");

                    //ȸ�������� ������� ���� ��ȣ�� ���� ����� �⺻ ����
                    //FirebaseUser newUser = task.Result;
                    WriteUser("Test", 0);
                    Login();
                }
                else
                    Debug.Log("ȸ������ ����\n");
            });
    }

    // ����� Ŭ���� ����
    private class User
    {
        public string user_avt;
        public int count;

        public User(int count)
        {
            this.user_avt = "Test";
            this.count = count;
        }
    }

    // ������ ȸ�� ���� ��ȣ�� ���� ����� �⺻�� ����
    public void WriteUser(string userId, int count) 
    {
        user = new User(count);

        // ������ ����ڿ� ���� ���� json �������� ����
        string json = JsonUtility.ToJson(user);

        // �����ͺ��̽��� json ���� ���ε�
        reference.Child(userId).SetRawJsonValueAsync(json); 
    }
}