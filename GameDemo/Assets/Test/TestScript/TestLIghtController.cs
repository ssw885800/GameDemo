using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLIghtController : MonoBehaviour
{
    public Transform GetLight; //獲取燈光遊戲物件
    public Light light;         //定義Light組建名為light

    [SerializeField, Header("控制參數")]
    [Range(0f,100f)]public float LightRange;    //照射距離
    public Vector2 LightSpotAngle;//照射角度(光照的圓圈大小)
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
        light.range = LightRange;                   //控制光的照射距離
        light.innerSpotAngle = LightSpotAngle.x;    //控制光的內圈大小
        light.spotAngle = LightSpotAngle.y;         //控制光的外圈大小
    }
}
