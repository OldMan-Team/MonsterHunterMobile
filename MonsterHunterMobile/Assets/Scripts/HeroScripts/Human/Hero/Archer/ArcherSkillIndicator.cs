using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherSkillIndicator : MonoBehaviour
{
    public GameObject indicator;
    public float OriginLength;

    public void SetLength(float length)
    {
        indicator.transform.localScale = new Vector3(indicator.transform.localScale.x, 1, length / OriginLength);
    }

    public void SetDirection(Vector2 dir)
    {
        indicator.transform.forward = new Vector3(dir.x, 0, dir.y);
    }

    public void SetIndicatorActive(bool active)
    {
        indicator.SetActive(active);
    }
}
