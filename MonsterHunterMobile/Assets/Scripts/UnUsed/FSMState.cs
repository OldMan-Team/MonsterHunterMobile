using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMState
{
    //字典存放：条件-转换目标状态
    protected Dictionary<Condition, FSMStateID> stateDic = new Dictionary<Condition, FSMStateID>();
    protected FSMStateID stateID;
    public FSMStateID ID 
    { 
        get 
        { 
            return stateID; 
        } 
    }

    protected Vector3 destPosition;
    protected Transform[] wayPoints;
    protected float curRotSpeed;
    protected float curMoveSpeed;

    /// <summary>
    /// 向字典添加项：条件-转换目标状态
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="id"></param>
    public void AddCondition(Condition condition,FSMStateID id)
    {
        if(stateDic.ContainsKey(condition))
        {
            Debug.Log("the condition has existed");
            return;
        }
        stateDic.Add(condition, id);
        Debug.Log("add " + condition + " id:" + id);
    }

    /// <summary>
    /// 删除字典某一项
    /// </summary>
    /// <param name="condition"></param>
    public void DeleteCondition(Condition condition)
    {
        if (stateDic.ContainsKey(condition))
        {
            stateDic.Remove(condition);
            return;
        }
        
    }

    /// <summary>
    /// 通过条件获取转换目标状态
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    public FSMStateID GetOutputState(Condition condition)
    {
        return stateDic[condition];
    }

    //判断状态是否转换，发生哪个转换
    public abstract void Judge(Transform player, Transform npc);
    //状态的角色行为，动画
    public abstract void Action(Transform player, Transform npc);

}
