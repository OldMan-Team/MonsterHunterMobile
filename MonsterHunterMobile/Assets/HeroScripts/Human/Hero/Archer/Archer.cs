using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Hero
{
    public int MaxFocusLevel = 3;
    [SerializeField]
    private int currentFocusLevel = 0;

    public float FullChargeTime = 1.0f;

    public float RollSpeed = 5.0f;

    [SerializeField]
    private bool isRolling = false;
    
    
    //蓄力开始
    public override void OnPressBegin_First()
    {
        if (isRolling)
            return;
        Moveable = false;
        StopMove();
        MyAnimator.SetBool("isCharing", true);
    }

    //放弃蓄力
    public override void OnPressEnd_First()
    {
        if (isRolling)
            return;
        Moveable = true;
        MyAnimator.SetBool("isCharing", false);
    }

    //改变方向
    public override void OnDragUpdate_First(Vector2 pos)
    {
        if (isRolling)
            return;
        Moveable = false;
        StopMove();
        MyAnimator.SetBool("isCharing", true);
        AimTo(pos);
    }

    //射击
    public override void OnDragEnd_First(Vector2 pos)
    {
        if (isRolling)
            return;
        MyAnimator.SetTrigger("Shoot");
    }
    
    public void ShootFinishCallback()
    {
        Moveable = true;
        MyAnimator.SetBool("isCharing", false);
    }

    //翻滚
    public override void OnDragEnd_Second(Vector2 pos)
    {
        if (!Moveable)
            return;
        Moveable = false;
        AimTo(pos);
        MyAnimator.SetTrigger("Roll");
        MyRigidbody.velocity = (new Vector3(pos.x, 0, pos.y).normalized * RollSpeed);

    }

    public void RollStartCallback()
    {
        isRolling = true;
        
    }

    public void RollEndCallback()
    {
        isRolling = false;
        Moveable = true;
        StopMove();
    }
}
