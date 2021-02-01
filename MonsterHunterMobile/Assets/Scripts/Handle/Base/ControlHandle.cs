using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public enum InputHandleIndex { First, Second}
public enum EventType { OnClick, OnPressBegin, OnPressEnd, OnDragUpdate, OnDragEnd }

public struct ControlMsg
{
    public InputHandleIndex index;
    public EventType type;
    public Vector2 handleVector;
}

public class ControlHandle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler
{
    public GameObject HandleBackground;
    public GameObject HandlePoint;
    public string TargetID;
    public InputHandleIndex HandleIndex;

    public float ClickTimeRange = 0.3f;
    public float ClickRadius = 0.3f;

    [SerializeField]
    protected float LastClickTime = 0f;
    [SerializeField]
    protected bool isOnControl = false;
    [SerializeField]
    protected bool isClick = false;
    [SerializeField]
    protected Vector2 HandleVector = Vector2.zero;
    [SerializeField]
    protected float HandleVectorMagnitude = 0;

    #region Events
    public virtual void OnClick()
    {
        //Debug.Log(gameObject.name + "Base Click");
        ActionEventCenter.GetInstance().EventTrigger(TargetID, 
            new ControlMsg 
            {
                index = HandleIndex, 
                handleVector = HandleVector, 
                type = EventType.OnClick 
            });

    }

    public virtual void OnPressBegin()
    {
        //Debug.Log(gameObject.name + "Base Press Begin");
        ActionEventCenter.GetInstance().EventTrigger(TargetID,
        new ControlMsg
        {
            index = HandleIndex,
            handleVector = HandleVector,
            type = EventType.OnPressBegin
        });
    }

    public virtual void OnPressEnd()
    {
        //Debug.Log(gameObject.name + "Base Press End");
        ActionEventCenter.GetInstance().EventTrigger(TargetID,
        new ControlMsg
        {
            index = HandleIndex,
            handleVector = HandleVector,
            type = EventType.OnPressEnd
        });
    }

    public virtual void OnDragUpdate()
    {
        //Debug.Log(gameObject.name + "Base Drag Update");
        ActionEventCenter.GetInstance().EventTrigger(TargetID,
        new ControlMsg
        {
            index = HandleIndex,
            handleVector = HandleVector,
            type = EventType.OnDragUpdate
        });
    }

    public virtual void OnDragEnd()
    {
        //Debug.Log(gameObject.name + "Base Drag End");
        ActionEventCenter.GetInstance().EventTrigger(TargetID,
            new ControlMsg
            {
                index = HandleIndex,
                handleVector = HandleVector,
                type = EventType.OnDragEnd
            });
    }

    #endregion Events

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log(this.gameObject.GetComponent<RectTransform>().lossyScale);
        //Debug.Log(this.gameObject.GetComponent<RectTransform>().position);
        //Debug.Log(eventData.position);

        if (CheckInCircle(eventData.position, HandlePoint))
        {
            isOnControl = true;
            LastClickTime = Time.unscaledTime;
            //HandleBackground.SetActive(true);
            isClick = true;
        }
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isOnControl)
        {
            isOnControl = false;
            if (isClick)
            {
                //Debug.Log("ClickRelease");
                OnClick();
            }
            else
            {
                if (HandleVectorMagnitude > ClickRadius)
                {
                    //Debug.Log("DragRelease");
                    OnDragEnd();
                }
                else
                {
                    //Debug.Log("PressRelease");
                    OnPressEnd();
                }
                
            }
            HandleBackground.SetActive(false);
            HandlePoint.SetActive(false);
            SetToOrigin(HandlePoint);
            HandleVector = Vector2.zero;
            HandleVectorMagnitude = 0;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isOnControl)
        {
            //显示触点
            if (!HandlePoint.activeSelf)
                HandlePoint.SetActive(true);

            if (!HandleBackground.activeSelf)
                HandleBackground.SetActive(true);
            //Debug.Log(CheckInCircle(eventData.position, HandleBackground));

            //设置触点的位置
            if (CheckInCircle(eventData.position, HandleBackground))
            {
                SetToPos(eventData.position, HandlePoint);
            }
            else
            {
                SetToPos(GetCircleEdge(eventData.position, HandleBackground), HandlePoint);
            }
            //Debug.Log("on drag");
            HandleVector = GetCircleAreaPos(GetScreenPos(HandlePoint), HandleBackground);
            HandleVectorMagnitude = HandleVector.magnitude;

            //拖拽事件
            
            if (HandleVectorMagnitude > ClickRadius)
            {
                isClick = false;
                OnDragUpdate();
            }
                

            //if (DragEvent != null)
            //    DragEvent(InputType.Drag, HandleVector);
        }
        //else
        //{
        //    if (CheckInCircle(eventData.position, HandlePoint))
        //    {
        //        isOnControl = true;
        //        HandleBackground.SetActive(true);
        //    }
        //}
    }

    protected void Update()
    {
        if (isOnControl && isClick)
        {
            if (Time.unscaledTime - LastClickTime > ClickTimeRange)
            {
                isClick = false;
                OnPressBegin();
            }
                
        }
    }

    //protected Vector3 PosToBase(Vector3 worldPos, GameObject baseObj)
    //{
    //    return Vector3.zero;
    //}

    #region Tools

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

    #endregion Tools

    //protected Vector3 GetViewPoint(Vector3 pos)
    //{
    //    return Camera.main.ScreenToViewportPoint(pos);
    //}

    //protected Vector2 ChangeInBase(Vector2 viewPoint, GameObject objbase)//将高一级的ViewPoint 转入低一级
    //{
    //    //获取RectTransform
    //    RectTransform objbase_rt = objbase.GetComponent<RectTransform>();

    //    float objbase_x = objbase_rt.anchorMax.x - objbase_rt.anchorMin.x;
    //    float objbase_y = objbase_rt.anchorMax.y - objbase_rt.anchorMin.y;
    //    //获取ViewPoint转换至objbase中的对应的View点

    //    return new Vector2((viewPoint.x - objbase_rt.anchorMin.x) / objbase_x, (viewPoint.y - objbase_rt.anchorMin.y) / objbase_y);

    //}

    //protected void SetToViewPoint(Vector2 viewPoint, GameObject objBase, GameObject objSet, bool model)
    //{
    //    RectTransform objSet_tr = objSet.GetComponent<RectTransform>();
    //    float objSet_x = objSet_tr.anchorMax.x - objSet_tr.anchorMin.x;
    //    float objSet_y = objSet_tr.anchorMax.y - objSet_tr.anchorMin.y;
    //    Vector2 topanel_Point;
    //    if (model)
    //    {
    //        topanel_Point = ChangeInBase(viewPoint, objBase);
    //    }
    //    else
    //    {
    //        topanel_Point = viewPoint;
    //    }
    //    objSet_tr.anchorMin = new Vector2(topanel_Point.x - objSet_x / 2, topanel_Point.y - objSet_y / 2);
    //    objSet_tr.anchorMax = new Vector2(topanel_Point.x + objSet_x / 2, topanel_Point.y + objSet_y / 2);
    //    //objSet_tr.position = viewPoint;
    //    Debug.Log(viewPoint);
    //}
}
