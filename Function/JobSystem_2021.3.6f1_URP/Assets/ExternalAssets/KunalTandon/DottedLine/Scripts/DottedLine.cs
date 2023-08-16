using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DottedLine : Singleton<DottedLine>
{
    [SerializeField]
    private Sprite Dot;

    [Range(0.00001f, 2f)]
    public float Size;
    [Range(0.0001f, 100f)]
    public float Delta;

    List<Vector2> positions = new List<Vector2>();
    List<GameObject> dots = new List<GameObject>();

    //private void FixedUpdate()
    //{
    //    if (positions.Count > 0)
    //    {
    //        DestroyAllDots();
    //        positions.Clear();
    //    }
    //}

    //private void DestroyAllDots()
    //{
    //    foreach (var dot in dots)
    //        Destroy(dot);

    //    dots.Clear();
    //}

    private GameObject GetOneDot()
    {
        GameObject gameObject = new GameObject("Dot");
        gameObject.transform.localScale = Vector3.one * Size;
        gameObject.transform.parent = transform;

        var sr = gameObject.AddComponent<Image>();
        sr.sprite = Dot;

        return gameObject;
    }

    public void DrawDottedLine(Vector2 start, Vector2 end)
    {
        //DestroyAllDots();

        Vector2 point = start;
        Vector2 direction = (end - start).normalized;

        while ((end - start).magnitude > (point - start).magnitude)
        {
            positions.Add(point);
            point += (direction * Delta);
        }

        Render();
    }

    private void Render()
    {
        foreach (var position in positions)
        {
            var g = GetOneDot();
            g.transform.localRotation = Quaternion.identity;
            g.GetComponent<RectTransform>().anchoredPosition = position;
            dots.Add(g);
        }
    }
}