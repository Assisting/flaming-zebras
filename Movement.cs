using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	//Moving
	private float MOVE_SPEED; // the lateral speed of the players (as a force for air-control)

	//Jumping
	private float JUMP_FORCE; // strength of the impulse used to launch the player on a jump
	private float PUSH_HEIGHT; // abstract height above the ground where players can be considered "on the ground"
	private float JUMP_HEIGHT; // factor for maximum height the player can jump

	//Dashing
	private float DASH_POWER; // speed at which the dash is performed (inverse to WAIT_TIME)
	private float WAIT_TIME; // time after dash to wait before turning physics back on
	private const int USEABLE = -1; // human-readable value for cooldowns that are complete
	private float dashCooldown1; // 1st dash cooldown timestamp
	private float dashCooldown2; // 2nd dash cooldown timestamp
	private float dashWait; // timestamp to wait till dash completes

	// Use this for initialization
	void Start ()
	{
		MOVE_SPEED = 10f;

		JUMP_FORCE = 10f;
		PUSH_HEIGHT = 0.4f;

		DASH_POWER = 100f;
		WAIT_TIME = 0.05f;
		dashCooldown1 = USEABLE;
		dashCooldown2 = USEABLE;
		dashWait = USEABLE;
	}

	// Update is called once per frame (used for non-physics)
	void Update()
	{
		if (USEABLE != dashWait && dashWait <= Time.time)
		{
			StartPhysics();
			dashWait = USEABLE;
		}
	}

	// FixedUpdate is called once per 0.02 sec time unit
	void FixedUpdate ()
	{
		if ( Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded() )
			Jump();

		if ( Input.GetKeyDown(KeyCode.D) && CanDash() )
			DashRight();

		if ( Input.GetKeyDown(KeyCode.A) && CanDash() )
			DashLeft();

		if ( Input.GetKey(KeyCode.LeftArrow) )
			rigidbody2D.AddForce(-Vector2.right * MOVE_SPEED);

		if ( Input.GetKey(KeyCode.RightArrow) )
			rigidbody2D.AddForce(Vector2.right * MOVE_SPEED);
	}

	// Stop rigidbody motion (for a dash)
	private void StopPhysics()
	{
		rigidbody2D.isKinematic = true;
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.angularVelocity = 0;
	}

	// Begin rigidbody motion once more
	private void StartPhysics()
	{
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.angularVelocity = 0;
		rigidbody2D.isKinematic = false;
	}

	// Dash the player to the left
	private void DashLeft()
	{
		RemoveDashMarker();
		StopPhysics();
		rigidbody2D.velocity = (-Vector2.right * DASH_POWER);
		dashWait = Time.time + WAIT_TIME;
	}

	// Dash the player to the right
	private void DashRight()
	{
		RemoveDashMarker();
		StopPhysics();
		rigidbody2D.velocity = (Vector2.right * DASH_POWER);
		dashWait = Time.time + WAIT_TIME;
	}

	// Does the player have an available dash?
	// returns: true if there is one or more dashes available
	private bool CanDash()
	{
		if ( USEABLE != dashCooldown1 && dashCooldown1 <= Time.time)
			dashCooldown1 = USEABLE;
		if ( USEABLE != dashCooldown2 && dashCooldown2 <= Time.time)
			dashCooldown2 = USEABLE;
		return (USEABLE == dashCooldown2 || USEABLE == dashCooldown1);
	}

	// sets one of the dash cooldowns before a dash.
	// this function assumes that you have already checked to see if a dash is available.
	// it will consume dashes from right to left (i.e. 2nd, then 1st)
	private void RemoveDashMarker()
	{
		if ( USEABLE != dashCooldown2 )
			dashCooldown1 = Time.time + 5;
		else
			dashCooldown2 = Time.time + 5;
	}

	// is the player on the ground (and able to jump)?
	// returns: true if player is close enough to the ground
	private bool IsGrounded()
	{
		RaycastHit2D result = Physics2D.Raycast(this.transform.position, -Vector2.up, PUSH_HEIGHT);
		return (null != result.collider);
	}

	//Make the player jump
	private void Jump()
	{
		rigidbody2D.AddForce(Vector2.up * JUMP_FORCE, ForceMode2D.Impulse);
	}

}
