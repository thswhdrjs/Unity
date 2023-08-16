using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

// 요소 추가
using Firebase;
using Firebase.Database;
using Firebase.Unity;
using UnityEngine.SceneManagement;


public class FirebaseManager : MonoBehaviour
{
    [SerializeField]
    public static string email, pw;

    // 인증을 관리할 객체
    public static FirebaseAuth auth;

    // 데이터를 쓰기 위한 reference 변수
    public static DatabaseReference reference;

    private User user;

    private void Awake()
    {
        // 객체 초기화
        email = "test2@a.com";
        pw = "qwer1234";

        auth = FirebaseAuth.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        Login();
    }

    private void Login()
    {
        // 제공되는 함수 : 이메일과 비밀번호로 로그인 시켜 줌
        auth.SignInWithEmailAndPasswordAsync(email, pw).ContinueWith(
            task => 
            {
                if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
                    Debug.Log(email + " 로 로그인 하셨습니다.");
                else
                {
                    Debug.Log("로그인에 실패하셨습니다.");
                    Register();
                }
            }
        );
    }

    private void Register()
    {
        // 제공되는 함수 : 이메일과 비밀번호로 회원가입 시켜 줌
        auth.CreateUserWithEmailAndPasswordAsync(email, pw).ContinueWith(
            task => 
            {
                print(!task.IsFaulted);
                if (!task.IsCanceled && !task.IsFaulted)
                {
                    Debug.Log(email + "로 회원가입\n");

                    //회원가입한 사용자의 고유 번호를 통한 사용자 기본 생성
                    //FirebaseUser newUser = task.Result;
                    WriteUser("Test", 0);
                    Login();
                }
                else
                    Debug.Log("회원가입 실패\n");
            });
    }

    // 사용자 클래스 생성
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

    // 가입한 회원 고유 번호에 대한 사용자 기본값 설정
    public void WriteUser(string userId, int count) 
    {
        user = new User(count);

        // 생성한 사용자에 대한 정보 json 형식으로 저장
        string json = JsonUtility.ToJson(user);

        // 데이터베이스에 json 파일 업로드
        reference.Child(userId).SetRawJsonValueAsync(json); 
    }
}