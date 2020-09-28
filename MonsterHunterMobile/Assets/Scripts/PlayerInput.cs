using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("=====key setting=====")]
    //前后左右上下
    private KeyCode MoveUP;
    private KeyCode MoveDown;
    private KeyCode MoveLeft;
    private KeyCode MoveRight;
    private KeyCode MoveRise;
    private KeyCode MoveFall;
    //功能键
    private KeyCode frighten;     //吓唬
    private KeyCode possess;      //附身

    [Header("=====output signal=====")]
    public float Dup;
    public float Dright;
    public float Drise;
    public float Dmag;

    public Vector3 Dvec;

    //1.pressing signal   按压式

    //2.trigger once signal  一次性触发
    public bool canFrighten;
    public bool possessing;
    public bool fright;
    //3.double trigger 


    public bool inputEnabled = true;
    [Header("=====others=====")]
    private float DupTarget;
    private float DrightTarget;
    private float DriseTarget;
    private float VelocityDup;
    private float VelocityDright;
    private float VelocityDrise;
    void Start()
    {
        //键位设置
        MoveUP=KeyCode.W;
        MoveDown=KeyCode.S;
        MoveLeft=KeyCode.A;
        MoveRight=KeyCode.D;
        MoveRise=KeyCode.J;
        MoveFall=KeyCode.K;
        frighten=KeyCode.U;     
        possess=KeyCode.I;

        EventCenter.GetInstance().AddEventListener<bool>("canFrighten",FrightenUpdate);
    }

    // Update is called once per frame
    void Update()
    {
        //输入
        DupTarget = (Input.GetKey(MoveUP) ? 1.0f : 0) - (Input.GetKey(MoveDown) ? 1.0f : 0);
        DrightTarget = (Input.GetKey(MoveRight) ? 1.0f : 0) - (Input.GetKey(MoveLeft) ? 1.0f : 0);
        DriseTarget = (Input.GetKey(MoveRise) ? 1.0f : 0) - (Input.GetKey(MoveFall) ? 1.0f : 0);
 
        possessing = Input.GetKeyDown(possess);
        //fright = Input.GetKeyDown(frighten);
        if (canFrighten)
        {
            if (fright = Input.GetKeyDown(frighten))
                Debug.Log("frighten");
        }


        if (!inputEnabled)
        {
            DupTarget = 0;
            DrightTarget = 0;
        }
        //平滑数值
        Dup = Mathf.SmoothDamp(Dup, DupTarget, ref VelocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, DrightTarget, ref VelocityDright, 0.1f);
        Drise= Mathf.SmoothDamp(Drise, DriseTarget, ref VelocityDrise, 0.1f);

    }
    /// <summary>
    /// 监听能否吓唬
    /// </summary>
    public void FrightenUpdate(bool canFright)
    {
        canFrighten = canFright;
    }

    //销毁时删除监听事件所触发的委托事件
    void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener<bool>("canFrighten", FrightenUpdate);
    }

    public Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
        return output;
    }
    
}
