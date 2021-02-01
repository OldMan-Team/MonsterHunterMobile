using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intelligent : MonoBehaviour, Hurtter
{
    public float HealthPoint;
    [SerializeField]
    protected float currentHP;
    
    public bool isDead
    {
        get
        {
            return currentHP <= 0;
        }
    }

    protected void ResetAll()
    {
        currentHP = HealthPoint;
    }

    public virtual void Hurt(float damage)
    {
        
        currentHP -= damage;
        if (currentHP < 0)
            currentHP = 0;
        if (currentHP == 0)
            Dead();
    }

    public virtual void Dead()
    {
        EventCenter.GetInstance().EventTrigger("Object Dead", this.gameObject);
        Debug.Log("Dead");
        //Destroy(this.gameObject);
    }

    public virtual void Start()
    {
        ResetAll();
    }

}
