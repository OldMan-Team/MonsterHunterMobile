using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public interface IEventInfo { }

public class EventInfo<T1, T2> : IEventInfo
{
    public UnityAction<T1, T2> action;
    public EventInfo(UnityAction<T1, T2> action)
    {
        this.action += action;
    }
}

public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> action;
    public EventInfo(UnityAction<T> action)
    {
        this.action += action;
    }
}

public class EventInfo : IEventInfo
{
    public UnityAction action;
    public EventInfo(UnityAction action)
    {
        this.action += action;
    }
}


/// <summary>
/// 事件中心
/// </summary>
public class EventCenter : SingletonBase<EventCenter>
{
    public EventCenter() 
    { 
        eventDic = new Dictionary<string, IEventInfo>(); 
    }
    private Dictionary<string, IEventInfo> eventDic;


    /// <summary>
    /// 带参事件触发
    /// </summary>
    /// <typeparam name="T1">参数类型1</typeparam>
    /// <typeparam name="T2">参数类型2</typeparam>
    /// <param name="name">触发的事件</param>
    /// <param name="msg1">参数1</param>
    /// <param name="msg2">参数2</param>
    public void EventTrigger<T1, T2>(string name, T1 msg1, T2 msg2)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T1, T2>).action.Invoke(msg1, msg2);
        }
    }


    /// <summary>
    /// 带参事件触发
    /// </summary>
    /// <typeparam name="T">参数类型</typeparam>
    /// <param name="name">触发的事件</param>
    /// <param name="msg">参数</param>
    public void EventTrigger<T>(string name, T msg)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).action.Invoke(msg);
        }
    }

    /// <summary>
    /// 无参事件触发
    /// </summary>
    /// <param name="name">触发的事件</param>
    public void EventTrigger(string name)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).action.Invoke();
        }
    }

    /// <summary>
    /// 添加带参事件监听
    /// </summary>
    /// <typeparam name="T1">参数类型1</typeparam>
    /// <typeparam name="T2">参数类型2</typeparam>
    /// <param name="name">要监听的事件</param>
    /// <param name="action">监听者</param>
    public void AddEventListener<T1, T2>(string name, UnityAction<T1, T2> action)
    {
        if (action == null)
            return;
        if (!eventDic.ContainsKey(name))
        {
            eventDic.Add(name, new EventInfo<T1, T2>(action));
        }
        else
        {
            (eventDic[name] as EventInfo<T1, T2>).action += action;
        }
    }


    /// <summary>
    /// 添加带参事件监听
    /// </summary>
    /// <typeparam name="T">参数类型</typeparam>
    /// <param name="name">要监听的事件</param>
    /// <param name="action">监听者</param>
    public void AddEventListener<T>(string name, UnityAction<T> action)
    {
        if (action == null)
            return;
        if (!eventDic.ContainsKey(name))
        {
            eventDic.Add(name, new EventInfo<T>(action));
        }
        else
        {
            (eventDic[name] as EventInfo<T>).action += action;
        }
    }

    /// <summary>
    /// 添加无参事件监听
    /// </summary>
    /// <param name="name">要监听的事件</param>
    /// <param name="action">监听者</param>
    public void AddEventListener(string name, UnityAction action)
    {
        if (action == null)
            return;
        if (!eventDic.ContainsKey(name))
        {
            eventDic.Add(name, new EventInfo(action));
        }
        else
        {
            (eventDic[name] as EventInfo).action += action;
        }
    }

    /// <summary>
    /// 移除带参事件监听
    /// </summary>
    /// <typeparam name="T1">参数类型1</typeparam>
    /// <typeparam name="T2">参数类型2</typeparam>
    /// <param name="name">要监听的事件</param>
    /// <param name="action">监听者</param>
    public void RemoveEventListener<T1, T2>(string name, UnityAction<T1, T2> action)
    {
        if (action == null)
            return;
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T1, T2>).action -= action;
            if ((eventDic[name] as EventInfo<T1, T2>).action == null)
                eventDic.Remove(name);
        }

    }



    /// <summary>
    /// 移除带参事件监听
    /// </summary>
    /// <typeparam name="T">参数类型</typeparam>
    /// <param name="name">要监听的事件</param>
    /// <param name="action">监听者</param>
    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (action == null)
            return;
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).action -= action;
            if ((eventDic[name] as EventInfo<T>).action == null)
                eventDic.Remove(name);
        }
            
    }

    /// <summary>
    /// 移除带参事件监听
    /// </summary>
    /// <param name="name">要监听的事件</param>
    /// <param name="action">参数</param>
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (action == null)
            return;
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).action -= action;
            if ((eventDic[name] as EventInfo).action == null)
                eventDic.Remove(name);
        }

    }

    /// <summary>
    /// 清空，场景转换调用
    /// </summary>
    public void Clear()
    {
        eventDic.Clear();
    }

}
