using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStrongLight : MonoBehaviour
{
    [SerializeField,Header("¥ú·Óªº½d³ò")]
    [Range(0f,100f)]public float CubeX;
    [Range(0f, 100f)] public float CubeY = 5f;
    private float CubeZ = 10f;
    public Transform transform;
    private Vector3 CubeSize;
    private Vector3 CubePosition;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

        CubeSize = new Vector3(CubeX, CubeY, CubeZ);
        CubePosition = transform.position;
    }
    private void OnDrawGizmos()
    {
      
        Gizmos.color =new Color(1,0,0,0.5f);
        Gizmos.DrawCube(CubePosition, CubeSize);
    }
}
