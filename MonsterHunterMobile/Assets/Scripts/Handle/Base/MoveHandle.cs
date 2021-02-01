using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveHandle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler
{
    public GameObject HandleBackground;
    public GameObject HandlePoint;
    public string TargetID;

    [SerializeField]
    protected bool isOnControl = false;
    [SerializeField]
    protected Vector2 HandleVector = Vector2.zero;
    [SerializeField]
    protected float HandleVectorMagnitude = 0;

    protected virtual void OperationTrigger()
    {
        MoveEventCenter.GetInstance().EventTrigger(TargetID, HandleVector);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CheckInCircle(eventData.position, HandlePoint))
        {
            isOnControl = true;
            OperationTrigger();
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isOnControl)
        {
            isOnControl = false;
            SetToOrigin(HandlePoint);
            HandleVector = Vector2.zero;
            HandleVectorMagnitude = 0;
            OperationTrigger();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isOnControl)
        {
            if (CheckInCircle(eventData.position, HandleBackground))
            {
                SetToPos(eventData.position, HandlePoint);
            }
            else
            {
                SetToPos(GetCircleEdge(eventData.position, HandleBackground), HandlePoint);
            }
            HandleVector = GetCircleAreaPos(GetScreenPos(HandlePoint), HandleBackground);
            HandleVectorMagnitude = HandleVector.magnitude;
            OperationTrigger();
        }
        else
        {
            if (CheckInCircle(eventData.position, HandlePoint))
            {
                isOnControl = true;
                OperationTrigger();
            }
        }
    }


    //protected Vector3 PosToBase(Vector3 worldPos, GameObject baseObj)
    //{
    //    return Vector3.zero;
    //}

    /// <summary>
    /// 获得一个ui组件的世界大小
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    protected Vector3 GetRealSize(GameObject obj)
    {
        RectTransform rtr = obj.GetComponent<RectTransform>();
        Rect r1 = rtr.rect;
        Vector3 v1 = new Vector3(r1.width, r1.height, 0);
        Vector3 v2 = rtr.lossyScale;
        return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
    }

    /// <summary>
    /// 获得一个ui的世界坐标
    /// </summary>
    /// <param name="uiObj"></param>
    /// <returns></returns>
    protected Vector3 GetScreenPos(GameObject uiObj)
    {
        return uiObj.GetComponent<RectTransform>().position;
    }

    /// <summary>
    /// 检测一个位置是否在圆形ui中
    /// </summary>
    /// <param name="screenPos"></param>
    /// <param name="uiObj"></param>
    /// <returns></returns>
    protected bool CheckInCircle(Vector3 screenPos, GameObject uiObj)
    {
        Vector3 uipos = GetScreenPos(uiObj);
        Vector3 size = GetRealSize(uiObj);
        Vector3 disvec = uipos - screenPos;
        if (disvec.magnitude - size.x / 2 < 0.001)
            return true;
        return false;
    }

    protected Vector3 GetCircleEdge(Vector3 screenPos, GameObject uiObj)
    {
        Vector3 uipos = GetScreenPos(uiObj);
        Vector3 dir = screenPos - uipos;
        return GetRealSize(uiObj).x / 2 * dir.normalized + GetScreenPos(uiObj);
    }

    protected Vector2 GetCircleAreaPos(Vector2 screenPos, GameObject uiObj)
    {
        Vector2 uipos = GetScreenPos(uiObj);
        Vector2 uiSize = GetRealSize(uiObj);
        Vector2 dir = screenPos - uipos;
        return new Vector2(dir.x / uiSize.x * 2, dir.y / uiSize.y * 2);
    }

    protected void SetToPos(Vector3 screenPos, GameObject uiObj)
    {
        uiObj.GetComponent<RectTransform>().position = screenPos;
    }

    protected void SetToOrigin(GameObject uiObj)
    {
        uiObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

}
