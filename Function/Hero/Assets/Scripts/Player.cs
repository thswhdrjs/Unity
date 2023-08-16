using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 playerPos;
    public bool isMoving;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Singleton.Instance.door)
            StartCoroutine(GameManager.Instance.Fade());
    }

    private void Start()
    {
        playerPos = transform.localPosition;
    }

    private void OnEnable()
    {
        StartCoroutine(PlayerInit());

        if(!GameManager.Instance.isMove)
            StartCoroutine(Move());
    }

    private void OnDisable()
    {
        if(Singleton.Instance.enemy != null)
            Singleton.Instance.enemy.GetComponent<Enemy>().enabled = false;
    }

    private IEnumerator PlayerInit()
    {
        while (!GameManager.Instance.isInit)
            yield return new WaitForEndOfFrame();

        transform.localPosition = playerPos;
        StartCoroutine(GameManager.Instance.CreateArrow(gameObject));
    }

    private IEnumerator Move()
    {
        GameManager.Instance.isMove = true;

        if (Input.GetKey(KeyCode.UpArrow) && transform.localPosition.y < 5.09f)
            transform.position += new Vector3(0, 0.01f, 0);

        if (Input.GetKey(KeyCode.DownArrow) && transform.localPosition.y > -5.09f)
            transform.position -= new Vector3(0, 0.01f, 0);

        if (Input.GetKey(KeyCode.LeftArrow) && transform.localPosition.x > -9.59f)
            transform.position -= new Vector3(0.01f, 0, 0);

        if (Input.GetKey(KeyCode.RightArrow) && transform.localPosition.x < 9.59f)
            transform.position += new Vector3(0.01f, 0, 0);

        yield return new WaitForEndOfFrame();
        StartCoroutine(Move());
    }
}
