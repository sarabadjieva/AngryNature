using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject goHitEffect;
    public Rigidbody2D rb2D;

    [SerializeField] private float speed;

    private void Awake()
    {
        if (rb2D == null)
        {
            rb2D = GetComponent<Rigidbody2D>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != Tag.Player.ToString())
        {
            GameObject hitFX = Instantiate(goHitEffect, transform.position, Quaternion.identity);
            Destroy(hitFX, 1f);

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != Tag.Player.ToString())
        {
            SpawnedEnemy enemy;
            other.gameObject.TryGetComponent<SpawnedEnemy>(out enemy);

            if (enemy != null)
            {
                enemy.TakeShot();
            }

            GameObject hitFX = Instantiate(goHitEffect, transform.position, Quaternion.identity);
            Destroy(hitFX, 1f);

            Destroy(gameObject);
        }
    }
}
