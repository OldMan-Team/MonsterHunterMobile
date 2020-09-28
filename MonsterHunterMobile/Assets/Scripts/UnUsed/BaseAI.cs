using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAI : MonoBehaviour
{
    public Transform []pathWaypoints;
    private float moveSpeed;
    //private 

    private Rigidbody rb;
    private Animator ani;

   
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        ani = gameObject.GetComponent<Animator>();
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
