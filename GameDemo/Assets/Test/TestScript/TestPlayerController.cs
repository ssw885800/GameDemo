using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class TestPlayerController : MonoBehaviour
{
    private CharacterController Controller;//獲取物件上的角色控制器
    
    [Header("移動參數")]
    [Range(5f,100f)]public float MoveSpeed;//移動速度
    [Range(5f, 20f)] public float JumpHigh;//跳躍高度
    public float Gravity = -9.8f;   //重力
    private Vector3 Velocity = Vector3.zero;    //3維向量變數

    [Header("地面檢測")]
    public Transform GroundCheck;   //獲取地面檢測體
    public float CheckRadius = 0.2f;//檢測範圍
    private bool IsGround;          //是否在地上
    public LayerMask layerMask;     //獲取層級遮罩


    // Start is called before the first frame update
    void Start()
    {
        Controller = transform.GetComponent<CharacterController>(); //獲取玩家身上的控制器組件
    }

    // Update is called once per frame
    void Update()
    {
        MoveMoment();//每幀執行移動控制器
        Flip();
    }
    void MoveMoment()     //移動控制器
    {
        IsGround = Physics.CheckSphere(GroundCheck.position, CheckRadius, layerMask);

        if (IsGround && Velocity.y < 0)                 //判斷是否在地面，如果是，將落下的動量歸零
        {
            Velocity.y = 0;
        }
        if (IsGround && Input.GetButtonDown("Jump"))    //跳躍
        {
            Velocity.y += Mathf.Sqrt(JumpHigh * -2 * Gravity);
        }

        var horizontal = Input.GetAxis("Horizontal");   //獲取InputManager的水平輸入
        var move = transform.forward * MoveSpeed * horizontal * Time.deltaTime;
        Controller.Move(move);

        Velocity.y += Gravity * Time.deltaTime;     //重力加速度
        Controller.Move(Velocity * Time.deltaTime);
    }
    void Flip()     //透過縮放Z軸實現角色翻轉
    {
        var horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0.1f)
        {
             transform.localScale = new Vector3(1, 1, 1);
        }
        if(horizontal < -0.1f)
        {
            transform.localScale = new Vector3(1, 1, -1);
        }
    }
}
