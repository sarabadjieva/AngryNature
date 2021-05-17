using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public bool followHorizontal;
    public bool followVertical;

    private float startPosX;
    private float startPosY;

    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector2(
            followHorizontal? Camera.main.transform.position.x : startPosX,
            followVertical ? Camera.main.transform.position.y : startPosY);
    }
}
