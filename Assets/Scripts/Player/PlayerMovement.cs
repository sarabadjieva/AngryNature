using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
	private const int maxJumps = 2;

	[SerializeField] private float speed = 3f;
	[SerializeField] private bool airControl = true;
	private float jumpForce = 900f;

	private Rigidbody2D rb2D;

	private bool grounded;
	private bool freeze = false;
	private bool facingRight = true;

	//for double jumps
	private int timesJumped = 0;
	private bool shouldJump = false;

	private float moveHorizontal = 0f;
	private float moveVertical = 0f;

	public bool Freeze
	{
		get => freeze;
		set 
		{
			freeze = value;
			rb2D.simulated = !value;
			if (value)
            {
				rb2D.velocity = Vector3.zero;
            }
		}
    }

	private void Awake()
	{
		if (rb2D == null)
        {
			rb2D = GetComponent<Rigidbody2D>();
        }
	}

    private void Update()
    {
		Freeze = GameManager.Instance.paused;

		if (freeze) return;

		moveHorizontal = Input.GetAxisRaw("Horizontal");
		moveVertical = Input.GetAxisRaw("Vertical");

		if (Input.GetKeyDown(KeyCode.Space) && !shouldJump && timesJumped < maxJumps)
		{
			shouldJump = true;
		}
	}

    private void FixedUpdate()
	{
		Freeze = GameManager.Instance.paused;

		if (freeze) return;

		if (grounded || (airControl && moveVertical > 0f)) moveVertical = 0f;

		Move();
	}

	public void Move()
	{
		//only control the player if grounded or airControl is turned on
		if (grounded || airControl)
		{
			rb2D.velocity = new Vector3(moveHorizontal * speed, rb2D.velocity.y, moveVertical * speed);

			if (moveHorizontal > 0 && !facingRight)
			{
				Flip();
			}
			else if (moveHorizontal < 0 && facingRight)
			{
				Flip();
			}
		}

		if (shouldJump && timesJumped++ < maxJumps)
		{
			rb2D.AddForce(new Vector2(0f, jumpForce));
			grounded = false;
			shouldJump = false;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == Tag.Ground.ToString())
		{
			timesJumped = 0;
			grounded = true;
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
