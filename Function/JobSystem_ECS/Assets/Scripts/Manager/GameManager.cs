using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static bool isWait, whileCheck;

    [SerializeField]
    private bool[] sceneChoice;

    private void Start()
    {
        FunctionManager.Instance.SetFrameRate(90);

        StartCoroutine(Contents());
    }

    //Contents
    private IEnumerator Contents()
    {
        #region Setting

        yield return new WaitUntil(() => true);


        #endregion

        #region Phase1

        if (sceneChoice[0])
        {
            /* Fade Out */
            StartCoroutine(FunctionManager.Instance.Fade(true, () => whileCheck = true));

            yield return new WaitUntil(() => whileCheck);
            whileCheck = false;

            /*Function*/


            /* Wait */
            yield return new WaitUntil(() => whileCheck);
            whileCheck = false;
        }

        #endregion
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}