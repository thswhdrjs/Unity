using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //대기 중인지 확인
    public bool waitCheck;

    //말하고 있는지 확인
    public bool isSpeak;

    private void Start()
    {
        StartCoroutine(Contents());
    }

    //컨텐츠 차례대로 실행
    private IEnumerator Contents()
    {
        #region Setting
        Singleton.Instance.microphoneManager.enabled = false;
        Singleton.Instance.textMatch.SetActive(false);
        Singleton.Instance.textCSR.SetActive(false);
        #endregion

        #region TTS 1(안녕하세요, 네)
        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.FX, 0);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.FX, 0));

        Singleton.Instance.textMatch.GetComponent<Text>().text = "안녕하세요";
        Singleton.Instance.textMatch.SetActive(true);

        Singleton.Instance.textCSR.GetComponent<Text>().text = "=> ";
        Singleton.Instance.textCSR.SetActive(true);

        Singleton.Instance.microphoneManager.enabled = true;
        yield return StartCoroutine(Listen("안녕하세요"));
        yield return StartCoroutine(Wait());
        Singleton.Instance.microphoneManager.enabled = false;

        Singleton.Instance.textMatch.SetActive(false);
        Singleton.Instance.textCSR.SetActive(false);

        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.FX, 1);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.FX, 1));

        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.FX, 2);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.FX, 2));

        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.FX, 3);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.FX, 3));

        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.FX, 4);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.FX, 4));

        Singleton.Instance.textMatch.GetComponent<Text>().text = "알겠습니다";
        Singleton.Instance.textMatch.SetActive(true);

        Singleton.Instance.textCSR.GetComponent<Text>().text = "=> ";
        Singleton.Instance.textCSR.SetActive(true);

        Singleton.Instance.microphoneManager.enabled = true;
        yield return StartCoroutine(Listen("알겠습니다"));
        yield return StartCoroutine(Wait());
        Singleton.Instance.microphoneManager.enabled = false;

        Singleton.Instance.textMatch.SetActive(false);
        Singleton.Instance.textCSR.SetActive(false);
        #endregion

        #region TTS 2(모르겠어요)
        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 0);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 0));

        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 1);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 1));

        Singleton.Instance.textMatch.GetComponent<Text>().text = "모르겠어요";
        Singleton.Instance.textMatch.SetActive(true);

        Singleton.Instance.textCSR.GetComponent<Text>().text = "=> ";
        Singleton.Instance.textCSR.SetActive(true);

        Singleton.Instance.microphoneManager.enabled = true;
        yield return StartCoroutine(Listen("모르겠어요"));
        yield return StartCoroutine(Wait());
        Singleton.Instance.microphoneManager.enabled = false;

        Singleton.Instance.textMatch.SetActive(false);
        Singleton.Instance.textCSR.SetActive(false);
        #endregion

        #region TTS 3(모르겠어요)
        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 2);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 2));

        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 3);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 3));

        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 4);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 4));

        Singleton.Instance.textMatch.GetComponent<Text>().text = "모르겠어요";
        Singleton.Instance.textMatch.SetActive(true);

        Singleton.Instance.textCSR.GetComponent<Text>().text = "=> ";
        Singleton.Instance.textCSR.SetActive(true);

        Singleton.Instance.microphoneManager.enabled = true;
        yield return StartCoroutine(Listen("모르겠어요"));
        yield return StartCoroutine(Wait());
        Singleton.Instance.microphoneManager.enabled = false;

        Singleton.Instance.textMatch.SetActive(false);
        Singleton.Instance.textCSR.SetActive(false);
        #endregion

        #region TTS 4(모르겠어요)
        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 5);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 5));

        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 6);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 6));

        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 7);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 7));

        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 8);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 8));

        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 9);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 9));

        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 10);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 10));

        Singleton.Instance.textMatch.GetComponent<Text>().text = "모르겠어요";
        Singleton.Instance.textMatch.SetActive(true);

        Singleton.Instance.textCSR.GetComponent<Text>().text = "=> ";
        Singleton.Instance.textCSR.SetActive(true);

        Singleton.Instance.microphoneManager.enabled = true;
        yield return StartCoroutine(Listen("모르겠어요"));
        yield return StartCoroutine(Wait());
        Singleton.Instance.microphoneManager.enabled = false;

        Singleton.Instance.textMatch.SetActive(false);
        Singleton.Instance.textCSR.SetActive(false);

        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 11);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 11));

        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 12);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 12));

        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 13);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 13));

        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 14);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 14));

        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 15);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 15));

        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 16);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 16));

        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 17);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 17));

        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 18);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 18));
        #endregion
    }

    //대기
    private IEnumerator Wait()
    {
        while (!waitCheck)
            yield return new WaitForEndOfFrame();

        yield return new WaitForEndOfFrame();
        waitCheck = false;
    }

    //말하면 녹음 시작
    private IEnumerator Listen(string matchText)
    {
        Singleton.Instance.microphoneManager.matchText = matchText;
        yield return new WaitForEndOfFrame();
        //if (Singleton.Instance.microphoneManager.loudness > 0.1f && !isSpeak)
        //{
        //    Singleton.Instance.microphoneManager.enabled = false;
        //    Singleton.Instance.microphoneManager.enabled = true;
        //    isSpeak = true;
        //}

        //if(!waitCheck)
        //{
        //    yield return new WaitForEndOfFrame();
        //    StartCoroutine(Listen(matchText));
        //}
    }
}