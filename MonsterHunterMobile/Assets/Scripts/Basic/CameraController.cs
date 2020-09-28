using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public List<GameObject> targetList;//摄像机可切换对象
    public Transform targetTr;         //当前摄像机跟随对象

    private Vector3 needToMove;         //摄像机移动向量
    private float moveSpeed = 2.0f;     //摄像机移动速度
    private Vector3 cameraRotation= new Vector3(45, 180, 0);  //固定摄像机角度
    private Transform cameraTr;
    private Rigidbody rd;
    void Awake()
    {
        cameraTr = GetComponent<Transform>();
        rd = gameObject.AddComponent<Rigidbody>();
        rd.useGravity = false;  //取消重力
        transform.rotation = Quaternion.Euler(cameraRotation);  //旋转固定摄像机角度
    }

    private void Start()
    {
        EventCenter.GetInstance().AddEventListener<GameObject>("SetPossessing", GetTarget);
    }

    void Update()
    {
        CameraUpdate();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeTarget(targetList[1], false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeTarget(targetList[0], false);
        }
    }

    private void CameraUpdate()
    {

        if (targetTr != null)
        {
            needToMove = new Vector3(targetTr.position.x - (cameraTr.position.x), targetTr.position.y+10-transform.position.y, targetTr.position.z+8 - cameraTr.position.z );
            rd.velocity = (needToMove.magnitude >= 0 ? moveSpeed * needToMove : Vector3.zero);
        }
    }

    /// <summary>
    /// 对外切换目标接口
    /// </summary>
    /// <param name="newTarget">新的目标</param>
    /// <param name="isImmediately">是否立刻将摄像机转移到目标位置</param>
    public void ChangeTarget(GameObject newTarget, bool isImmediately)
    {
        //FollowingTarget = newTarget;
        targetTr = newTarget.GetComponent<Transform>();
        if (isImmediately)
        {
            cameraTr.position = targetTr.position;
        }
    }

    //监听事件触发的委托事件
    public void GetTarget(GameObject Info)
    {
        ChangeTarget(Info, false);
    }
    //销毁时删除监听事件所触发的委托事件
    void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener<GameObject>("SetPossessing", GetTarget);
    }

}
