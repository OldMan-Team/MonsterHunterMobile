using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thief_Control : MonoBehaviour
{

	Animator animator;

	Rigidbody rigidbody;

	Collider collider;

	int walkHash = Animator.StringToHash("Walk");
	int climbHash = Animator.StringToHash("Climb");
	int idleHash = Animator.StringToHash("Idle");
	int fallHash = Animator.StringToHash("Fall");

	int fallGroundHash = Animator.StringToHash("FallGround");

	int layerID;


	[SerializeField]
	Transform pointBall;

	[SerializeField]
	Transform tree;

	[SerializeField]
	float dis = 1.0f;

	[SerializeField]
	Button but_Climb;

	[SerializeField]
	Button but_Fall;

	float angle;
	Quaternion lookRo;

	AnimatorStateInfo animatorStateInfo;

	bool fallFlag = false;

	Vector3 fallStartPos;

	////[SerializeField]
	//float fixH = 1.0f;  //高度修正值

	////[SerializeField]
	//float fixH2 = 2.0f;

	////[SerializeField]
	//float fixX = 0.8f;

    float fixH = 1.6f;  //高度修正值1
    float fixH2 = 1.87f; //高度修正值2
    float fixX = 0.4f;  //X轴修正float fixH = 1.6f;  //高度修正值1
 

    float fallVelcity = 0f;

	float fallDis = 0;

	[SerializeField]

	float groundTime = 3.0f;

	float nowGTime = 0f;


	private void Awake()
	{
		animator = gameObject.GetComponent<Animator>();

		rigidbody = gameObject.GetComponent<Rigidbody>();

		collider = gameObject.GetComponentInChildren<Collider>();

		layerID = animator.GetLayerIndex("Base Layer");

	}

	// Update is called once per frame
	void Update()
    {

		animatorStateInfo = animator.GetCurrentAnimatorStateInfo(layerID);

		if (animatorStateInfo.shortNameHash == walkHash || animatorStateInfo.shortNameHash == idleHash)
		{
			but_Fall.interactable = false;
			if (Mathf.Pow(transform.position.x - pointBall.position.x, 2) + Mathf.Pow(transform.position.z - pointBall.position.z, 2) > dis * dis)//需要移动
			{

				animator.SetBool(walkHash, true);

				angle = Vector3.Angle(transform.forward, new Vector3(pointBall.position.x, transform.position.y, pointBall.position.z));
				lookRo = Quaternion.LookRotation(new Vector3(pointBall.position.x - transform.position.x, transform.position.y, pointBall.position.z - transform.position.z), Vector3.up);
				gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, lookRo, 60 * Time.deltaTime < angle / 10.0f ? 60 * Time.deltaTime : angle / 10.0f);

				but_Climb.interactable = false;
			}
			else
			{
				animator.SetBool(walkHash, false);

				if(Mathf.Pow(transform.position.x-tree.position.x,2)+ Mathf.Pow(transform.position.z - tree.position.z,2) < 1f)
				{
					but_Climb.interactable = true;
				}
				else
				{

					but_Climb.interactable = false;
				}

			}

		}else if (animatorStateInfo.shortNameHash == climbHash)
		{
			but_Climb.interactable = false;
			but_Fall.interactable = true;

		}else if (animatorStateInfo.shortNameHash == fallGroundHash)
		{
			nowGTime += Time.deltaTime;

			if (nowGTime > groundTime)
			{

				animator.SetBool(climbHash, false);

				rigidbody.useGravity = true;

				collider.enabled = true;

				fallFlag = false;
			}
		}

		//if (animator.GetNextAnimatorStateInfo(layerID).shortNameHash == fallHash)
		//{
		//	transform.position = fallStartPos;
		//}
        
    }

	private void OnAnimatorMove()
	{
		if (!fallFlag)
		{
			Debug.Log("OnMove");
			animator.ApplyBuiltinRootMotion();
		}
		else
		{
			animatorStateInfo = animator.GetCurrentAnimatorStateInfo(layerID);

			animator.applyRootMotion = false;
			rigidbody.velocity = Vector3.zero;
			//transform.position = fallStartPos-fixH*Vector3.up;

			if (animatorStateInfo.shortNameHash == fallHash)
			{

				if (fallDis < fallStartPos.y + 0.25f)
				{

					fallVelcity += Time.deltaTime * 9.8f;

					fallDis += fallVelcity * Time.deltaTime;

					transform.position = fallStartPos - (fixH + fallDis) * Vector3.up;
				}
				else
				{
					animator.SetTrigger(fallGroundHash);
					
				}
			}
			else if(animatorStateInfo.shortNameHash ==fallGroundHash)
			{
                //Unity高度修正+xz平面修正
				transform.position= transform.position = fallStartPos - (fixH + fallDis-fixH2) * Vector3.up - Vector3.Normalize(new Vector3(tree.position.x, transform.position.y, tree.position.z) - transform.position) * fixX;
			}
		}
	}

	Vector3 lookPos;

	public void startClimb()
	{
		animator.SetBool(climbHash, true);

		rigidbody.useGravity = false;

		collider.enabled = false;

		lookPos = new Vector3(tree.position.x, transform.position.y, tree.position.z);

		gameObject.transform.LookAt(lookPos);

		gameObject.transform.position=lookPos-Vector3.Normalize(lookPos-transform.position)* 0.15815f;
	}

	public void startFall()
	{
		animator.SetTrigger(fallHash);

		fallFlag = true;

		Debug.Log("StartFall");

		fallStartPos = transform.position;

		fallVelcity = 0;

		fallDis = 0;

		nowGTime = 0;
		//rigidbody.useGravity = true;

		//collider.enabled = true;
	}
}
