//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public enum StoryPoint
//{
//    chapter1_1,
//    chapter1_2,
//    chapter1_3

//}

//public class StoryTriggerPoint : MonoBehaviour
//{
//    public bool precondition;       //前提条件
//    public StoryPoint curPoint;     //当前点

//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }

//    public void OnTriggerEnter(Collider other)
//    {
//        if(other.tag=="Thief")
//        {
//            if(precondition)
//            {
//                StoryMgr.Instance.ChangeProgress(curPoint);
//            }
//        }
//    }
//}
