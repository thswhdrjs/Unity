using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //��� ������ Ȯ��
    public bool waitCheck;

    //���ϰ� �ִ��� Ȯ��
    public bool isSpeak;

    private void Start()
    {
        StartCoroutine(Contents());
    }

    //������ ���ʴ�� ����
    private IEnumerator Contents()
    {
        #region Setting
        Singleton.Instance.microphoneManager.enabled = false;
        Singleton.Instance.textMatch.SetActive(false);
        Singleton.Instance.textCSR.SetActive(false);
        #endregion

        #region TTS 1(�ȳ��ϼ���, ��)
        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.FX, 0);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.FX, 0));

        Singleton.Instance.textMatch.GetComponent<Text>().text = "�ȳ��ϼ���";
        Singleton.Instance.textMatch.SetActive(true);

        Singleton.Instance.textCSR.GetComponent<Text>().text = "=> ";
        Singleton.Instance.textCSR.SetActive(true);

        Singleton.Instance.microphoneManager.enabled = true;
        yield return StartCoroutine(Listen("�ȳ��ϼ���"));
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

        Singleton.Instance.textMatch.GetComponent<Text>().text = "�˰ڽ��ϴ�";
        Singleton.Instance.textMatch.SetActive(true);

        Singleton.Instance.textCSR.GetComponent<Text>().text = "=> ";
        Singleton.Instance.textCSR.SetActive(true);

        Singleton.Instance.microphoneManager.enabled = true;
        yield return StartCoroutine(Listen("�˰ڽ��ϴ�"));
        yield return StartCoroutine(Wait());
        Singleton.Instance.microphoneManager.enabled = false;

        Singleton.Instance.textMatch.SetActive(false);
        Singleton.Instance.textCSR.SetActive(false);
        #endregion

        #region TTS 2(�𸣰ھ��)
        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 0);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 0));

        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 1);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 1));

        Singleton.Instance.textMatch.GetComponent<Text>().text = "�𸣰ھ��";
        Singleton.Instance.textMatch.SetActive(true);

        Singleton.Instance.textCSR.GetComponent<Text>().text = "=> ";
        Singleton.Instance.textCSR.SetActive(true);

        Singleton.Instance.microphoneManager.enabled = true;
        yield return StartCoroutine(Listen("�𸣰ھ��"));
        yield return StartCoroutine(Wait());
        Singleton.Instance.microphoneManager.enabled = false;

        Singleton.Instance.textMatch.SetActive(false);
        Singleton.Instance.textCSR.SetActive(false);
        #endregion

        #region TTS 3(�𸣰ھ��)
        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 2);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 2));

        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 3);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 3));

        Singleton.Instance.soundManager.GetComponent<SoundManager>().PlaySound(TIMEnum.SOUND_TYPE.MAIN, 4);
        yield return new WaitForSeconds(Singleton.Instance.soundManager.GetComponent<SoundManager>().GetClipLength(TIMEnum.SOUND_TYPE.MAIN, 4));

        Singleton.Instance.textMatch.GetComponent<Text>().text = "�𸣰ھ��";
        Singleton.Instance.textMatch.SetActive(true);

        Singleton.Instance.textCSR.GetComponent<Text>().text = "=> ";
        Singleton.Instance.textCSR.SetActive(true);

        Singleton.Instance.microphoneManager.enabled = true;
        yield return StartCoroutine(Listen("�𸣰ھ��"));
        yield return StartCoroutine(Wait());
        Singleton.Instance.microphoneManager.enabled = false;

        Singleton.Instance.textMatch.SetActive(false);
        Singleton.Instance.textCSR.SetActive(false);
        #endregion

        #region TTS 4(�𸣰ھ��)
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

        Singleton.Instance.textMatch.GetComponent<Text>().text = "�𸣰ھ��";
        Singleton.Instance.textMatch.SetActive(true);

        Singleton.Instance.textCSR.GetComponent<Text>().text = "=> ";
        Singleton.Instance.textCSR.SetActive(true);

        Singleton.Instance.microphoneManager.enabled = true;
        yield return StartCoroutine(Listen("�𸣰ھ��"));
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

    //���
    private IEnumerator Wait()
    {
        while (!waitCheck)
            yield return new WaitForEndOfFrame();

        yield return new WaitForEndOfFrame();
        waitCheck = false;
    }

    //���ϸ� ���� ����
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