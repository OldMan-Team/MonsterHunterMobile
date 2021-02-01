using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    public float Height = 10f;
    public GameObject Target;

    void Update()
    {
        this.transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y + Height, Target.transform.position.z - Height / Mathf.Tan(transform.localEulerAngles.x / 180 * Mathf.PI));
    }
}
