using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Human
{
    public string HeroID;
    public virtual void Move(Vector2 dir)
    {
        if (!Moveable)
            return;
        movingDir = dir.normalized;
        if (dir.magnitude < 0.01f)    
        {
            isMoving = false;
            MyRigidbody.velocity = Vector3.zero;
            MyAnimator.SetBool("isMoving", false);
        }
        else
        {
            isMoving = true;
            MyAnimator.SetBool("isMoving", true);
        }
            
    }

    public virtual void StopMove()
    {
        isMoving = false;
        MyAnimator.SetBool("isMoving", false);
        MyRigidbody.velocity = Vector3.zero;
    }

    public Vector3 GetEuler(Vector2 dir)
    {
        Vector3 euler = Vector3.zero;
        float arccos = Mathf.Acos(dir.x) / Mathf.PI * 90;
        if (dir.y < 0)
            arccos = -arccos;
        euler.y = arccos;
        return euler;
    }

    public void AimTo(Vector2 dir)
    {
        if (isDead)
            return;
        this.transform.forward = new Vector3(dir.x, 0, dir.y);
    }

    protected Vector2 GetFaceTo()
    {
        return new Vector2(this.transform.forward.x, this.transform.forward.z).normalized;
    }

    protected void Update_Move()
    {
        if (isMoving && Moveable && !isDead)
        {
            AimTo(movingDir);
            MyRigidbody.velocity = new Vector3(movingDir.x, 0, movingDir.y) * MoveSpeed;
        }
    }

    protected virtual void Update()
    {
        Update_Move();
    }

    public override void Start()
    {
        base.Start();
        MoveEventCenter.GetInstance().AddEventListener<Vector2>(HeroID, Move);
        ActionEventCenter.GetInstance().AddEventListener<ControlMsg>(HeroID, ControllEventTigger);
    }

    [SerializeField]
    protected bool isMoving = false;
    [SerializeField]
    protected bool Moveable = true;
    [SerializeField]
    protected Vector2 movingDir = Vector2.zero;

    public float MoveSpeed = 10f;

    public Animator MyAnimator;
    public Rigidbody MyRigidbody;
    public GameObject hitter;

    public override void Dead()
    {
        base.Dead();
        hitter.SetActive(false);
    }

    #region Events
    public virtual void ControllEventTigger(ControlMsg msg)
    {
        switch (msg.index)
        {
            case InputHandleIndex.First:
                switch(msg.type)
                {
                    case EventType.OnClick:
                        OnClick_First();
                        break;
                    case EventType.OnPressBegin:
                        OnPressBegin_First();
                        break;
                    case EventType.OnPressEnd:
                        OnPressEnd_First();
                        break;
                    case EventType.OnDragUpdate:
                        OnDragUpdate_First(msg.handleVector);
                        break;
                    case EventType.OnDragEnd:
                        OnDragEnd_First(msg.handleVector);
                        break;
                }
                break;
            case InputHandleIndex.Second:
                switch (msg.type)
                {
                    case EventType.OnClick:
                        OnClick_Second();
                        break;
                    case EventType.OnPressBegin:
                        OnPressBegin_Second();
                        break;
                    case EventType.OnPressEnd:
                        OnPressEnd_Second();
                        break;
                    case EventType.OnDragUpdate:
                        OnDragUpdate_Second(msg.handleVector);
                        break;
                    case EventType.OnDragEnd:
                        OnDragEnd_Second(msg.handleVector);
                        break;
                }
                break;
        }
    }
    #region First
    public virtual void OnClick_First() { }

    public virtual void OnPressBegin_First() { }

    public virtual void OnPressEnd_First() { }

    public virtual void OnDragUpdate_First(Vector2 pos) { }

    public virtual void OnDragEnd_First(Vector2 pos) { }
    #endregion First

    #region Second
    public virtual void OnClick_Second() { }

    public virtual void OnPressBegin_Second() { }

    public virtual void OnPressEnd_Second() { }

    public virtual void OnDragUpdate_Second(Vector2 pos) { }

    public virtual void OnDragEnd_Second(Vector2 pos) { }
    #endregion Second
    #endregion Events

}
