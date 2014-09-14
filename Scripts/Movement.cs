using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	private PlayerData playerData;

	// Use this for initialization
	void Start ()
	{
		playerData = GetComponent<PlayerData>();
	}

	// Update is called once per frame (used for non-physics)
	void Update()
	{
		if (playerData.IsDashOver())
		{
			StartPhysics();
			playerData.ClearWaitTimer();
		}

		if (Input.GetKeyDown(KeyCode.J))
		{
			playerData.LevelUp(PlayerData.Attribute.Dash, 3);
		}
	}

	// FixedUpdate is called once per 0.02 sec time unit
	void FixedUpdate ()
	{
		if ( Input.GetKeyUp(KeyCode.UpArrow) || rigidbody2D.velocity.y < 0)
			playerData.SetJumpable(true);

		if ( Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded() )
			Jump();

		if ( Input.GetKey(KeyCode.UpArrow) && playerData.CanJump() )
			rigidbody2D.AddForce(Vector2.up * playerData.GetJUMP_FORCE());

		if ( Input.GetKeyDown(KeyCode.D) && playerData.CanDash() )
			DashRight();

		if ( Input.GetKeyDown(KeyCode.A) && playerData.CanDash() )
			DashLeft();

		if ( Input.GetKey(KeyCode.LeftArrow) )
			rigidbody2D.AddForce(-Vector2.right * playerData.GetMOVE_SPEED());

		if ( Input.GetKey(KeyCode.RightArrow) )
			rigidbody2D.AddForce(Vector2.right * playerData.GetMOVE_SPEED());
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
		playerData.RemoveDashMarker();
		StopPhysics();
		rigidbody2D.velocity = (-Vector2.right * playerData.GetDASH_POWER());
		playerData.SetWaitTimer();
	}

	// Dash the player to the right
	private void DashRight()
	{
		playerData.RemoveDashMarker();
		StopPhysics();
		rigidbody2D.velocity = (Vector2.right * playerData.GetDASH_POWER());
		playerData.SetWaitTimer();
	}

	// is the player on the ground (and able to jump)?
	// returns: true if player is close enough to the ground
	private bool IsGrounded()
	{
		RaycastHit2D result = Physics2D.Raycast(transform.position, -Vector2.up, playerData.PUSH_HEIGHT);
		bool grounded = null != result.collider;
		if (grounded)
			playerData.SetJumpable(false);
		return (grounded);
	}

	//Make the player jump
	private void Jump()
	{
		rigidbody2D.AddForce(Vector2.up * playerData.GetJUMP_IMPULSE(), ForceMode2D.Impulse);
	}

}
