using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	private PlayerData playerData;

	private Vector2 endPoint;

	public Transform groundCheck; // location of object from which to raycast toward the ground
	private float groundRadius = 0.07f; // abstract height above a collider where players can be considered "on the ground"
	public LayerMask groundType; // what is ground?
	private float jumpLag; // small wait to avoid being grounded while lifting off

	// Use this for initialization
	void Start ()
	{
		playerData = GetComponent<PlayerData>();
	}

	// Update is called once per frame (used for non-physics and detecting single presses of buttons)
	void Update()
	{
		//jump
		if ( Input.GetButtonDown("Jump") && playerData.CanJump() )
		{
			playerData.IncrementJumpCounter();
			if ( playerData.IsGrounded() ) //ground jump
			{
				jumpLag = Time.time + 0.02f;
				playerData.SetGrounded(false);
				rigidbody2D.AddForce( Vector2.up * playerData.GetJUMP_FORCE() );
			}
			else //air jump
				rigidbody2D.velocity = Vector2.up * 3f;
		}

		//dash right
		if ( Input.GetButtonDown("RDash") && playerData.CanDash() )
			DashRight();

		//dash left
		if ( Input.GetButtonDown("LDash") && playerData.CanDash() )
			DashLeft();

		//let go of jump
		if ( Input.GetButtonUp("Jump") && rigidbody2D.velocity.y >= 0)
		{
			Vector2 currentVector = rigidbody2D.velocity;
			currentVector.y = 0;
			rigidbody2D.velocity = currentVector;
		}
	}

	// FixedUpdate is called once per physics tick
	void FixedUpdate ()
	{
		if ( playerData.IsDashing() )
			continueDash();

		GroundCheck();

		//move left
		if ( Input.GetButton("Left") && rigidbody2D.velocity.x > -playerData.GetMAX_SPEED() )
		{
			playerData.SetMovingRight(false);
			rigidbody2D.AddForce( -Vector2.right * playerData.GetMOVE_SPEED() );
		}

		//move right
		if ( Input.GetButton("Right") && rigidbody2D.velocity.x < playerData.GetMAX_SPEED() )
		{
			playerData.SetMovingRight(true);
			rigidbody2D.AddForce( Vector2.right * playerData.GetMOVE_SPEED() );
		}
	}

	// Set up the player to dash to the left
	private void DashLeft()
	{
		StartDash();
		endPoint = new Vector2(transform.position.x - 5, transform.position.y);
	}

	// Set up the player to dash to the right
	private void DashRight()
	{
		StartDash();
		endPoint = new Vector2(transform.position.x + 5, transform.position.y);
	}

	// Used to move the player through a dash each frame
	private void continueDash ()
	{
		Vector2 lastSpot = transform.position;
		transform.position = Vector2.MoveTowards( transform.position, endPoint, playerData.GetDASH_SPEED() * Time.deltaTime );
		if (transform.position.x < lastSpot.x && transform.position.x <= endPoint.x)
			StopDash();
		else if (transform.position.x > lastSpot.x && transform.position.x >= endPoint.x)
			StopDash();
	}

	//turns off physics so that an air-dash can begin
	private void StartDash ()
	{
		playerData.RemoveDashMarker();
		playerData.SetDashing(true);
		rigidbody2D.isKinematic = true;
	}

	//turns on physics for return to normality after a dash
	private void StopDash ()
	{
		playerData.SetDashing(false);
		rigidbody2D.isKinematic = false;
	}

	//run in FixedUpdate() to update grounded status, animation, current platform etc.
	private void GroundCheck()
	{
		Collider2D floorType = Physics2D.Raycast(groundCheck.position, -Vector2.up, groundRadius, groundType).collider;
		playerData.SetGrounded(floorType != null);
		if ( playerData.IsGrounded() && jumpLag < Time.time)
			playerData.ResetJumpCounter();
	}
}
