using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoArrowShooter : MonoBehaviour
{
    public float ShootTime = 3;
    [SerializeField]
    private float currentShootTime = 0;

    public GameObject ArrowPrefab;

    private void Update()
    {
        currentShootTime += Time.unscaledDeltaTime;
        while (currentShootTime > ShootTime)
        {
            currentShootTime -= ShootTime;

            GameObject go = Instantiate(ArrowPrefab);
            go.SetActive(true);
            go.GetComponent<ArcherArrow>().ShootOut(this.transform.position, Vector2.right, 10, 100, 0, 100, 20, 20);
        }
    }
}
