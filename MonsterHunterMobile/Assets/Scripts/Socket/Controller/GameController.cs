using SocketGameProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Socket.Controller
{
    class GameController:BaseController
    {

        public GameController()
        {
            requestCode = RequestCode.Game;
        }

        //更新玩家位置
        public MainPack UpPos(Client client, MainPack pack)
        {
            client.GetRoom.BroadcastTo(client, pack);
            client.UpPos(pack);//更新位置信息
            return null;
        }

        //更新玩家操作
        public MainPack UpOperation(Client client,MainPack pack)
        {

            return null;
        }
    }
}
