using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMessage :MonoBehaviour
{
    #region 单例化
    public static SceneMessage Instance { get; private set; }
    protected void Awake()
    {
        if (Instance == null)
        {
            Instance = (SceneMessage)this;
        }
        else
        {
            Destroy(gameObject);
        }
        Init();
    }
    #endregion
    public GameObject thief;
    public GameObject aside;
    public GameObject dog;
    void Init()
    {
        thief = GameObject.FindGameObjectWithTag("Thief");
        aside = GameObject.FindGameObjectWithTag("Aside");
        dog= GameObject.FindGameObjectWithTag("Dog");
    }
    

    //test
    public int[] ThiefMoveOrde;
    private int curIndex=0;

   

    private void Start()
    {
        //asideController.LoadAsideList(1);
        //EventCenter.GetInstance().AddEventListener("MoveToEndIndex", thiefUpdate);
    }
    private void Update()
    {

    }


    //private void thiefUpdate()
    //{
    //    Invoke("ThiefMove", 1f);
        
    //}

    //private void ThiefMove()
    //{
    //    if (curIndex < ThiefMoveOrde.Length)
    //    {
    //        thiefController.SetEndIndex(ThiefMoveOrde[curIndex]);
    //        curIndex++;
    //    }
    //}


}
