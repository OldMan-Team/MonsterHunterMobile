using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MonoController : MonoBehaviour
{
    private event UnityAction updateEvent;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if(updateEvent!=null)
        {
            updateEvent();
        }
    }

    /// <summary>
    /// 添加帧更新监听
    /// </summary>
    /// <param name="action"></param>
    public void AddUpdateListener(UnityAction action)
    {
        updateEvent += action;
    }

    /// <summary>
    /// 移除帧更新监听
    /// </summary>
    /// <param name="action"></param>
    public void RemoveUpdateListener(UnityAction action)
    {
        updateEvent -= action;
    }

}
