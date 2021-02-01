using SocketGameProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public Button join;
    public Text text;

    public RoomListPanel roomListPanel;
    // Start is called before the first frame update
    void Start()
    {
        join.onClick.AddListener(OnJoinClick);
    }


    private void OnJoinClick()
    {
        roomListPanel.JoinRoom(text.text);
    }

    public void SetRoomInfo(string title, float curnum, float maxnum, RoomStatus status)
    {
        text.text = title;
        text.text += curnum + "/" + maxnum;
        switch (status)
        {
            case RoomStatus.Vacancy:
                text.text += "\n等待加入";
                break;
            case RoomStatus.Enough:
                text.text += "\n房间已满人";
                break;
            case RoomStatus.Playing:
                text.text += "\n游戏中";
                break;
        }
    }

}
