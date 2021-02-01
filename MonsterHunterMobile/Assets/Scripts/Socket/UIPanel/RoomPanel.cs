using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using SocketGameProtocol;


public class RoomPanel : BasePanel
{
    public Button back,start;

    public Transform playerMessageTF;

    public GameObject playerMessagePrefab;

    public Text roomName;



    private void Start()
    {
        back.onClick.AddListener(OnBackClick);
        //send.onClick.AddListener(OnSendClick);
        start.onClick.AddListener(OnStartClick);
    }

    private void OnBackClick()
    {
        RequestCenter.GetInstance().ExitRoom();
    }


    private void OnStartClick()
    {
        RequestCenter.GetInstance().StartGame();
    }

    /// <summary>
    /// 刷新玩家列表
    /// </summary>
    /// <param name="pack"></param>
    public void UpdatePlayerList(MainPack pack)
    {
        //for (int i = 0; i < playerMessageTF.childCount; i++)
        //{
        //    Destroy(playerMessageTF.GetChild(i).gameObject);
        //}

        //foreach (PlayerPack player in pack.PlayerPack)
        //{
        //    if (PlayerPrefs.GetString("username").Equals(player.PlayerName))
        //    {
        //        PlayerPrefs.SetString("id", pack.PlayerPack.IndexOf(player).ToString());
        //    }
        //    PlayerBox playerMessage = Instantiate(playerMessagePrefab, Vector3.zero, Quaternion.identity).GetComponent<PlayerBox>();
        //    playerMessage.gameObject.transform.SetParent(playerMessageTF);
        //    playerMessage.playerNameStr = player.PlayerName;
        //    //Useritem useritem = Instantiate(UserItemobj, Vector3.zero, Quaternion.identity).GetComponent<Useritem>();
        //    //useritem.gameObject.transform.SetParent(content);
        //    //useritem.SetPlayerInFo(player.Playername);
        //}
    }

    public void ExitRoomResponse()
    {
        UIManager.Instance.PopPanel(PanelType.normalPanel);
    }



    public void StartGameResponse(MainPack pack)
    {
        switch (pack.ReturnCode)
        {
            case ReturnCode.Fail:
                UIManager.Instance.ShowMessage("开始游戏失败！您不是房主");
                start.interactable = false;
                break;
            case ReturnCode.Succeed:
                UIManager.Instance.ShowMessage("游戏已启动！");
                start.interactable = false;
                break;
        }
    }

    public void GameStarting(MainPack packs)
    {
        GamePanel gamePanel = UIManager.Instance.PushPanel(PanelName.Game,PanelType.normalPanel).GetComponent<GamePanel>();
        //gamePanel.UpdateList(packs);
    }


    public void SetRoomInfo(string title, int curnum, int maxnum, RoomStatus status)
    {
        //this.roomName.text = title;
        //this..text = curnum + "/" + maxnum;
        //switch (status)
        //{
        //    case 0:
        //        this.status.text = "等待加入";
        //        break;
        //    case 1:
        //        this.statc.text = "房间已满人";
        //        break;
        //    case 2:
        //        this.statc.text = "游戏中";
        //        break;
        //}
    }


    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("进入房间");
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
        //chattext.text = "";
        start.interactable = true;
        Debug.Log("enterroom");
    }

    private void Exit()
    {
        gameObject.SetActive(false);
    }

    //private void OnSendClick()
    //{
    //    if (inputtext.text == "")
    //    {
    //        UIManager.Instance.ShowMessage("发送内容不能为空！");
    //        return; ;
    //    }
    //    chatRequest.SendRequest(inputtext.text);
    //    chattext.text += "我：" + inputtext.text + "\n";
    //    inputtext.text = "";
    //}
    //public void ChatResponse(string str)
    //{
    //    chattext.text += str + "\n";
    //}
}
