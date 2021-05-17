using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceGround : MonoBehaviour
{
    private Vector3 fall;
    private float speed = 1f;

    private void Start()
    {
        fall = new Vector3(0f, -0.1f);
    }

    void FixedUpdate()
    {
        transform.position += fall * Time.fixedDeltaTime * speed;      
    }
}
