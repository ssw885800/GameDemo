using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLIghtController : MonoBehaviour
{
    public Transform GetLight; //����O���C������
    public Light light;         //�w�qLight�իئW��light

    [SerializeField, Header("����Ѽ�")]
    [Range(0f,100f)]public float LightRange;    //�Ӯg�Z��
    public Vector2 LightSpotAngle;//�Ӯg����(���Ӫ����j�p)
    // Start is called before the first frame update
    void Start()
    {
        LightSpotAngle.x = 25f;
        LightSpotAngle.y = 30f;
        LightRange = 10f;
        LightSetting();
    }

    // Update is called once per frame
    void Update()
    {
        light = GetLight.GetComponent<Light>();
        LightSetting();
    }
    void LightSetting()
    {
        light.range = LightRange;                   //��������Ӯg�Z��
        light.innerSpotAngle = LightSpotAngle.x;    //�����������j�p
        light.spotAngle = LightSpotAngle.y;         //��������~��j�p
    }
}
