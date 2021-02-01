using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SocketGameProtocol;
using System.Reflection;

public class RequestCenter : BaseManager<RequestCenter>
{

    private float recTime = 0;
    public bool isRec = false;
    private void Update()
    {
        if (isRec)
        {
            Debug.Log("接收消息时间间隔： " + (Time.time - recTime));
            recTime = Time.time;
            isRec = false;
        }
    }

    public void HandleResponse(MainPack pack)
    {
        string metname = pack.ActionCode.ToString()+"Response";
        MethodInfo method = this.GetType().GetMethod(metname);
        if (method == null)
        {
            Console.WriteLine("没有找到对应的处理方法");
            return;
        }
        method.Invoke(this, new object[] { pack });

    }

    public void Send(MainPack pack)
    {
        ClientManager.Instance.Send(pack);
    }

    public void SendTo(MainPack pack)
    {
        pack.User = ClientManager.Instance.UserName;
        ClientManager.Instance.SendTo(pack);
    }
    public void GameExit()
    {
        //PlayerManager.Instance.GameExit();
        //UIManager.Instance.PopPanel();
        //UIManager.Instance.PopPanel();
    }

    public void PanelReflect(PanelName panelType,MainPack pack,String methodName)
    {
        //查找所需panel
        if (UIManager.Instance.panelDict.TryGetValue(panelType, out BasePanel panel))
        {
            //获取方法
            MethodInfo method = panel.GetType().GetMethod(methodName);
            if (method == null)
            {
                Debug.Log("没有找到对应的处理方法");
                return;
            }
            Debug.Log("找到对应的处理方法"+method);
            //调用方法
            method.Invoke(panel, new object[] { pack });
        }
    }


    //请求实现

    //---------------------------------Connect-------------------------------------
    ///房间：创建，查找，加入，退出

    public void CreateRoom(string roomname, int maxnum)
    {
        GameManager.Instance.InitServer();
        GameManager.Instance.InitClient();
        MainPack pack = new MainPack();
        pack.RequestCode = RequestCode.Connect;
        pack.ActionCode = ActionCode.CreateRoom;
        RoomPack room = new RoomPack();
        room.RoomName = roomname;
        room.MaxNum = maxnum;
        pack.RoomPack.Add(room);
        Send(pack);
        Debug.Log("CreateRoomRequest has send");
    }
    public void CreateRoomResponse(MainPack pack)
    {
        PanelReflect(PanelName.RoomList, pack, "CreateRoomResponse");
    }
    
    public void FindRoom()
    {
        GameManager.Instance.InitClient();
        MainPack pack = new MainPack();
        pack.RequestCode = RequestCode.Connect;
        pack.ActionCode = ActionCode.FindRoom;
        pack.Str = "r";
        Send(pack);
    }

    public void FindRoomResponse(MainPack pack)
    {
        PanelReflect(PanelName.RoomList, pack, "FindRoomResponse");
    }

    public void JoinRoom(string roomName)
    {
        MainPack pack = new MainPack();
        pack.RequestCode =RequestCode.Connect;
        pack.ActionCode = ActionCode.JoinRoom;
        pack.Str = roomName;
        Send(pack);

    }

    public void JoinRoomResponse(MainPack pack)
    {
        PanelReflect(PanelName.RoomList, pack, "JoinRoomResponse");
    }

    public void ExitRoom()
    {
        MainPack pack = new MainPack();
        pack.RequestCode = RequestCode.Connect;
        pack.ActionCode = ActionCode.ExitRoom;
        pack.Str = "r";  //防止没有字符串，变成空信息
        Send(pack);
    }

    public void ExitRoomResponse(MainPack pack)
    {
        
        PanelReflect(PanelName.Room, pack, "ExitRoomResponse");
    }
    //----------Game---------：开始，退出，服务端发来的开始
    public void StartGame()
    {
        MainPack pack = new MainPack();
        pack.RequestCode = RequestCode.Connect;
        pack.ActionCode = ActionCode.StartGame;
        pack.Str = "r";
        Send(pack);
    }

    public void StartGameResponse(MainPack pack)
    {
        PanelReflect(PanelName.Room, pack, "StartGameResponse");
    }

    public void ExitGame()
    {
        MainPack pack = new MainPack();
        pack.RequestCode = RequestCode.Connect;
        pack.ActionCode = ActionCode.ExitGame;
        pack.Str = "r";
        Send(pack);
    }

    public void ExitGameResponse(MainPack pack)
    {
        GameExit();
    }

    public void StartingResponse(MainPack pack)
    {
        Debug.Log("游戏正式开始！");
        //PlayerManager.Instance.addPlayer(pack);
        PanelReflect(PanelName.Room, pack, "GameStarting");
    }

    public void UpCharacterListResponse(MainPack pack)
    {
        PanelReflect(PanelName.Game, pack, "UpdateList");
        //PlayerManager.Instance.removePlayer(pack.Str);
    }
}
