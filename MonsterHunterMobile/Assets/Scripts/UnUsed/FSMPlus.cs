using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Condition
{
    SawThief=0
}
public enum FSMStateID
{
    Idle=0,
    Walk,
    Climb,
    Dead
}

public class FSMPlus : FSM
{
    private List<FSMState> fsmStates;   //存放所有状态
    private FSMStateID currentStateID;  //当前状态编号
    public FSMStateID CurrentStateID
    {
        get { return currentStateID; }
    }
    private FSMState currentState;      //当前状态
    public FSMState CurrentState
    {
        get { return currentState; }
    }

    public FSMPlus()
    {
        fsmStates = new List<FSMState>();
    }

    /// <summary>
    /// 添加状态
    /// </summary>
    /// <param name="fsmstate"></param>
    public void AddFSMState(FSMState fsmstate)
    {
        if(fsmstate==null)      //检查是否为空
        {
            Debug.Log("fsmstate is null");
        }
        if(fsmStates.Count==0)
        {
            fsmStates.Add(fsmstate);
            currentState = fsmstate;
            currentStateID = fsmstate.ID;
            return;
        }
        foreach(FSMState state in fsmStates) //检查是否重复
        {
            if(state.ID==fsmstate.ID)
            {
                Debug.Log("try to add fsmstate that has existed");
                return;
            }
        }
        fsmStates.Add(fsmstate);
    }

    /// <summary>
    /// 删除状态
    /// </summary>
    /// <param name="fsmstate"></param>
    public void DeleteState(FSMStateID fsmstate)
    {
        foreach (FSMState state in fsmStates) //检查是否重复
        {
            if (state.ID == fsmstate)
            {
                fsmStates.Remove(state);
                return;
            }
        }
        Debug.Log("try to delete fsmstate that has not existed");
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="condition"></param>
    public void StateChange(Condition condition)
    {
        //获取条件下转换后的新状态
        FSMStateID id = currentState.GetOutputState(condition);

        //将当前状态ID切换
        currentStateID = id;
        //将当前状态切换
        foreach (FSMState state in fsmStates)
        {
            if(state.ID==currentStateID)
            {
                currentState = state;
                break;
            }
        }
    }

}
