using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Hero
{
    public float RollSpeed = 5.0f;

    public ArcherSkillIndicator indicator;
    public ArcherInfoUI archerInfoUI;

    [SerializeField]
    private bool isRolling = false;

    [Header("攻击属性")]
    public GameObject ArrowPrefab;
    public Transform ArrowStartPos;

    public float ArrowSpeed = 5;
    private float currentArrowSpeed
    {
        get
        {
            return ArrowSpeed * currentChargeRate;
        }
    }

    #region Charge
    //无蓄力生效属性
    public float BaseChargeLevel = 0.4f;
    //(0,1)代表蓄力状态
    [SerializeField]
    private float _currentChargeLevel;
    private float currentChargeLevel
    {
        get 
        {
            return _currentChargeLevel;
        }
        set
        {
            _currentChargeLevel = value;
            if (archerInfoUI)
                archerInfoUI.SetCharge(value);
        }
    }
    private float currentChargeRate
    {
        get
        {
            return (1 - BaseChargeLevel) * currentChargeLevel + BaseChargeLevel;
        }
    }
    [SerializeField]
    private bool isCharging = false;
    [SerializeField]
    private float StartChargeTime = 0;



    #endregion Charge

    public int MaxFocusLevel = 3;
    [SerializeField]
    private int currentFocusLevel = 0;

    public float FullChargeTime = 1.0f;
    public float ChargeTimeDecreasePerLevel = 0.15f;
    private float currentFullChargeTime
    {
        get
        {
            return FullChargeTime - ChargeTimeDecreasePerLevel * currentFocusLevel;
        }
    }

    public float BaseMaxDamage = 50;
    public float MaxDamageIncreasePerLevel = 20;
    private float currentMaxDamage
    {
        get
        {
            return (BaseMaxDamage + MaxDamageIncreasePerLevel * currentFocusLevel) * currentChargeRate;
        }
    }

    public float BaseMinDamage = 20;
    public float MinDamageIncreasePerLevel = 15;
    private float currentMinDamage
    {
        get
        {
            return (BaseMinDamage + MinDamageIncreasePerLevel * currentFocusLevel) * currentChargeRate;
        }
    }

    public float BaseMaxDistance = 6;
    public float MaxDistanceIncreasePerLevel = 1.2f;
    private float currentMaxDistance
    {
        get
        {
            return (BaseMaxDistance + MaxDistanceIncreasePerLevel * currentFocusLevel) * currentChargeRate;
        }
    }

    //最佳位置设定
    //从人物位置到最末位置 映射为 (0,1)
    //从BestDistancePos 两边延伸
    //满蓄力最末距离在最佳范围内 因此将最大距离/4为每层蓄力的位置
    [Range(0.5f, 0.95f)]
    public float BestDistancePos;
    private float currentMaxBestDistance
    {
        get
        {
            return currentMaxDistance * (BestDistancePos + (1 - BestDistancePos) / (MaxFocusLevel + 1) * (currentFocusLevel + 1));
        }
    }
    private float currentMinBestDistance
    {
        get
        {
            return currentMaxDistance * (BestDistancePos - (1 - BestDistancePos) / (MaxFocusLevel + 1) * (currentFocusLevel + 1));
        }
    }

    //蓄力开始
    public override void OnPressBegin_First()
    {
        if (isRolling || !Moveable || isDead)
            return;
        Moveable = false;

        
        StopMove();
        MyAnimator.SetBool("isCharing", true);
        StartCharge();
    }

    private void StartCharge()
    {
        isCharging = true;
        StartChargeTime = Time.time;
        currentChargeLevel = 0;
        indicator.SetIndicatorActive(true);
    }

    private void Update_Charge()
    {
        if (!isCharging)
            return;



        float c = (Time.time - StartChargeTime) / currentFullChargeTime;
        if (c > 1)
            currentChargeLevel = 1;
        else
            currentChargeLevel = c;

        indicator.SetDirection(GetFaceTo());
        indicator.SetLength(currentMaxDistance);
    }

    protected override void Update()
    {
        base.Update();
        Update_Charge();
    }

    //放弃蓄力
    public override void OnPressEnd_First()
    {
        ChargeBreak();
    }

    private void ChargeBreak()
    {
        Moveable = true;
        isCharging = false;
        MyAnimator.SetBool("isCharing", false);
        indicator.SetIndicatorActive(false);

    }

    //改变方向
    public override void OnDragUpdate_First(Vector2 pos)
    {
        if (isRolling || isDead)
            return;

        if (!isCharging && Moveable)
            StartCharge();

        Moveable = false;
        StopMove();
        MyAnimator.SetBool("isCharing", true);
        AimTo(pos);
    }

    //射击
    public override void OnDragEnd_First(Vector2 pos)
    {
        if (isRolling || isDead)
            return;
        MyAnimator.SetTrigger("Shoot");
        if (isCharging)
            ShootOut();
    }
    
    private void ShootOut()
    {
        
        GameObject arrow = Instantiate(ArrowPrefab);
        arrow.SetActive(true);
        arrow.GetComponent<ArcherArrow>().ShootOut(
            ArrowStartPos.position, GetFaceTo(),
            currentArrowSpeed,
            currentMaxDistance,
            currentMinBestDistance,
            currentMaxBestDistance,
            currentMaxDamage,
            currentMinDamage);
        isCharging = false;
        currentChargeLevel = 0;
        indicator.SetIndicatorActive(false);
    }

    public void ShootFinishCallback()
    {
        Moveable = true;
        MyAnimator.SetBool("isCharing", false);
    }

    public override void OnClick_Second()
    {
        if (isDead)
            return;
        Roll(GetFaceTo());
    }

    public override void OnDragUpdate_Second(Vector2 pos)
    {
        if (isDead)
            return;
        indicator.SetIndicatorActive(true);
        indicator.SetDirection(pos);
        indicator.SetLength(2);
    }

    public override void OnPressEnd_Second()
    {
        if (isDead)
            return;
        indicator.SetIndicatorActive(false);
    }

    //翻滚
    public override void OnDragEnd_Second(Vector2 pos)
    {
        if (isDead)
            return;
        indicator.SetIndicatorActive(false);
        Roll(pos);
    }

    private void Roll(Vector2 dir)
    {
        if (!Moveable)
            return;
        Moveable = false;
        AimTo(dir);
        MyAnimator.SetTrigger("Roll");
        MyRigidbody.velocity = (new Vector3(dir.x, 0, dir.y).normalized * RollSpeed);
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

    public override void Hurt(float damage)
    {
        if (currentHP <= 0)
            return;
        base.Hurt(damage);
        ChargeBreak();
        MyAnimator.SetTrigger("GetHurt");
        if (archerInfoUI)
            archerInfoUI.SetHP(currentHP);
        StopMove();
        Moveable = false;

    }

    public override void Dead()
    {
        base.Dead();
        StopMove();
        Moveable = false;
        MyAnimator.SetBool("Deadb", true);
    }

    public void HurtEndCallback()
    {
        Moveable = true;
    }

    //public override void Start()
    //{
    //    base.Start();
    //}


}
