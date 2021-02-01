using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using SocketGameProtocol;
using System.Net.Sockets;

public class GameManager : SingletonMono<GameManager>
{
    private Server server;
    private Thread serverThread;    //服务器线程
    private bool isServer=false;





    //初始化客户端
    public void InitClient()
    {
        ClientManager.Instance.InitClient();
        Debug.Log("客户端已启动...");
        //InitUDP();
    }

    public void CloseClient()
    {
        ClientManager.Instance.CloseClient();
    }


    //初始化服务端
    public void InitServer()
    {
        isServer = true;
        serverThread = new Thread(ServerThread);
        serverThread.Start();
        Debug.Log("服务端已启动...");
    }

    public void ServerThread()
    {
        server = new Server(6666);
    }

    public void CloseServer()
    {
        if (serverThread != null)
        {
            serverThread.Abort();
            serverThread = null;
        }
    }
    /// <summary>
    /// 关闭网络联机
    /// </summary>
    public void CloseSocket()
    {
        CloseClient();
        if (isServer)
        {
            CloseServer();
        }
    }

    public void GameOver()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
         Application.Quit();
#endif

    }
}
