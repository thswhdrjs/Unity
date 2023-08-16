using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject arrow;

    public float time;
    public int[] random;

    private void OnEnable()
    {
        StartCoroutine(EnemyInit());
    }

    private IEnumerator EnemyInit()
    {
        random = new int[Singleton.Instance.points.transform.childCount];
        StartCoroutine(GameManager.Instance.RandomInit());

        while (!GameManager.Instance.isInit)
            yield return new WaitForEndOfFrame();

        for (int i = 0; i < GameManager.Instance.enemyCount; i++)
        {
            transform.GetChild(i).GetChild(0).GetChild(0).GetChild(0).GetComponent<Slider>().value = 100f;
            transform.GetChild(i).position = Singleton.Instance.points.transform.GetChild(random[i]).position;
            transform.GetChild(i).gameObject.SetActive(true);

            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(GameManager.Instance.RandomInit());
        StartCoroutine(RandomMove());
        StartCoroutine(GameManager.Instance.CreateArrow(gameObject));
    }

    private IEnumerator RandomMove()
    {
        time += Time.deltaTime;

        if(time > 5f)
        {
            time = 0;
            StartCoroutine(GameManager.Instance.RandomInit());
        }

        for(int i = 0; i < GameManager.Instance.enemyCount; i++)
            transform.GetChild(i).position = Vector3.MoveTowards(transform.GetChild(i).position, Singleton.Instance.points.transform.GetChild(random[i]).position, 0.005f);

        if (Singleton.Instance.character.activeSelf && GameManager.Instance.enemyCount != GameManager.Instance.catchCount)
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(RandomMove());
        }
    }
}
