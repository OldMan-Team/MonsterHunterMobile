using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intelligent : MonoBehaviour
{
    private float lifeValue;            //生命值
    private float moveSpeed;            //移动速度
    private float turnSpeed;            //转向速度
    private float angle;                //转向角度
    private Quaternion lookRotation;    //转向向量
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 朝向目标点
    /// </summary>
    /// <param name="target">目标点</param>
    /// <param name="turnSpeed">转向速度</param>
    public void TurnToTarget(Transform target, float turnSpeed)
    {
        //获取当前朝向与目标点之间的角度
        angle = Vector3.Angle(transform.forward, new Vector3(target.position.x, transform.position.y, target.position.z));
        //转向速度系数，默认为1秒，当角度过小，转向速度变慢
        float temp = 60 * Time.deltaTime < angle / 10.0f ? 60 * Time.deltaTime : angle / 10.0f;
        //当前位置指向目标点的Quaternion
        lookRotation = Quaternion.LookRotation(new Vector3(target.position.x - transform.position.x, 0, 
                                                        target.position.z - transform.position.z), Vector3.up);
        //进行转向
        gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, lookRotation, temp * turnSpeed);
    }
}
