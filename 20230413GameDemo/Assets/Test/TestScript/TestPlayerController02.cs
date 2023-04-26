using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController02 : MonoBehaviour
{
    //�����ĤT�H�ٵ����H�έ����������ʤ覡�m��
    private CharacterController Controller;//�������W�����ⱱ�
    [Header("���ʰѼ�")]
    public float MoveSpeed ;        //���ʳt��
    public float RotateSpeed;       //����t��
    public float Gravity = -9.8f;   //���O
    private Vector3 Velocity = Vector3.zero;

    [Header("���D�Ѽ�")]
    public float JumpHeight = 3f;

    [Header("�a���˴�")]
    public Transform GroundCheck;   //����a���˴���
    public float CheckRadius = 0.2f;//�˴��d��
    private bool IsGround;          //�O�_�b�a�W
    public LayerMask layerMask;     //����h�žB�n
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
    void Move01()//�ĤT�H�ٲ��ʤ覡�Aw�Bs����e��;a�Bd�����V(����)
    {
        IsGround = Physics.CheckSphere(GroundCheck.position, CheckRadius,layerMask);
        if (IsGround && Velocity.y < 0)
        {
            Velocity.y = 0;
        }
        if (IsGround && Input.GetButtonDown("Jump"))//���D
        {
            Velocity.y += Mathf.Sqrt(JumpHeight * -2 * Gravity);
        }
        var horizontal = Input.GetAxis("Horizontal");   //���InputManager��������J
        var vertical = Input.GetAxis("Vertical");       //���InputManager��������J

        var move = transform.forward * MoveSpeed * vertical * Time.deltaTime;
        Controller.Move(move);

        Velocity.y += Gravity * Time.deltaTime;
        Controller.Move(Velocity * Time.deltaTime);

        transform.Rotate(Vector3.up,horizontal * RotateSpeed);
    }
    void Move02()//���������ʤ覡�Aw�Bs����W�U;a�Bd����k
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

    private void OnControllerColliderHit(ControllerColliderHit hit)//���ⱱ��P���餬�ʪ���k
    {
        if (hit.transform.CompareTag("Finish"))
        {
            hit.rigidbody.AddForce(transform.forward * MoveSpeed);
        }
    }


}

