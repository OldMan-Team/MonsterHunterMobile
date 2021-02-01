using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMessage : MonoBehaviour
{
    public Image occupation;                                        //职业图片
    public Image frame;                                             //玩家框
    public Text playerNameText;                                     //玩家名字文本
    public GameObject ready, unReady;                               //准备好和没准备好


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerMessageInit(bool isReady, Sprite occupationImg, Sprite frameImg,string playerName)
    {
        this.occupation.sprite = occupationImg;
        frame.sprite = frameImg;
        this.playerNameText.text = playerName;
        if(isReady)
        {
            unReady.SetActive(false);
            ready.SetActive(true);
        }
        else
        {
            unReady.SetActive(true);
            ready.SetActive(false);
        }    

    }
}
