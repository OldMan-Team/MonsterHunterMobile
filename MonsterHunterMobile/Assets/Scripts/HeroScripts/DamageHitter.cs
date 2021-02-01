using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Hurtter
{
    void Hurt(float damage);
}

public class DamageHitter : MonoBehaviour
{
    public GameObject hurtterObj;
    public void Start()
    {
        hurtter = hurtterObj.GetComponent<Hurtter>();
    }
    public Hurtter hurtter;
    public void MakeDamage(float Damage)
    {
        hurtter.Hurt(Damage);
    }

}
