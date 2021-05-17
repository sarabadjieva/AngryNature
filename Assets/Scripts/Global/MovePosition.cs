using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePosition : MonoBehaviour
{
    public bool onCollisionWithPlayer = false;

    public Vector3 movePositionWith;
    public float speed = 1f;

    void FixedUpdate()
    {
        transform.position += movePositionWith * Time.fixedDeltaTime * speed;
    }
}
