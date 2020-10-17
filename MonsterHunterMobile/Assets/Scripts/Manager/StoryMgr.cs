//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
////public class sceneBorder
////{
////    public float xLowest;   //x轴移动的最大边界
////    public float xHighest;
////    public float yLowest;   //y轴移动的最大边界
////    public float yHighest;
////    public float zLowest;   //z轴移动的最大边界
////    public float zHighest;
////}
//public enum Scenes
//{
//    startScene,
//    chapter1,
//    chapter2,
//    chapter3,
//    chapter4,
//    endScene
//}



//public class StoryMgr : Singleton<StoryMgr>
//{ 
//    public Scenes curScene;
//    private bool sceneHasChanged=false;
//    //public Dictionary<int, sceneBorder> sceneBorderList = new Dictionary<int, sceneBorder>();
//    public GameObject thief;
//    public GameObject aside;
//    public GameObject dog;
//    public GameObject cloudBurst;
//    private ThiefController thiefController;
//    private Aside asideController;


//    /// <summary>
//    /// 小偷移动序号字典：场景-对应场景移动列表
//    /// </summary>
//    public Dictionary<Scenes,int[]> ThiefMoveOrdeDic; 
//    private int curSceneDicIndex ;  //当前场景的小偷移动序号


//    private void Start()
//    {
//        EventCenter.GetInstance().AddEventListener("MoveToEndIndex", thiefUpdate);
//        curScene = Scenes.startScene;

//    }

//    private void Update()
//    {
//        MonitorSceneIfChanged();
//    }

//    /// <summary>
//    /// 每个场景的初始化
//    /// </summary>
//    private void EverySceneInit()
//    {
//        if(curScene!= Scenes.startScene)  //如果当前场景不是开始场景，则更新组件
//        {
//            if(SceneMessage.Instance!=null)
//            {
//                thief = SceneMessage.Instance.thief;
//                aside = SceneMessage.Instance.aside;
//                dog = SceneMessage.Instance.dog;
//                cloudBurst = Resources.Load<GameObject>("Prefabs/CloudBurst");
//                thiefController = thief.GetComponent<ThiefController>();
//                asideController = aside.GetComponent<Aside>();
//                curSceneDicIndex = 0;
//                thiefUpdate();
//            }
//        }
        
//    }

//    /// <summary>
//    /// 小偷在当前场景的移动更新(监听函数)
//    /// </summary>
//    private void thiefUpdate()
//    {
//        Invoke("ThiefMove", 1f); 
//    }
//    private void ThiefMove()
//    {
//        thiefController.SetEndIndex(ThiefMoveOrdeDic[curScene][curSceneDicIndex]);
//        curSceneDicIndex++;
//    }


//    /// <summary>
//    /// 监视场景是否改变
//    /// </summary>
//    private void MonitorSceneIfChanged()
//    {
//        if(sceneHasChanged)
//        {
//            EverySceneInit();
//            sceneHasChanged = false;//重置
//        }
//    }

//    /// <summary>
//    /// 修改故事进度
//    /// </summary>
//    public void ChangeProgress(StoryPoint point)
//    {
//        switch(point)
//        {
//            case StoryPoint.chapter1_1://瓦片

//                break;
//            case StoryPoint.chapter1_2://可落入草丛点
//                break;
//            case StoryPoint.chapter1_3://到达厨房点
//                asideController.MakeAside(3);

//                Invoke("LoadNextScene",3.0f);
//                break;
//        }

//    }



//    /// <summary>
//    /// 切换到下一个场景
//    /// </summary>
//    public void LoadNextScene()
//    {
//        switch(curScene)
//        {
//            case Scenes.startScene:
//                //asideController.LoadAsideList(1);
//                curScene = Scenes.chapter1;
//                SceneMgr.GetInstance().LoadSceneAsyn("Scenes/GameScenes/Chapter_1",()=>
//                {
//                    asideController.LoadAsideList(1);
//                });
//                break;
//            case Scenes.chapter1:
//                //asideController.LoadAsideList(2);
//                curScene = Scenes.chapter2;
//                SceneMgr.GetInstance().LoadSceneAsyn("Scenes/GameScenes/Chapter_2",() =>
//                {
//                    asideController.LoadAsideList(2);
//                });
//                break;
//            case Scenes.chapter2:
//                //asideController.LoadAsideList(3);
//                curScene = Scenes.chapter3;
//                SceneMgr.GetInstance().LoadSceneAsyn("Scenes/GameScenes/Chapter_3", () =>
//                {
//                    asideController.LoadAsideList(3);
//                });
//                break;
//            case Scenes.chapter3:
//                //asideController.LoadAsideList(4);
//                curScene = Scenes.chapter4;
//                SceneMgr.GetInstance().LoadSceneAsyn("Scenes/GameScenes/Chapter_4", () =>
//                {
//                    asideController.LoadAsideList(4);
//                });
//                break;
//            case Scenes.chapter4:
//                curScene = Scenes.endScene;
//                SceneMgr.GetInstance().LoadSceneAsyn("Scenes/GameScenes/EndScene");
//                break;
//            case Scenes.endScene:
//                curScene = Scenes.startScene;
//                SceneMgr.GetInstance().LoadSceneAsyn("Scenes/GameScenes/startScene");
//                break;
//        }
//        sceneHasChanged = true;
//    }

//    /// <summary>
//    /// 返回标题界面
//    /// </summary>
//    public void BackStartScene()
//    {
//        curScene = Scenes.startScene;
//        SceneMgr.GetInstance().LoadSceneAsyn("Scenes/GameScenes/startScene");
//    }

//    public void Fright()
//    {
//        asideController.MakeAside(0);
//        Invoke("RealFight", 2.5f);  //为了先让字幕显示完
//    }

//    public void RealFight()
//    {
//        GameObject cloud = Instantiate(cloudBurst, (thief.transform.position + dog.transform.position) / 2 + new Vector3(0, 1, 0), Quaternion.identity);
//        MusicMgr.GetInstance().PlaySoundMusic("DogAndHumanFight", false);
//        dog.GetComponent<DogController>().FightAfter();
//        thiefController.StartWalk();
//    }

//}
