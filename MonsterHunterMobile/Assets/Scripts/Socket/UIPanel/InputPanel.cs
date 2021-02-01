using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public class InputPanel:BasePanel
{
    public Button confirmBtn;
    public InputField inputText;
    public Text noticeText;

    public void ShowNotice(string str)
    {
        noticeText.text = str;
    }
    public void Start()
    {
        confirmBtn.onClick.AddListener(OnRenameClick);
    }

    public void OnRenameClick()
    {
        if (inputText.text == "")
        {
            UIManager.Instance.ShowMessage("玩家名字不能为空！");
            return;
        }
        ClientManager.Instance.UserName = inputText.text;
        UIManager.Instance.PopPanel(PanelType.normalPanel);
        UIManager.Instance.ShowMessage(inputText.text);
        Debug.Log("调用Message");

    }

    public override void OnEnter()
    {
        Enter();
    }

    public override void OnExit()
    {
        Exit();
    }

    public override void OnPause()
    {
        Exit();
    }

    public override void OnRecovery()
    {
        Enter();
    }

    private void Enter()
    {
        gameObject.SetActive(true);
    }

    private void Exit()
    {
        gameObject.SetActive(false);
    }
}
