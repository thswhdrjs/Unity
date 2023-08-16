using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static MonoBehaviour instance = null;

    public static T Instance { get { return (T)instance; } }

    protected virtual void Awake()
    {
        if (instance == null)
            instance = this;
        else if (!instance == this)
            DestroyImmediate(gameObject);
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
}

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
        set { instance = value; }
    }

    public GameObject env;

    public GameObject canvas;
    public GameObject loading, imageLoading;

    public Singleton()
    {
        env = GameObject.Find("Env");

        canvas = GameObject.Find("Canvas");
        loading = canvas.transform.GetChild(canvas.transform.childCount - 1).gameObject;
        imageLoading = loading.transform.GetChild(0).gameObject;
    }
}