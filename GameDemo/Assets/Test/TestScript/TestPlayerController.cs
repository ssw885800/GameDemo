using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class TestPlayerController : MonoBehaviour
{
    private CharacterController Controller;//�������W�����ⱱ�
    
    [Header("���ʰѼ�")]
    [Range(5f,100f)]public float MoveSpeed;//���ʳt��
    [Range(5f, 20f)] public float JumpHigh;//���D����
    public float Gravity = -9.8f;   //���O
    private Vector3 Velocity = Vector3.zero;    //3���V�q�ܼ�

    [Header("�a���˴�")]
    public Transform GroundCheck;   //����a���˴���
    public float CheckRadius = 0.2f;//�˴��d��
    private bool IsGround;          //�O�_�b�a�W
    public LayerMask layerMask;     //����h�žB�n


    // Start is called before the first frame update
    void Start()
    {
        Controller = transform.GetComponent<CharacterController>(); //������a���W������ե�
    }

    // Update is called once per frame
    void Update()
    {
        MoveMoment();//�C�V���沾�ʱ��
        Flip();
    }
    void MoveMoment()     //���ʱ��
    {
        IsGround = Physics.CheckSphere(GroundCheck.position, CheckRadius, layerMask);

        if (IsGround && Velocity.y < 0)                 //�P�_�O�_�b�a���A�p�G�O�A�N���U���ʶq�k�s
        {
            Velocity.y = 0;
        }
        if (IsGround && Input.GetButtonDown("Jump"))    //���D
        {
            Velocity.y += Mathf.Sqrt(JumpHigh * -2 * Gravity);
        }

        var horizontal = Input.GetAxis("Horizontal");   //���InputManager��������J
        var move = transform.forward * MoveSpeed * horizontal * Time.deltaTime;
        Controller.Move(move);

        Velocity.y += Gravity * Time.deltaTime;     //���O�[�t��
        Controller.Move(Velocity * Time.deltaTime);
    }
    void Flip()     //�z�L�Y��Z�b��{����½��
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
