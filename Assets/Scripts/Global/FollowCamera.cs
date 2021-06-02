using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform trTarget;

    public bool followHorizontal;
    public bool followVertical;

    public float offsetY = 0f;

    private float startPosX;
    private float startPosY;

    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.paused) return;
        
        if (trTarget == null)
        {
            transform.position = new Vector2(
                followHorizontal ? Camera.main.transform.position.x : startPosX,
                followVertical ? Camera.main.transform.position.y + offsetY : startPosY);
        }
        else
        {
            transform.position = new Vector2(
                followHorizontal ? trTarget.position.x : startPosX,
                followVertical ? trTarget.position.y + offsetY : startPosY);
        }

    }
}
