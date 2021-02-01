using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : BasePanel
{
    public Text text;
    public Button backBtn;
    string msg = null;

    //public override void OnEnter()
    //{
    //    text.CrossFadeAlpha(0, 0.1f, false);
    //    UIManager.Instance.SetMessagePanel(this);
    //    backBtn.onClick.AddListener(OnBackClick);
    //}

    //private void Update()
    //{
    //    if (msg != null)
    //    {
    //        ShowText(msg);
    //        msg = null;
    //    }
    //}

    //public void ShowMessage(string str, bool sync = false)
    //{
    //    if (sync)
    //    {
    //        //异步显示
    //        msg = str;
    //    }
    //    else
    //    {
    //        ShowText(str);
    //    }
    //    text.text = str;
    //    Enter();
    //}

    //private void ShowText(string str)
    //{
    //    text.text = str;
    //    text.CrossFadeAlpha(1, 0.1f, false);
    //    Invoke("HideText", 1);
    //}

    //private void HideText()
    //{
    //    text.CrossFadeAlpha(0, 1f, false);
    //}

    public void ShowMessage(string str, bool sync = false)
    {
        text.text = str;
        Enter();
    }

    public void OnBackClick()
    {
        UIManager.Instance.PopPanel(PanelType.noticePanel);
    }

    public override void OnEnter()
    {
        UIManager.Instance.SetMessagePanel(this);
        backBtn.onClick.AddListener(OnBackClick);
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