using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class TestPlayerController03 : MonoBehaviour
{
    private CharacterController Controller;//�������W�����ⱱ�

    [Header("���ʰѼ�")]
    [Range(5f, 100f)] public float MoveSpeed;//���ʳt��
    [Range(5f, 20f)] public float JumpHigh;//���D����
    public float Gravity = -9.8f;   //���O
    private Vector3 Velocity = Vector3.zero;    //3���V�q�ܼ�
    private Vector2 movement;

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
        GravityAdd();
    
    }
    public void PlayerMoveInput(InputAction.CallbackContext callbackContext)    //����s��J�t�Ϊ����ʼƭ�
    {
        movement = callbackContext.ReadValue<Vector2>();
        var move = transform.forward * MoveSpeed * movement.x * Time.deltaTime;
        Controller.Move(move);
        Flip();
        Debug.Log(movement);
    }
    void GravityAdd()   //���a���O
    {
        IsGround = Physics.CheckSphere(GroundCheck.position, CheckRadius, layerMask);
        if (IsGround && Velocity.y < 0)                 //�P�_�O�_�b�a���A�p�G�O�A�N���U���ʶq�k�s
        {
            Velocity.y = 0;
        }
        Velocity.y += Gravity * Time.deltaTime;     //���O�[�t��
        Controller.Move(Velocity * Time.deltaTime);
    }   
    void Flip()     //�z�L�Y��Z�b��{����½��
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
