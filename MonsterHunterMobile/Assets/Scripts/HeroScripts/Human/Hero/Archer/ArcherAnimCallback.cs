using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArcherAnimCallback : MonoBehaviour
{
    public UnityEvent ShootCallback;
    public UnityEvent RollStartCallback;
    public UnityEvent RollMidCallback;
    public UnityEvent RollEndCallback;
    public UnityEvent HurtEndCallback;

    private void ShootEvent()
    {
        if (ShootCallback != null)
            ShootCallback.Invoke();
    }

    private void RollStartEvent()
    {
        if (RollStartCallback != null)
            RollStartCallback.Invoke();
    }

    private void RollMidEvent()
    {
        if (RollMidCallback != null)
            RollMidCallback.Invoke();
    }

    private void RollEndEvent()
    {
        if (RollEndCallback != null)
            RollEndCallback.Invoke();
    }

    private void HurtEndEvent()
    {
        if (HurtEndCallback != null)
            HurtEndCallback.Invoke();
    }
}
