using UnityEngine;

public class Singleton
{
    private static Singleton instance;

    public static Singleton Instance
    {
        get
        {
            if (null == instance)
                instance = new Singleton();

            return instance;
        }
    }

    public GameObject env, rock, limit, door;
    public GameObject player, character, enemy;
    public GameObject points;
    public GameObject canvas, expBar, panelFrame, panelGameOver;
    public GameObject canvasFade, panelFade;

    public Singleton()
    {
        env = GameObject.Find("Env");
        rock = env.transform.GetChild(1).gameObject;
        limit = env.transform.GetChild(2).gameObject;
        door = env.transform.GetChild(3).gameObject;

        player = GameObject.Find("Player");
        character = player.transform.GetChild(0).gameObject;
        enemy = GameObject.Find("Enemy");

        points = GameObject.Find("Points");

        canvas = GameObject.Find("Canvas_UI");
        expBar = canvas.transform.GetChild(1).GetChild(0).gameObject;
        panelFrame = canvas.transform.GetChild(2).gameObject;
        panelGameOver = canvas.transform.GetChild(3).gameObject;

        canvasFade = GameObject.Find("Canvas_Fade");
        panelFade = canvasFade.transform.GetChild(0).gameObject;
    }
}