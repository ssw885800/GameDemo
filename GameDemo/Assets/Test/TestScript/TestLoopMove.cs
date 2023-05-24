using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class TestLoopMove : MonoBehaviour
{
    [SerializeField]
    public Transform[] transforms;
    int a;
    float Speed = 3;
    // Start is called before the first frame update
    void Start()
    {
        a = 0;
      
    }

    // Update is called once per frame
    void Update()
    {
        MoveMment();
        if (a > 1)a = 0;
    }
    void MoveMment()
    {

        transform.position = Vector3.MoveTowards(transform.position, transforms[a].position,Speed * Time.deltaTime);
        if (transform.position.x == transforms[a].position.x)
        {
            a++;
        }
    }
}
