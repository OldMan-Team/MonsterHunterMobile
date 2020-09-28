using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 空接口(里氏代换)
/// </summary>
public interface IEventInfo
{

}

public class EventInfo<T>:IEventInfo
{
    public UnityAction<T> actions;
    public EventInfo(UnityAction<T> action)
    {
        actions = action;
    }
}

public class EventInfo: IEventInfo
{
    public UnityAction actions;
    public EventInfo(UnityAction action)
    {
        actions = action;
    }
}
public class EventCenter : BaseManager<EventCenter>
{
    /// <summary>
    /// key:事件名称
    /// value:委托函数
    /// </summary>
    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();
    //如果是无参数的，只需要将<Object>去掉即可，

    /// <summary>
    /// 添加有参事件监听
    /// </summary>
    /// <param name="name">事件名</param>
    /// <param name="action">用于处理事件的委托函数</param>
    public void AddEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions += action;
        }
        else
        {
            eventDic.Add(name, new EventInfo<T>(action));
        }
    }

    /// <summary>
    /// 添加无参事件监听
    /// </summary>
    /// <param name="name">事件名</param>
    /// <param name="action">用于处理事件的委托函数</param>
    public void AddEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions += action;
        }
        else
        {
            eventDic.Add(name, new EventInfo(action));
        }
    }

    /// <summary>
    /// 删除有参事件监听
    /// </summary>
    /// <param name="name">事件名</param>
    /// <param name="action">处理事件的委托函数</param>
    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions -= action;
        }
    }

    /// <summary>
    /// 删除无参事件监听
    /// </summary>
    /// <param name="name">事件名</param>
    /// <param name="action">处理事件的委托函数</param>
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions -= action;
        }
    }

    /// <summary>
    /// 有参事件触发
    /// </summary>
    /// <param name="name">监听事件名</param>
    public void EventTrigger<T>(string name,T Info)
    {
        if (eventDic.ContainsKey(name))
        {
            if((eventDic[name] as EventInfo<T>).actions != null)
                (eventDic[name] as EventInfo<T>).actions(Info);
        }
    }

    /// <summary>
    /// 无参事件触发
    /// </summary>
    /// <param name="name">监听事件名</param>
    public void EventTrigger(string name)
    {
        if (eventDic.ContainsKey(name))
        {
            if ((eventDic[name] as EventInfo).actions != null)
                (eventDic[name] as EventInfo).actions();
        }
    }

    /// <summary>
    ///清空监听事件列表
    /// 切换场景时,防止漏写
    /// </summary>
    public void ClearEventListener()
    {
        eventDic.Clear();
    }
}
