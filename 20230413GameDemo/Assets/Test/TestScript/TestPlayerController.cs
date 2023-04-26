using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TestPlayerController : MonoBehaviour
{
    
    public Rigidbody PlayerRigidbody;


    [Header("���ʰѼ�")]
    [Range(5f,100f)]public float MoveSpeed;
    [Range(5f, 100f)] public float JumpHigh;
    float MoveDir;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()     //����
    {
        MoveDir = Input.GetAxis("Horizontal");  //���o���k���ʫ���
        PlayerRigidbody.velocity = new Vector2(MoveDir * MoveSpeed , PlayerRigidbody.velocity.y);      //�@�벾�t
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            PlayerRigidbody.velocity = new Vector2();
        }
    }
}
