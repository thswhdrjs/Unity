using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private void Start()
    {
        AddOnClickEvent();
    }

    // 게임 종료 버튼 클릭 시
    public void ClickedGameExit()
    {
        Application.Quit();
    }

    // obj의 Button의 OnClick에 action Event 추가
    public void ButtonAddOnClick(GameObject obj, UnityAction action)
    {
        if (obj.GetComponent<Button>() == null)
            return;

        obj.GetComponent<Button>().onClick.AddListener(action);
    }

    //Add Button OnClick Event
    private void AddOnClickEvent()
    {
        // EX
        // ButtonAddOnClick(Singleton.Instance.canvas, ClickedGameExit);
    }
}