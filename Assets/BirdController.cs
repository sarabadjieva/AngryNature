using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    private bool hasReachedEnd = false;

    public Vector3 movePositionWith;
    public float speed = 1f;

    void FixedUpdate()
    {
       /* if (GameManager.Instance.paused) return;

        if (!hasReachedEnd && (transform.localPosition.x == 150 || transform.localPosition.x <= 0))
        {
            movePositionWith = -movePositionWith;
            transform.rotation =  Quaternion.Euler(0, transform.rotation.y + 180f, 0f);
            hasReachedEnd = true;
        }

        transform.position += movePositionWith * Time.fixedDeltaTime * speed;
    }*/
}
