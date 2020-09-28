using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayControl : MonoBehaviour
{

	Camera cma;

	[SerializeField]
	Transform pointBall;

	private void Awake()
	{
		cma = gameObject.GetComponent<Camera>();
	}

	RaycastHit raycastHit;

	// Update is called once per frame
	void Update()
    {

		if (Input.GetMouseButton(0))
		{
			if(Physics.Raycast(cma.ScreenPointToRay(Input.mousePosition),out raycastHit)){
				if (raycastHit.collider.CompareTag("Ground"))
				{
					pointBall.position = raycastHit.point + Vector3.up;
				}
			}

		}
        
    }
}
