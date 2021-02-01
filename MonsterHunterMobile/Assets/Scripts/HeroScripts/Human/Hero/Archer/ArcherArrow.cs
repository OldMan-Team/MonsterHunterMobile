using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherArrow : MonoBehaviour
{
    [SerializeField]
    private Vector3 startPos;
    [SerializeField]
    private float distance_max;
    [SerializeField]
    private float distance_best_max;
    [SerializeField]
    private float distance_best_min;
    [SerializeField]
    private float damage_max;
    [SerializeField]
    private float damage_min;

    private float damage
    {
        get
        {
            float dis = (this.transform.position - startPos).magnitude;
            if (dis < distance_best_min)
            {
                return Mathf.Lerp(damage_min, damage_max, dis / distance_best_min);
            }
            else if (dis > distance_best_max)
            {
                return Mathf.Lerp(damage_min, damage_max, (dis - distance_best_max) / (distance_max - distance_best_max));
            }
            else
                return damage_max;
        }
    }

    public void ShootOut(Vector3 start, Vector2 dir, float speed, float dis_max, float dis_best_min, float dis_best_max, float damage_max, float damage_min)
    {
        startPos = start;
        this.transform.position = start;
        this.transform.right = new Vector3(dir.x, 0, dir.y);
        this.GetComponent<Rigidbody>().velocity = new Vector3(dir.x, 0, dir.y).normalized * speed;
        distance_max = dis_max;
        distance_best_max = dis_best_max;
        distance_best_min = dis_best_min;
        this.damage_max = damage_max;
        this.damage_min = damage_min;
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageHitter hitter = other.gameObject.GetComponent<DamageHitter>();
        if (hitter != null)
        {
            Hit(hitter);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    DamageHitter hitter = collision.gameObject.GetComponent<DamageHitter>();
    //    if (hitter != null)
    //    {
    //        Hit(hitter);
    //    }
    //    else
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}

    private void Hit(DamageHitter hitter)
    {
        Debug.Log("Hit");
        hitter.MakeDamage(damage);
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if ((this.transform.position - startPos).sqrMagnitude > distance_max * distance_max)
        {
            OutOfMaxRange();
        }
    }

    private void OutOfMaxRange()
    {
        Destroy(this.gameObject);
    }

}
