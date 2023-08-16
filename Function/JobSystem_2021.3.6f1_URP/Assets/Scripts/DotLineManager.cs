using System;
using UnityEngine;

public enum Pivot
{
    Top,
    Bottom,
    Left,
    Right
}

[Serializable]
public class DotLine
{
    public RectTransform startRect;
    public Pivot startPivot;

    [Space, Space]

    public RectTransform endRect;
    public Pivot endPivot;
}

public class DotLineManager : MonoBehaviour
{
    [SerializeField] private DotLine[] dotLine;

    private void Start()
    {
        for (int i = 0; i < dotLine.Length; i++)
        {
            Vector2 startPos = GetPosition(dotLine[i].startRect, dotLine[i].startPivot);
            Vector2 endPos = GetPosition(dotLine[i].endRect, dotLine[i].endPivot);

            if(dotLine[i].startPivot == Pivot.Bottom && dotLine[i].endPivot == Pivot.Top || dotLine[i].startPivot == Pivot.Right && dotLine[i].endPivot == Pivot.Left || dotLine[i].startPivot == Pivot.Left && dotLine[i].endPivot == Pivot.Right)
                DottedLine.Instance.DrawDottedLine(startPos, endPos);
            else
            {
                Vector2 halfPos = halfPos = new Vector2(dotLine[i].endRect.anchoredPosition.x, dotLine[i].startRect.anchoredPosition.y);

                if (dotLine[i].endPivot == Pivot.Left)
                    halfPos = new Vector2(dotLine[i].startRect.anchoredPosition.x, dotLine[i].endRect.anchoredPosition.y);

                DottedLine.Instance.DrawDottedLine(startPos, halfPos);
                DottedLine.Instance.DrawDottedLine(halfPos, endPos);
            }
        }
    }

    private Vector2 GetPosition(RectTransform rect, Pivot pivot)
    {
        Vector2 pos = Vector2.zero;

        switch (pivot)
        {
            case Pivot.Top:
                {
                    pos = rect.anchoredPosition + new Vector2(0, rect.sizeDelta.y / 2 + DottedLine.Instance.Size * 100);
                    break;
                }
            case Pivot.Bottom:
                {
                    pos = rect.anchoredPosition - new Vector2(0, rect.sizeDelta.y / 2 + DottedLine.Instance.Size * 100);
                    break;
                }
            case Pivot.Left:
                {
                    pos = rect.anchoredPosition - new Vector2(rect.sizeDelta.x / 2 + DottedLine.Instance.Size * 100, 0);
                    break;
                }
            case Pivot.Right:
                {
                    pos = rect.anchoredPosition + new Vector2(rect.sizeDelta.x / 2 + DottedLine.Instance.Size * 100, 0);
                    break;
                }
        }

        return pos;
    }
}
