using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : Singleton<EventManager>
{
    public void HPReduction(GameObject obj, int damage)
    {
        Slider slider = obj.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Slider>();
        slider.value -= damage;
        
        if (slider.value <= 0)
        {
            obj.SetActive(false);

            if (obj.name.Equals("Bat"))
                GameManager.Instance.catchCount++;
            else
            {
                Singleton.Instance.player.SetActive(false);
                Singleton.Instance.enemy.SetActive(false);
                Singleton.Instance.rock.SetActive(false);
                Singleton.Instance.panelGameOver.SetActive(true);
                GameManager.Instance.isInit = false;

                for(int i = 1; i < Singleton.Instance.player.transform.childCount; i++)
                    Destroy(Singleton.Instance.player.transform.GetChild(i).gameObject);

                for (int i = GameManager.Instance.enemyCount; i < Singleton.Instance.enemy.transform.childCount; i++)
                    Destroy(Singleton.Instance.enemy.transform.GetChild(i).gameObject);

                StartCoroutine(Restart());
            }
        }
    }

    public IEnumerator EXPUp()
    {
        Slider slider = Singleton.Instance.expBar.GetComponent<Slider>();
        float exp = slider.value;
        int index = 0;

        while (slider.value < exp + 50)
        {
            slider.value += 0.1f;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.5f);

        if (exp == 50)
        {
            slider.value = 0;

            Singleton.Instance.character.GetComponent<SpriteRenderer>().enabled = false;
            Singleton.Instance.character.transform.GetChild(0).gameObject.SetActive(false);

            for (int i = 0; i < Singleton.Instance.rock.transform.childCount; i++)
                Singleton.Instance.rock.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;

            Singleton.Instance.panelFrame.SetActive(true);

            while (true)
            {
                if (Input.GetKey(KeyCode.LeftArrow) && index != 0)
                    index--;

                if (Input.GetKey(KeyCode.RightArrow) && index != 2)
                    index++;

                for(int i = 0; i < Singleton.Instance.panelFrame.transform.childCount; i++)
                {
                    if(i == index)
                        Singleton.Instance.panelFrame.transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.yellow;
                    else
                        Singleton.Instance.panelFrame.transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.white;
                }

                if (Input.GetKey(KeyCode.Return))
                    break;

                yield return new WaitForSeconds(0.1f);
            }

            switch(index)
            {
                case 0:
                    {
                        GameManager.Instance.playerDamage += 25;
                        break;
                    }
                case 1:
                    {
                        GameManager.Instance.enemyDamage -= 5;
                        break;
                    }
                case 2:
                    {
                        GameManager.Instance.playerAttackTime -= 0.2f;
                        break;
                    }
            }

            Singleton.Instance.character.GetComponent<SpriteRenderer>().enabled = true;
            Singleton.Instance.character.transform.GetChild(0).gameObject.SetActive(true);

            for (int i = 0; i < Singleton.Instance.rock.transform.childCount; i++)
                Singleton.Instance.rock.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;

            Singleton.Instance.panelFrame.SetActive(false);
        }
        else
            slider.value = exp + 50;

        Singleton.Instance.door.SetActive(true);
        GameManager.Instance.isInit = false;
    }

    private IEnumerator Restart()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.R))
            {
                GameManager.Instance.gameObject.GetComponent<GameManager>().enabled = false;
                GameManager.Instance.gameObject.GetComponent<GameManager>().enabled = true;
                GameManager.Instance.isMove = false;

                yield return new WaitForEndOfFrame();
                Singleton.Instance.player.SetActive(true);

                Singleton.Instance.character.SetActive(true);
                Singleton.Instance.character.GetComponent<Player>().enabled = true;
                Singleton.Instance.character.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Slider>().value = 100;

                Singleton.Instance.character.GetComponent<SpriteRenderer>().enabled = false;
                Singleton.Instance.character.transform.GetChild(0).gameObject.SetActive(false);

                Singleton.Instance.enemy.SetActive(true);
                Singleton.Instance.enemy.GetComponent<Enemy>().enabled = true;

                for (int i = 0; i < GameManager.Instance.enemyCount; i++)
                {
                    Singleton.Instance.enemy.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
                    Singleton.Instance.enemy.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
                }

                while (!GameManager.Instance.isInit)
                    yield return new WaitForEndOfFrame();

                Singleton.Instance.character.GetComponent<SpriteRenderer>().enabled = true;
                Singleton.Instance.character.transform.GetChild(0).gameObject.SetActive(true);

                for (int i = 0; i < GameManager.Instance.enemyCount; i++)
                {
                    Singleton.Instance.enemy.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
                    Singleton.Instance.enemy.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                }

                Singleton.Instance.rock.SetActive(true);
                Singleton.Instance.panelGameOver.SetActive(false);

                break;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
