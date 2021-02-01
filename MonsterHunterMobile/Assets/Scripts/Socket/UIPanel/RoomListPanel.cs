using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using SocketGameProtocol;

public class RoomListPanel : BasePanel
{
    public Button backBtn, findBtn, createBtn;
    public InputField roomName;
    public int num;

    public Transform roomListTransform;
    public GameObject roomitem;


    private void Start()
    {
        backBtn.onClick.AddListener(OnBackClick);
        findBtn.onClick.AddListener(OnFindClick);
        createBtn.onClick.AddListener(OnCreateClick);
    }

    /// <summary>
    /// 注销登录
    /// </summary>
    private void OnBackClick()
    {
        UIManager.Instance.PopPanel(PanelType.normalPanel);
        GameManager.Instance.CloseSocket();
    }
    /// <summary>
    /// 查询房间
    /// </summary>
    private void OnFindClick()
    {
        RequestCenter.GetInstance().FindRoom();
    }
    /// <summary>
    /// 创建房间
    /// </summary>
    private void OnCreateClick()
    {
        if (roomName.text == "")
        {
            UIManager.Instance.ShowMessage("房间名不能为空！");
            return;
        }
        RequestCenter.GetInstance().CreateRoom(roomName.text, num);
    }

    public void CreateRoomResponse(MainPack pack)
    {
        Debug.Log("CreateRoomResponse"+ pack.ReturnCode);
        switch (pack.ReturnCode)
        {
            case ReturnCode.Succeed:
                
                UIManager.Instance.ShowMessage("创建成功！");
                Debug.Log("房间创建成功0！");
                RoomPanel roomPanel = UIManager.Instance.PushPanel(PanelName.Room,PanelType.normalPanel).GetComponent<RoomPanel>();
                roomPanel.UpdatePlayerList(pack);
                Debug.Log("房间创建成功1！");
                break;
            case ReturnCode.Fail:
                Debug.Log("房间创建失败！");
                UIManager.Instance.ShowMessage("创建失败！");

                break;
            default:
                Debug.Log("def");
                break;
        }
    }

    public void FindRoomResponse(MainPack pack)
    {
        switch (pack.ReturnCode)
        {
            case ReturnCode.Succeed:
                UIManager.Instance.ShowMessage("查询成功！一共有" + pack.RoomPack.Count + "个房间");
                break;
            case ReturnCode.Fail:
                UIManager.Instance.ShowMessage("查询出错！");
                break;
            case ReturnCode.ReturnNone:
                UIManager.Instance.ShowMessage("当前没有房间！");
                break;
            default:
                Debug.Log("def");
                break;
        }
        UpdateRoomList(pack);
    }

    private void UpdateRoomList(MainPack pack)
    {
        //清空房间列表
        for (int i = 0; i < roomListTransform.childCount; i++)
        {
            Debug.Log("清除" + i);
            Destroy(roomListTransform.GetChild(i).gameObject);
        }

        if (pack.RoomPack.Count == 0) return;
        foreach (RoomPack room in pack.RoomPack)
        {
            RoomItem item = Instantiate(roomitem, Vector3.zero, Quaternion.identity).GetComponent<RoomItem>();
            item.roomListPanel = this;
            item.gameObject.transform.SetParent(roomListTransform);
            item.SetRoomInfo(room.RoomName, room.CurNum, room.MaxNum, room.RoomStatus);
        }

    }


    public void JoinRoomResponse(MainPack pack)
    {
        switch (pack.ReturnCode)
        {
            case ReturnCode.Succeed:
                UIManager.Instance.ShowMessage("加入房间成功！");
                RoomPanel roomPanel = UIManager.Instance.PushPanel(PanelName.Room).GetComponent<RoomPanel>();
                roomPanel.UpdatePlayerList(pack);
                break;
            case ReturnCode.Fail:
                UIManager.Instance.ShowMessage("加入房间失败！");
                break;
            default:
                UIManager.Instance.ShowMessage("房间不存在！");
                break;
        }
    }

    public void JoinRoom(string roomName)
    {
        RequestCenter.GetInstance().JoinRoom(roomName);
    }




    public override void OnEnter()
    {
        base.OnEnter();
        Enter();
    }

    public override void OnExit()
    {
        base.OnExit();
        Exit();
    }

    public override void OnPause()
    {
        base.OnPause();
        Exit();
    }

    public override void OnRecovery()
    {
        base.OnRecovery();
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
