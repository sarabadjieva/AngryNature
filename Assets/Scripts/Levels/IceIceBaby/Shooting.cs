using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform trFirePointRight;
    public Transform trFirePointLeft;
    public GameObject goBulletPrefab;

    public float bulletForce = 20f;

    private bool shouldShootFromRight = true;

    void Update()
    {
        if (GameManager.Instance.paused) return;

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(goBulletPrefab,
            shouldShootFromRight ? trFirePointRight.position : trFirePointLeft.position,
            shouldShootFromRight ? trFirePointRight.rotation : trFirePointLeft.rotation);

        Rigidbody2D rb2D = bullet.GetComponent<Rigidbody2D>();
        rb2D.AddForce((shouldShootFromRight ? trFirePointRight.up : trFirePointLeft.up) * bulletForce, ForceMode2D.Impulse);

        shouldShootFromRight = !shouldShootFromRight;
    }
}
