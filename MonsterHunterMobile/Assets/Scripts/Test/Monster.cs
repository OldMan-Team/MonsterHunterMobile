using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int lifeValue = 100;//怪物血量
    public new string name = "bird";//怪物名称
    public int ExeValue = 50;//被杀死时所获经验
    private void Start()
    {
        Dead();
    }
    //void Update()
    //{
    //    if (lifeValue == 0)
    //    {
    //        Dead();
    //    }
    //}
    public void Dead()
    {
        Debug.Log("Monster has Dead");
        EventCenter.GetInstance().EventTrigger<Monster>("MonsterDead",this);
    }
}
