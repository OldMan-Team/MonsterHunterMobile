using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketGameProtocol;
using System.Reflection;
using Assets.Scripts.Socket.Controller;
using System.Diagnostics;
using UnityEngine;

class ControllerManager
{
    private Dictionary<RequestCode, BaseController> controlDict = new Dictionary<RequestCode, BaseController>();

    private Server server;
    public ControllerManager(Server server)
    {
        this.server = server;


        ConnectController roomController = new ConnectController();
        controlDict.Add(roomController.GetRequestCode, roomController);
        GameController gameController = new GameController();
        controlDict.Add(gameController.GetRequestCode, gameController);
    }


    public void HandleRequest(MainPack pack, Client client, bool isUDP = false)
    {
        if (controlDict.TryGetValue(pack.RequestCode, out BaseController controller))
        {
            string metname = pack.ActionCode.ToString();
            MethodInfo method = controller.GetType().GetMethod(metname);
            if (method == null)
            {
                Console.WriteLine("没有找到对应的处理方法");
                return;
            }
            UnityEngine.Debug.Log("处理方法"+metname);
            object[] obj;
            if (isUDP)//UDP
            {
                obj = new object[] { client, pack };
                method.Invoke(controller, obj);
            }
            else//TCP
            {
                obj = new object[] { server, client, pack };
                object ret = method.Invoke(controller, obj);
                if (ret != null)
                {
                    client.Send(ret as MainPack);
                    Console.WriteLine("发送数据：");
                }
            }

        }
        else
        {
            Console.WriteLine("没有找到对应的controller处理");
        }
    }
}
