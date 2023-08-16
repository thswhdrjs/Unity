using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private GameObject target;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "Character":
                {
                    if (transform.parent.name != "Player")
                    {
                        EventManager.Instance.HPReduction(other.gameObject, GameManager.Instance.enemyDamage);
                        Destroy(gameObject);
                    }

                    break;
                }

            case "Bat":
                {
                    if (transform.parent.name != "Enemy")
                    {
                        EventManager.Instance.HPReduction(other.gameObject, GameManager.Instance.playerDamage);
                        Destroy(gameObject);
                    }

                    break;
                }

            default:
                {
                    if (other.gameObject.name != "Arrow(Clone)")
                        Destroy(gameObject);

                    break;
                }
        }
    }

    private void Start()
    {
        target = transform.parent.name.Equals("Enemy") ? Singleton.Instance.character : GetCloseEnemy();
        transform.LookAt(target.transform);
        transform.localEulerAngles = new Vector3(0, 0, transform.position.x > target.transform.position.x ? transform.localEulerAngles.x + 180 : -transform.localEulerAngles.x);
        
        Vector3 targetPos = GetExtensionPos(target.transform.position, transform.position);
        StartCoroutine(Shoot(targetPos));
    }

    private GameObject GetCloseEnemy()
    {
        int index = 0;
        float distance = 1000f;

        for(int i = 0; i < GameManager.Instance.enemyCount; i++)
        {
            if (!Singleton.Instance.enemy.transform.GetChild(i).gameObject.activeSelf)
                continue;

            float dis = Vector3.Distance(Singleton.Instance.character.transform.position, Singleton.Instance.enemy.transform.GetChild(i).position);

            if(distance > dis)
            {
                distance = dis;
                index = i;
            }
        }

        return Singleton.Instance.enemy.transform.GetChild(index).gameObject;
    }

    private Vector3 GetExtensionPos(Vector3 origin, Vector3 target)
    {
        return new Vector3(origin.x - (target.x - origin.x) * 5, origin.y - (target.y - origin.y) * 5, origin.z);
    }

    private IEnumerator Shoot(Vector3 tagetPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, tagetPos, 0.01f);
        yield return new WaitForEndOfFrame();
        StartCoroutine(Shoot(tagetPos));
    }
}
