using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketGameProtocol;

namespace Assets.Scripts.Socket.Controller
{
    class ConnectController:BaseController
    {
        public ConnectController()
        {
            requestCode = RequestCode.Connect;
        }
        //Room
        public MainPack CreateRoom(Server server, Client client, MainPack pack)
        {
            return server.CreateRoom(client, pack);
        }

        public MainPack FindRoom(Server server, Client client, MainPack pack)
        {
            return server.FindRoom();
        }

        public MainPack JoinRoom(Server server, Client client, MainPack pack)
        {
            return server.JoinRoom(client, pack);
        }

        public MainPack Exit(Server server, Client client, MainPack pack)
        {
            return server.ExitRoom(client, pack);
        }

        public MainPack Chat(Server server, Client client, MainPack pack)
        {
            server.Chat(client, pack);
            return null;
        }

        public MainPack StartGame(Server server, Client client, MainPack pack)
        {
            pack.ReturnCode = client.GetRoom.StartGame(client);
            return pack;
        }

        //人物
        //游戏
    }
}
