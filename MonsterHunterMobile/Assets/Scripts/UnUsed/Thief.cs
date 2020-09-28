using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    public GameObject[] wayPoints;
    public GameObject model;
    private Animator ani;


    //动画ID
    private int walkingID = Animator.StringToHash("walking");
    private int climbingID = Animator.StringToHash("climbing");
    private int climbing1ID = Animator.StringToHash("climbing1");
    private int fallStartID = Animator.StringToHash("fallStart");
    private int fallingID = Animator.StringToHash("falling");
    private int fallGroundID = Animator.StringToHash("fallGround");

    private float curRotSpeed ;
    private float curMoveSpeed;
    private float arrivedDistance = 0.6f;
    private int curPointIndex=0;

    [SerializeField]
    private bool walking;
    [SerializeField]
    private bool climbing;
    [SerializeField]
    private bool falling;
    public Vector3 velocity;

    private void Awake()
    {
        ani = model.GetComponent<Animator>();

    }

    void Start()
    {
        //ani.SetBool(walkingID, true);
    }

    // Update is called once per frame
    void Update()
    {

        //velocity = GetComponent<Rigidbody>().velocity;
        if (Input.GetKeyDown(KeyCode.X))
        {
            //walking = true;
            ani.SetBool(walkingID, true);
        }
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    //climbing = true;
        //    //walking = false;
        //    ani.SetBool(walkingID, false);
        //    ani.SetBool(climbingID, true);
        //    Rigidbody rb = GetComponent<Rigidbody>();
        //    rb.freezeRotation = true;
        //    rb.velocity = Vector3.zero;
        //    rb.useGravity = false;
        //}
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    //climbing = false;
        //    //walking = false;
        //    ani.SetBool(climbingID, false);
        //    FallStart(transform, fallStartID);
        //}

        if (Input.GetKeyDown(KeyCode.B))
        {
            FallStart(transform, fallStartID);
        }

        if (walking)
        {
            Pathfind(curPointIndex, walkingID);
        }

        if (climbing)
        {
            Climbing(wayPoints[wayPoints.Length - 1].transform, transform, climbingID);
        }

    }

    //寻路
    public void Pathfind(int index,int actionID)
    {
        if(index<wayPoints.Length-1)
        MoveToTarget(wayPoints[index].transform,transform,actionID);
        //Debug.Log("walking");
    }

    /// <summary>
    /// 移动到目标点
    /// </summary>
    /// <param name="target">目标点</param>
    /// <param name="npc">移动的物体</param>
    /// <param name="actionID">移动的动画ID</param>
    public void MoveToTarget(Transform target, Transform npc,int actionID)
    {
        Vector3 destPosition = target.position;

        curRotSpeed = 6.0f;
        curMoveSpeed = 0.3f;

        //目标点与npc相距超过一定值才会移动
        if (Vector3.Distance(npc.position, target.position) > arrivedDistance)
        {
            //转向目标
            Quaternion targetRotation = Quaternion.LookRotation(destPosition - npc.position);
            //只旋转y轴
            npc.rotation = new Quaternion(npc.rotation.x,Quaternion.Slerp(npc.rotation, targetRotation, Time.deltaTime * curRotSpeed).y, npc.rotation.z, npc.rotation.w);

            //Vector3 destPosition2 = new Vector3(destPosition.x, npc.position.y, destPosition.z);
            //Vector3 targetForward = Vector3.Slerp(npc.forward,destPosition2, 0.3f);
            //npc.forward = targetForward;

            //Vector3 nowPosition = new Vector3(npc.position.x, 0, npc.position.z);
            //Vector3 destPosition2 = new Vector3(destPosition.x, 0, destPosition.z);
            ani.SetBool(actionID, true);
            Rigidbody rb = npc.GetComponent<Rigidbody>();
            rb.velocity = (destPosition-npc.position)* curMoveSpeed ;
            //transform.Translate(Vector3.forward * curSpeed * Time.deltaTime, Space.World);
            Debug.Log("walking");
        }
        else
        {
            ani.SetBool(actionID, false);
            curPointIndex++;
            Debug.Log("walking arrived");
            Invoke("ClimbStart",1.0f);
            walking = false;
        }
    }

    public void ClimbStart()
    {
        climbing = true;
    }

    //ClimbState
    public void Climbing(Transform target, Transform npc, int actionID)
    {
        curMoveSpeed = 0.2f;

        Rigidbody rb = npc.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.velocity = Vector3.zero;
        Vector3 destPosition = target.position;

        //目标点与npc相距超过一定值才会移动
        if (Vector3.Distance(npc.position, target.position) > arrivedDistance)
        {
            ani.SetBool(actionID, true);
            Debug.Log("climbing");            
            rb.useGravity = false;
            transform.Translate(Vector3.up * curMoveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            Debug.Log("到达高点");
            ani.SetBool(actionID, false);
            climbing = false;       
        }
    }
    public void FallStart(Transform npc,int actionID)
    {
        
        Debug.Log("fallstart");

        climbing = false;
        falling = true;
        Rigidbody rb = npc.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        
        rb.useGravity = true;
        ani.SetTrigger(actionID);
    }

    public void Falling(Transform npc, int actionID)
    {

    }

    public void FallGround()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (falling)
        {
            if (collision.gameObject.tag == "Ground")
            {
                
                 Debug.Log("OnGround");
                 falling = false;
                 ani.SetBool(climbingID, false);
                 ani.SetTrigger(fallGroundID);
                
                //FallGround();         
            }
        }
       
    }
}
