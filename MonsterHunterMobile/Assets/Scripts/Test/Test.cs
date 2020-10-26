//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class test
//{
//    public  test()
//    {
//        MonoMgr.GetInstance().StartCoroutine(test1());
//    }
//    IEnumerator test1()
//    {
//        yield return new WaitForSeconds(1f);
//        Debug.Log("test123");
//    }
//    public void Update()
//    {
//        Debug.Log("test");
//    }
//}
//public class Test : MonoBehaviour
//{
//    public GameObject[] LightList;
//    private float time;
//    private AudioSource audio;
//    void Start()
//    {
//        //test t = new test();
//        //MonoMgr.GetInstance().AddUpdateListener(t.Update);

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //time += Time.deltaTime;
//        //if(time>1.4)
//        //{
//        //    LightList[7].SetActive(true);
//        //}
//        //else if (time > 1.2)
//        //{
//        //    LightList[6].SetActive(true);
//        //}
//        //else if (time > 1)
//        //{
//        //    LightList[5].SetActive(true);
//        //}
//        //else if (time > 0.8)
//        //{
//        //    LightList[4].SetActive(true);
//        //}
//        //else if (time > 0.6)
//        //{
//        //    LightList[3].SetActive(true);
//        //}
//        //else if (time > 0.4)
//        //{
//        //    LightList[2].SetActive(true);
//        //}
//        //else if (time > 0.2)
//        //{
//        //    LightList[1].SetActive(true);
//        //}
//        //else if (time > 0)
//        //{
//        //    LightList[0].SetActive(true);
//        //}
//        //if (Input.GetKeyDown(KeyCode.A))
//        //{
//        //    //MusicMgr.GetInstance().PlayBkMusic("I See Fire");
//        //    MusicMgr.GetInstance().PlaySoundMusic("dog", false,(source)=>
//        //    {
//        //        audio = source;
//        //    });
//        //}

//        //if (Input.GetKeyDown(KeyCode.S))
//        //{
//        //    //MusicMgr.GetInstance().PauseBkMusic("I See Fire");
//        //    //if(audio!=null)
//        //    if (audio == null) Debug.Log("null");
//        //    MusicMgr.GetInstance().StopSoundMusic(audio);
//        //    Debug.Log("stop");
//        //}

//        //if (Input.GetKeyDown(KeyCode.D))
//        //{
//        //    MusicMgr.GetInstance().StopBkMusic("I See Fire");
//        //}

//        //if(Input.GetKeyDown(KeyCode.F))
//        //{
//        //    MusicMgr.GetInstance().changeValue(0.5f);
//        //}

//        //if(Input.GetKeyDown(KeyCode.C))
//        //{
//        //    //两种场景加载方式，可用字符串加载也可用数字加载
//        //    //SceneMgr.GetInstance().LoadScene(1);
//        //    //SceneMgr.GetInstance().LoadSceneAsyn(1);
//        //    //SceneMgr.GetInstance().LoadScene("Assets/Scenes/MainScene.unity");
//        //}
//    }
//}
