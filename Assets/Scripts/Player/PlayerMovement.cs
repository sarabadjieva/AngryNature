using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float speed = 3f;
	[SerializeField] private float m_JumpForce = 400f;
	[SerializeField] private bool airControl = false;

	private Rigidbody2D rb2D;

	private bool freeze = false;
	private bool grounded;
	private bool shouldJump = false;
	private bool facingRight = true;

	private float moveHorizontal = 0f;
	private float moveVertical = 0f;

	public bool Freeze
	{
		get => freeze;
		set 
		{
			freeze = value;
			rb2D.simulated = value;
			rb2D.velocity = Vector3.zero;
		}
    }

	private void Awake()
	{
		rb2D = GetComponent<Rigidbody2D>();
	}

    private void Update()
    {
		if (freeze) return;

		moveHorizontal = Input.GetAxisRaw("Horizontal");
		moveVertical = Input.GetAxisRaw("Vertical");

		if (Input.GetKeyDown(KeyCode.Space) && !shouldJump && grounded)
		{
			shouldJump = true;
		}
	}

    private void FixedUpdate()
	{
		if (freeze) return;

		if (grounded || (airControl && moveVertical > 0f)) moveVertical = 0f;

		Move();
	}

	public void Move()
	{

		//only control the player if grounded or airControl is turned on
		if (grounded || airControl)
		{
			//m_Rigidbody2D.MovePosition(transform.position + movement);
			//transform.position += movement;
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

		if (grounded && shouldJump)
		{
			rb2D.AddForce(new Vector2(0f, m_JumpForce));
			grounded = false;
			shouldJump = false;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Ground")
		{
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

	//MINE

	/*
	private Direction previousDirection;
	private Direction currentDirection = Direction.None;

	private float moveTimer = 0f;
	private float timeToMove = 1f;

	public float jumpForce = 300f;

	public Rigidbody2D rigidbody2D;
	public GameObject goSpritesContainer;

	private Vector3 movement;

	public bool shouldJump = false;
	//public bool shouldMove = false;
	public bool shouldMoveAllDirections = false;
	public bool isGrounded = true;

	public bool CanMoveVertical
	{
		//primerno
		get => shouldMoveAllDirections;
		set => shouldMoveAllDirections = value;
	}

	private void Update()
    {

		if (Input.GetKeyDown(KeyCode.Space))
		{
			shouldJump = true;
		}

		//if (Input.GetAxisRaw("Horizontal") != 0f && !CanMoveVertical)
		//{
		//	Move();
		//}
		//else if (Input.GetAxisRaw("Horizontal") != 0f || (Input.GetAxisRaw("Vertical") != 0f && CanMoveVertical))
		//{
		//	MoveAllDirections();
		//}

		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");

		if (previousDirection != currentDirection)
		{
			Flip();
			previousDirection = currentDirection;
		}
	}

    private void FixedUpdate()
	{
		if (shouldJump && isGrounded)
		{
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			shouldJump = false;
			isGrounded = false;
		}

		if (isGrounded && movement.y > 0f) movement.y = 0f;
		if (movement.x < 0) currentDirection = Direction.Left;
		if (movement.x > 0) currentDirection = Direction.Right;

		//transform.position += movement * Time.deltaTime * speed;
		if (isGrounded)
        {
			rigidbody2D.MovePosition(transform.position + movement * Time.deltaTime * speed);
        }
        else
        {
			movement.x = 1f;
			rigidbody2D.MovePosition(transform.position + movement * Time.deltaTime * speed);
		}

		//shouldMove = false;
		//shouldMoveAllDirections = false;
	}

	private void Flip()
    {
        switch (currentDirection)
        {
            case Direction.None:
                break;
            case Direction.Up:
                break;
            case Direction.Down:
                break;
            case Direction.Left:
				transform.rotation = Quaternion.Euler(0f, 180f, 0);
				break;
            case Direction.Right:
				transform.rotation = Quaternion.Euler(0f, 0f, 0);
				break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.collider.tag == "Ground")
        {
			isGrounded = true;
        }
    }
*/
}
