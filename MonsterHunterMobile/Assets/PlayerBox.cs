using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBox : MonoBehaviour
{
    public Button addPlayerBtn; 
    public Image occupation;                                        //职业图片
    public Image frame;                                             //玩家框
    public Text playerNameText;                                     //玩家名字文本

    public GameObject ready, unReady;                               //准备好和没准备好
    public List<GameObject> playerBoxList = new List<GameObject>(); //游戏框
    public List<Sprite> occupationSp=new List<Sprite>();            //职业图片
    public List<Sprite> frameSp=new List<Sprite>();                 //边框图片，0为房主，1为其他玩家


    public bool isReady;                                            //是否已经准备
    public string playerNameStr;                                    //玩家名字

    void Start()
    {
        addPlayerBtn.onClick.AddListener(AddPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPlayer()
    {

    }


    public void PlayerMessageInit(bool isHomeowner,Occupation occupation,string playerName)
    {
        if(occupation==Occupation.Archer)
        {
            this.occupation.sprite = occupationSp[0];
        }
        if (isHomeowner)
        {
            frame.sprite = frameSp[0];
        }
        else
        {
            frame.sprite = frameSp[1];
        }
        this.playerNameText.text = playerName;
        unReady.SetActive(true);
    }


    public void SetReady(bool isReady)
    {
        this.isReady = isReady;
        if (isReady)
        {
            unReady.SetActive(false);
            ready.SetActive(true);
        }
        else
        {
            ready.SetActive(false);
            unReady.SetActive(true);
        }
    }
}
