using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController02 : MonoBehaviour
{
    //此為第三人稱視角以及俯視角的移動方式練習
    private CharacterController Controller;//獲取物件上的角色控制器
    [Header("移動參數")]
    public float MoveSpeed ;        //移動速度
    public float RotateSpeed;       //旋轉速度
    public float Gravity = -9.8f;   //重力
    private Vector3 Velocity = Vector3.zero;

    [Header("跳躍參數")]
    public float JumpHeight = 3f;

    [Header("地面檢測")]
    public Transform GroundCheck;   //獲取地面檢測體
    public float CheckRadius = 0.2f;//檢測範圍
    private bool IsGround;          //是否在地上
    public LayerMask layerMask;     //獲取層級遮罩
    // Start is called before the first frame update
    void Start()
    {
        Controller = transform.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move01();
        //Move02();
    }
    void Move01()//第三人稱移動方式，w、s控制前後;a、d控制方向(角度)
    {
        IsGround = Physics.CheckSphere(GroundCheck.position, CheckRadius,layerMask);
        if (IsGround && Velocity.y < 0)
        {
            Velocity.y = 0;
        }
        if (IsGround && Input.GetButtonDown("Jump"))//跳躍
        {
            Velocity.y += Mathf.Sqrt(JumpHeight * -2 * Gravity);
        }
        var horizontal = Input.GetAxis("Horizontal");   //獲取InputManager的水平輸入
        var vertical = Input.GetAxis("Vertical");       //獲取InputManager的垂直輸入

        var move = transform.forward * MoveSpeed * vertical * Time.deltaTime;
        Controller.Move(move);

        Velocity.y += Gravity * Time.deltaTime;
        Controller.Move(Velocity * Time.deltaTime);

        transform.Rotate(Vector3.up,horizontal * RotateSpeed);
    }
    void Move02()//府視角移動方式，w、s控制上下;a、d控制左右
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var direction = new Vector3(horizontal, 0, vertical).normalized;
        var move = direction* MoveSpeed * Time.deltaTime;
        Controller.Move(move);

        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        var point = Input.mousePosition - playerScreenPoint;
        var angle = Mathf.Atan2(point.x, point.y) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)//角色控制器與鋼體互動的方法
    {
        if (hit.transform.CompareTag("Finish"))
        {
            hit.rigidbody.AddForce(transform.forward * MoveSpeed);
        }
    }


}

