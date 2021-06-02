using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePosition : MonoBehaviour
{
    public bool onCollisionWithPlayer = false;

    public Vector3 movePositionWith;
    public float speed = 1f;

    private bool collided= false;

    void FixedUpdate()
    {
        if (GameManager.Instance.paused) return;

        if (!onCollisionWithPlayer || collided)
        {
            transform.position += movePositionWith * Time.fixedDeltaTime * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Tag.Player.ToString() && !collided)
        {
            collided = true;
        }
    }
}
