using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    private GameObject[] arrowPull;
    public GameObject arrow;

    private float fadeTime, time;
    public float playerAttackTime, enemyAttackTime;
    private float playerAttackTimer, enemyAttackTimer;

    public int enemyCount = 7, catchCount;
    public int playerDamage, enemyDamage;

    public bool[] isCrash;
    public bool isInit, isMove;

    private void Start()
    {
        Singleton.Instance.panelGameOver.SetActive(false);
    }

    private void OnEnable()
    {
        Application.targetFrameRate = 500;

        playerDamage = 50;
        playerAttackTime = 1f;

        enemyDamage = 25;
        enemyAttackTime = 2f;

        enemyCount = Singleton.Instance.enemy.transform.childCount;
        catchCount = 0;

        isCrash = new bool[enemyCount];
        fadeTime = 2f;

        arrowPull = new GameObject[enemyCount];

        Singleton.Instance.door.SetActive(false);
        Singleton.Instance.canvasFade.SetActive(false);
        Singleton.Instance.panelFrame.SetActive(false);

        StartCoroutine(Clear());
    }

    private void Update()
    {
        playerAttackTimer += Time.deltaTime;
        enemyAttackTimer += Time.deltaTime;

        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    private void Initialization()
    {
        Singleton.Instance.door.SetActive(false);
        Singleton.Instance.character.GetComponent<Player>().enabled = false;
        Singleton.Instance.enemy.GetComponent<Enemy>().enabled = false;

        Singleton.Instance.character.GetComponent<Player>().enabled = true;
        Singleton.Instance.enemy.GetComponent<Enemy>().enabled = true;
    }

    private IEnumerator Clear()
    {
        if (enemyCount == catchCount)
        {
            playerAttackTimer = 0f;
            enemyAttackTimer = 0f;

            StartCoroutine(RandomInit());
            StartCoroutine(EventManager.Instance.EXPUp());

            yield return new WaitForSeconds(0.5f);
            catchCount = 0;
        }

        yield return new WaitForEndOfFrame();
        StartCoroutine(Clear());
    }

    public IEnumerator CreateArrow(GameObject obj)
    {
        if (obj == Singleton.Instance.character)
        {
            if (playerAttackTimer > playerAttackTime)
            {
                playerAttackTimer = 0f;

                GameObject weapon = Instantiate(arrow);
                weapon.transform.parent = Singleton.Instance.player.transform;
                weapon.transform.position = obj.transform.position;
                weapon.transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        else
        {
            if(enemyAttackTimer > enemyAttackTime)
            {
                enemyAttackTimer = 0f;

                for (int i = 0; i < arrowPull.Length; i++)
                {
                    if (!obj.transform.GetChild(i).gameObject.activeSelf)
                        continue;

                    arrowPull[i] = Instantiate(arrow);
                    arrowPull[i].transform.parent = obj.transform;
                    arrowPull[i].transform.position = obj.transform.GetChild(i).position;
                    arrowPull[i].transform.eulerAngles = Vector3.zero;
                }
            }   
        }

        if(Singleton.Instance.character.activeSelf && enemyCount != catchCount)
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(CreateArrow(obj));
        }
    }

    public IEnumerator RandomInit()
    {
        for (int i = 0; i < Singleton.Instance.points.transform.childCount; i++)
        {
            Singleton.Instance.enemy.GetComponent<Enemy>().random[i] = Random.Range(0, Singleton.Instance.points.transform.childCount);

            if (i > 0)
            {
                while (true)
                {
                    bool check = false;

                    for (int j = 0; j < i; j++)
                    {
                        if (Singleton.Instance.enemy.GetComponent<Enemy>().random[i] == Singleton.Instance.enemy.GetComponent<Enemy>().random[j])
                        {
                            Singleton.Instance.enemy.GetComponent<Enemy>().random[i] = Random.Range(0, Singleton.Instance.points.transform.childCount);
                            break;
                        }

                        if (j == i - 1)
                            check = true;

                        yield return new WaitForEndOfFrame();
                    }

                    if (check)
                        break;

                    yield return new WaitForEndOfFrame();
                }
            }

            yield return new WaitForEndOfFrame();
        }

        isInit = true;
        playerAttackTimer = 0f;
        enemyAttackTimer = 0f;
    }

    public IEnumerator Fade()
    {
        Singleton.Instance.canvasFade.SetActive(true);
        Image fadeImg = Singleton.Instance.panelFade.GetComponent<Image>();
        fadeImg.color = new Color(0, 0, 0, 0);
        Color color = fadeImg.color;
        time = 0f;

        while (color.a < 1f)
        {
            time += Time.deltaTime / fadeTime;
            color.a = Mathf.Lerp(0, 1, time);
            fadeImg.color = color;
            yield return new WaitForEndOfFrame();
        }

        time = 0f;
        Initialization();

        while (!isInit)
            yield return new WaitForEndOfFrame();

        while (color.a > 0f)
        {
            time += Time.deltaTime / fadeTime;
            color.a = Mathf.Lerp(1, 0, time);
            fadeImg.color = color;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();
    }
}
