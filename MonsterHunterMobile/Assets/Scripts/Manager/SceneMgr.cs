//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.SceneManagement;

//public class SceneMgr : BaseManager<SceneMgr>
//{
//    /// <summary>
//    /// 同步加载场景
//    /// </summary>
//    /// <param name="name"></param>
//    /// <param name="action">加载完场景做的事</param>
//    public void LoadScene(string name,UnityAction action=null)
//    {
//        SceneManager.LoadScene(name);

//        if(action!=null)
//            action();
//    }

//    public void LoadScene(int number, UnityAction action = null)
//    {
//        SceneManager.LoadScene(number);

//        if (action != null)
//            action();
//    }

//    /// <summary>
//    /// 外部异步加载
//    /// </summary>
//    /// <param name="name"></param>
//    /// <param name="action"></param>
//    public void LoadSceneAsyn(string name,UnityAction action=null)
//    {
//        MonoMgr.GetInstance().StartCoroutine(RealLoadSceneAsyn(name, action));
//    }

//    public void LoadSceneAsyn(int number, UnityAction action = null)
//    {
//        MonoMgr.GetInstance().StartCoroutine(RealLoadSceneAsyn(number, action));
//    }

//    /// <summary>
//    /// 协程异步加载
//    /// </summary>
//    /// <param name="name"></param>
//    /// <param name="action"></param>
//    /// <returns></returns>
//    private IEnumerator RealLoadSceneAsyn(string name,UnityAction action)
//    {
//        AsyncOperation ao=SceneManager.LoadSceneAsync(name);
//        //获得场景加载的进度
//        while(!ao.isDone)
//        {
//            //事件中心向外更新进度条
//            EventCenter.GetInstance().EventTrigger("进度条更新",ao.progress);
//            //在里面更新进度
//            yield return ao.progress;
//        }
        
//        yield return ao;

//        if (action != null)
//            action();
//    }

//    private IEnumerator RealLoadSceneAsyn(int number, UnityAction action)
//    {
//        AsyncOperation ao = SceneManager.LoadSceneAsync(number);
//        //获得场景加载的进度
//        while (!ao.isDone)
//        {
//            //事件中心向外更新进度条
//            EventCenter.GetInstance().EventTrigger("进度条更新", ao.progress);
//            //在里面更新进度
//            yield return ao.progress;
//        }

//        yield return ao;

//        if (action != null)
//            action();
//    }
//}
