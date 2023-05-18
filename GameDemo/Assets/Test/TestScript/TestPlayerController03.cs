using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class TestPlayerController03 : MonoBehaviour
{
    private CharacterController Controller;//獲取物件上的角色控制器

    [Header("移動參數")]
    [Range(5f, 100f)] public float MoveSpeed;//移動速度
    [Range(5f, 20f)] public float JumpHigh;//跳躍高度
    public float Gravity = -9.8f;   //重力
    private Vector3 Velocity = Vector3.zero;    //3維向量變數
    private Vector2 movement;

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
        GravityAdd();
    
    }
    public void PlayerMoveInput(InputAction.CallbackContext callbackContext)    //獲取新輸入系統的移動數值
    {
        movement = callbackContext.ReadValue<Vector2>();
        var move = transform.forward * MoveSpeed * movement.x * Time.deltaTime;
        Controller.Move(move);
        Flip();
        Debug.Log(movement);
    }
    void GravityAdd()   //玩家重力
    {
        IsGround = Physics.CheckSphere(GroundCheck.position, CheckRadius, layerMask);
        if (IsGround && Velocity.y < 0)                 //判斷是否在地面，如果是，將落下的動量歸零
        {
            Velocity.y = 0;
        }
        Velocity.y += Gravity * Time.deltaTime;     //重力加速度
        Controller.Move(Velocity * Time.deltaTime);
    }   
    void Flip()     //透過縮放Z軸實現角色翻轉
    {
        if (movement.x > 0.1f)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        if (movement.x < -0.1f)
        {
            transform.localScale = new Vector3(1,1,-1);
        }
    }
}
