using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketGameProtocol;
using System.Threading;
using Google.Protobuf.Collections;

class Room
{
    private RoomPack roomInfo;//房间信息
    private Server server;
    private List<Client> clientList = new List<Client>();//房间内所有的客户端,[0]为房主

    private float sdfos
    {
        get;
        set;
    }

    /// <summary>
    /// 返回房间信息
    /// </summary>
    public RoomPack GetRoomInFo
    {
        get
        {
            roomInfo.CurNum = clientList.Count;
            return roomInfo;
        }
    }

    public Room(Client client, RoomPack pack, Server server)
    {
        roomInfo = pack;
        clientList.Add(client);
        client.GetRoom = this;
        this.server = server;
    }

    //获取房间内玩家信息
    public RepeatedField<PlayerPack> GetPlayerInFo()
    {
        RepeatedField<PlayerPack> pack = new RepeatedField<PlayerPack>();
        foreach (Client c in clientList)
        {
            PlayerPack player = new PlayerPack();
            player.PlayerName = c.GetUserInFo.UserName;
            pack.Add(player);
        }
        return pack;
    }

    //TCP广播
    public void Broadcast(Client client, MainPack pack)
    {
        foreach (Client c in clientList)
        {
            if (c.Equals(client))
            {
                continue;
            }
            c.Send(pack);
        }
    }

    //UDP广播
    public void BroadcastTo(Client client, MainPack pack)
    {
        //Console.WriteLine("广播数据");
        foreach (Client c in clientList)
        {
            if (c.Equals(client))
            {
                continue;
            }
            c.SendTo(pack);
        }
    }


    //Damage
    //public void Damage(MainPack pack, Client cc)
    //{
    //    BulletHitPack bulletHitPack = pack.Bullethitpack;
    //    PosPack posPack = null;
    //    Client client = null;
    //    foreach (Client c in clientList)
    //    {
    //        if (c.GetUserInFo.UserName == bulletHitPack.Hituser)
    //        {
    //            posPack = c.GetUserInFo.Pos;
    //            client = c;
    //            break;
    //        }
    //    }

    //    double distance = Math.Sqrt(Math.Pow((bulletHitPack.PosX - posPack.PosX), 2) + Math.Pow((bulletHitPack.PosY - posPack.PosY), 2));

    //    Console.WriteLine(cc.GetUserInFo.UserName + " 击中 " + bulletHitPack.Hituser + " 距离 " + distance);

    //    if (distance < 0.7f)
    //    {
    //        //击中

    //        Broadcast(null, pack);
    //    }
    //}


    public void Join(Client client)
    {
        clientList.Add(client);
        if (clientList.Count >= roomInfo.MaxNum)
        {
            //满人了
            roomInfo.RoomStatus = RoomStatus.Enough;
        }
        client.GetRoom = this;
        MainPack pack = new MainPack();
        pack.ActionCode = ActionCode.UpPlayerList;
        foreach (PlayerPack player in GetPlayerInFo())
        {
            pack.PlayerPack.Add(player);
        }
        Broadcast(client, pack);
    }

    public void Exit(Server server, Client client)
    {
        MainPack pack = new MainPack();
        if (roomInfo.RoomStatus == RoomStatus.Playing)//游戏已经开始
        {
            ExitGame(client);
        }
        else//游戏未开始
        {
            if (client == clientList[0])
            {
                //房主离开
                client.GetRoom = null;
                pack.ActionCode = ActionCode.ExitRoom;
                Broadcast(client, pack);
                server.RemoveRoom(this);
                return;
            }
            clientList.Remove(client);
            roomInfo.RoomStatus = RoomStatus.RoomNone;
            client.GetRoom = null;
            pack.ActionCode = ActionCode.UpPlayerList;
            foreach (PlayerPack player in GetPlayerInFo())
            {
                pack.PlayerPack.Add(player);
            }
            Broadcast(client, pack);
        }


    }

    public ReturnCode StartGame(Client client)
    {
        if (client != clientList[0])
        {
            return ReturnCode.Fail;
        }
        roomInfo.RoomStatus = RoomStatus.Playing;
        Thread starttime = new Thread(Time);
        starttime.Start();
        Console.WriteLine("开始游戏");
        return ReturnCode.Succeed;
    }

    private void Time()
    {
        MainPack pack = new MainPack();
        //pack.ActionCode = ActionCode.Chat;
        pack.Str = "房主已启动游戏...";
        Broadcast(null, pack);
        Thread.Sleep(1000);
        for (int i = 5; i > 0; i--)
        {
            pack.Str = i.ToString();
            Broadcast(null, pack);
            Thread.Sleep(1000);
        }

        pack.ActionCode = ActionCode.Starting;


        foreach (var VARIABLE in clientList)
        {
            PlayerPack player = new PlayerPack();
            VARIABLE.GetUserInFo.HP = 100;
            player.PlayerName = VARIABLE.GetUserInFo.UserName;
            player.HP = VARIABLE.GetUserInFo.HP;
            pack.PlayerPack.Add(player);
        }
        Broadcast(null, pack);
    }

    public void ExitGame(Client client)
    {
        MainPack pack = new MainPack();
        if (client == clientList[0])
        {
            //房主退出
            pack.ActionCode = ActionCode.ExitGame;
            pack.Str = "r";
            Broadcast(client, pack);
            server.RemoveRoom(this);
            client.GetRoom = null;
        }
        else
        {
            //其他成员退出
            clientList.Remove(client);
            client.GetRoom = null;
            pack.ActionCode = ActionCode.UpCharacterList;
            foreach (var VARIABLE in clientList)
            {
                PlayerPack playerPack = new PlayerPack();
                playerPack.PlayerName = VARIABLE.GetUserInFo.UserName;
                playerPack.HP = VARIABLE.GetUserInFo.HP;
                pack.PlayerPack.Add(playerPack);
            }
            pack.Str = client.GetUserInFo.UserName;
            Broadcast(client, pack);
        }
    }
}