using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedEnemy : MonoBehaviour
{
    public float speed;

    [Space]
    public Transform trTarget;
    public Transform trTargetExit;

    public Animator personAnimator;

    [SerializeField]private Sprite sprite;


    [Header("Spawn Directions")]
    public bool spawnFromTop;
    public bool spawnFromBottom;
    public bool spawnFromLeft;
    public bool spawnFromRight;

    [HideInInspector] public float spriteWidth;
    [HideInInspector] public float spriteHeight;

    private int health = 3;

    private void Awake()
    {
        if (sprite == null)
        {
            sprite = this.GetComponent<SpriteRenderer>().sprite;
        }

        spriteWidth = sprite.bounds.size.x/2;
        spriteHeight = sprite.bounds.size.y/2;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.paused) return;

        if (trTarget != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, trTarget.position, speed * Time.deltaTime);
        }

        if (transform.position == trTargetExit.position)
        {
            Destroy(gameObject);
        }
    }

    public void TakeShot()
    {
        if (--health == 0)
        {
            TopDownPlayerController.CarsShot++;

            personAnimator.enabled = true;
            personAnimator.Play("Person" + Random.Range(1,7));
            trTarget = trTargetExit;
            tag = Tag.Person.ToString();
        }
        else if (health == -1)
        {
            TopDownPlayerController.PeopleShot++;

            Destroy(gameObject);
        }
    }
}
