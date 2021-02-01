using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 非Mono单例基类
/// </summary>
/// <typeparam name="T">要实现的单例</typeparam>
public class SingletonBase<T> where T : new()
{
    private static T instance;
    public static T GetInstance()
    {
        if (instance == null)
            instance = new T();
        return instance;
    }

    protected SingletonBase(){}
}
