using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    public Button startBtn;
    public Button exitBtn;

    private void Start()
    {
        startBtn.onClick.AddListener(StartButtonClick);
        exitBtn.onClick.AddListener(ExitButtonClick);
    }

    private void StartButtonClick()
    {
        UIManager.Instance.PushPanel(PanelName.RoomList,PanelType.normalPanel);
    }

    private void ExitButtonClick()
    {
        GameManager.Instance.GameOver();
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
        //Exit();
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

