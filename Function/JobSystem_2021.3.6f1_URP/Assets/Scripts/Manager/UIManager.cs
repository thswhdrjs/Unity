using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private void Start()
    {
        AddOnClickEvent();
    }

    // ���� ���� ��ư Ŭ�� ��
    public void ClickedGameExit()
    {
        Application.Quit();
    }

    // obj�� Button�� OnClick�� action Event �߰�
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