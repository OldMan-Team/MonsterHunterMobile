using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SocketGameProtocol;
using System.Net.Sockets;
using Assets.Scripts.Socket.Tool;
using UnityEngine;
using System.Net;

public class ClientManager : SingletonMono<ClientManager>
{
    private Socket socket;
    private Message message;
    private Thread aucThread;
    private const string ip = "127.0.0.1";

    //玩家名
    [SerializeField]
    public string UserName
    {
        set;
        get;
    }

    public string UserID
    {
        set;
        get;
    }


    public  void Start()
    {
        message = new Message();
        
        //UserID = ((IPEndPoint)socket.LocalEndPoint).Address.ToString();
        //InitSocket();

        //InitUDP();
    }

    public  void OnDestroy()
    {
        message = null;
        CloseSocket();
        if (aucThread != null)
        {
            aucThread.Abort();
            aucThread = null;
        }

    }
    /// <summary>
    /// 初始化客户端
    /// </summary>
    public void InitClient()
    {
        InitSocket();
        InitUDP();
        //UserID = ((IPEndPoint)socket.LocalEndPoint).Address.ToString();

    }

    public void CloseClient()
    {
        CloseSocket();
    }

    /// <summary>
    /// 初始化socket
    /// </summary>
    private bool InitSocket()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            socket.Connect(ip, 6666);
            //连接成功
            StartReceive();
            UIManager.Instance.ShowMessage("Socket连接成功！");
            Debug.Log("Socket连接成功！");
            return true;
        }
        catch (Exception e)
        {
            //连接出错
            Debug.LogWarning(e);
            UIManager.Instance.ShowMessage("Socket连接失败！");
            return false;
        }
    }

    /// <summary>
    /// 关闭socket
    /// </summary>
    private void CloseSocket()
    {
        if (socket.Connected && socket != null)
        {
            socket.Close();
        }
    }

    private void StartReceive()
    {
        socket.BeginReceive(message.Buffer, message.StartIndex, message.Remsize, SocketFlags.None, ReceiveCallback, null);
    }

    private void ReceiveCallback(IAsyncResult iar)
    {
        try
        {
            if (socket == null || socket.Connected == false) return;
            int len = socket.EndReceive(iar);
            if (len == 0)
            {
                Debug.Log("数据为0");
                CloseSocket();
                return;
            }

            message.ReadBuffer(len, HandleResponse);
            StartReceive();
        }
        catch (Exception e)
        {
           Debug.LogWarning(e);
        }
    }

    private void HandleResponse(MainPack pack)
    {
        RequestCenter.GetInstance().HandleResponse(pack);
        //Debug.Log("client处理");
    }

    public void Send(MainPack pack)
    {
        if (socket.Connected == false || socket == null) return;
        socket.Send(Message.PackData(pack));
    }


    //UDP协议

    private Socket udpClient;
    private IPEndPoint ipEndPoint;
    private EndPoint EPoint;
    private Byte[] buffer = new Byte[1024];

    private void InitUDP()
    {
        udpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), 6667);
        EPoint = ipEndPoint;
        try
        {
            udpClient.Connect(EPoint);
        }
        catch
        {
            Debug.Log("UDP连接失败！");
            return;
        }

        Loom.RunAsync(() =>
        {
            aucThread = new Thread(ReceiveMsg);
            aucThread.Start();
        }
        );



    }

    private void ReceiveMsg()
    {
        Debug.Log("UDP开始接收");
        while (true)
        {
            int len = udpClient.ReceiveFrom(buffer, ref EPoint);
            MainPack pack = (MainPack)MainPack.Descriptor.Parser.ParseFrom(buffer, 0, len);
            // if (pack.Actioncode == ActionCode.UpPos)
            // {
            //     if (pack.Playerpack[0].Playername == "1234")
            //     {
            //         face.isRec = true;
            //     }
            // }

            //Debug.Log(Encoding.UTF8.GetString(buffer,0,len));
            //Debug.Log("接收数据："+pack.Actioncode.ToString()+pack.User);
            Loom.QueueOnMainThread((param) =>
            {
                HandleResponse(pack);
            }, null);

            //face.isRec = true;
        }
    }



    public void SendTo(MainPack pack)
    {
        Byte[] sendBuff = Message.PackDataUDP(pack);
        udpClient.Send(sendBuff, sendBuff.Length, SocketFlags.None);
    }



}