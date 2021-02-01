//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;

//public class ResMgr : BaseManager<ResMgr>
//{
//    //同步加载
//    public T Load<T>(string name)where T:Object
//    {
//        T res = Resources.Load<T>(name);
//        if(res is GameObject)
//        {
//            return GameObject.Instantiate(res);
//        }
//        else
//        {
//            return res;
//        }
//    }
//    //异步加载
//    public void LoadAsync<T>(string name,UnityAction<T> callBack)where T:Object
//    {
//        MonoMgr.GetInstance().StartCoroutine(RealLoadAsync<T>(name,callBack));
//    }
//    /// <summary>
//    /// 真正的异步加载
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    /// <param name="name">地址文件名</param>
//    /// <param name="callBack">加载完做的事</param>
//    /// <returns></returns>
//    private IEnumerator RealLoadAsync<T>(string name, UnityAction<T> callBack) where T:Object
//    {
//        ResourceRequest rr=Resources.LoadAsync<T>(name);
//        yield return rr;

//        if(rr.asset is GameObject)
//        {
//            callBack(GameObject.Instantiate(rr.asset) as T);
//        }
//        else
//        {
//            callBack(rr.asset as T);
//        }
//    }
//}
