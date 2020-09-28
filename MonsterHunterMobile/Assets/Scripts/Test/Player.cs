using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int EXE = 0;  //经验
    void Start()
    {
        EventCenter.GetInstance().AddEventListener<Monster>("MonsterDead", GetEXE);
        
    }
    //监听事件触发的委托事件
    public void GetEXE(Monster Info)
    {
        EXE++;
        Debug.Log("Player kill a "+Info.name);
    }
    //销毁时删除监听事件所触发的委托事件
    void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener<Monster>("MonsterDead", GetEXE);
    }
}
