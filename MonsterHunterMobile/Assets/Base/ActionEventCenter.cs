using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionEventCenter : SingletonBase<ActionEventCenter>
{
    public ActionEventCenter()
    {
        eventDic = new Dictionary<string, IEventInfo>();
    }
    private Dictionary<string, IEventInfo> eventDic;

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
    /// 清空，场景转换调用
    /// </summary>
    public void Clear()
    {
        eventDic.Clear();
    }
}
