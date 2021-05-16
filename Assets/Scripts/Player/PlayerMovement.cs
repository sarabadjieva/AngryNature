using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Direction
{
	None,
	Up,
	Down,
	Left,
	Right
}

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float speed = 3f;
	[SerializeField] private float m_JumpForce = 400f;
	[SerializeField] private bool m_AirControl = false;

	private bool m_Grounded;
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;
	private bool shouldJump = false;
	private Vector3 movement;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

    private void Update()
    {
		movement.x = Input.GetAxisRaw("Horizontal") * speed;
		movement.y = Input.GetAxisRaw("Vertical");

		if (Input.GetKeyDown(KeyCode.Space) && !shouldJump && m_Grounded)
		{
			shouldJump = true;
		}
	}

    private void FixedUpdate()
	{
		movement.x = movement.x * Time.fixedDeltaTime;

		if (m_Grounded || movement.y > 0f) movement.y = 0f;

		Move();
	}

	public void Move()
	{

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			//m_Rigidbody2D.MovePosition(transform.position + movement);
			transform.position += movement;

			if (movement.x > 0 && !m_FacingRight)
			{
				Flip();
			}
			else if (movement.x < 0 && m_FacingRight)
			{
				Flip();
			}
		}

		if (m_Grounded && shouldJump)
		{
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			m_Grounded = false;
			shouldJump = false;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Ground")
		{
			m_Grounded = true;
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

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
