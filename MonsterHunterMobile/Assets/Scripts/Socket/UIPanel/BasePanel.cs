using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PanelName
{
    Message,
    Start,
    Input,
    RoomList,
    Room,
    Game,
    GameOver
}

public class BasePanel : MonoBehaviour
{
    //protected UIManager uiMgr;

    //public UIManager SetUIMgr
    //{
    //    set
    //    {
    //        uiMgr = value;
    //    }
    //}

    public virtual void OnEnter()
    {

    }

    public virtual void OnPause()
    {

    }

    public virtual void OnRecovery()
    {

    }

    public virtual void OnExit()
    {

    }
}
