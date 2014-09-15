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
		if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded() && playerData.CanJump())
			rigidbody2D.AddForce(Vector2.up * playerData.GetJUMP_FORCE());

		if (Input.GetKeyDown(KeyCode.D) && playerData.CanDash())
			DashRight();

		if (Input.GetKeyDown(KeyCode.A) && playerData.CanDash())
			DashLeft();

		if (Input.GetKeyUp(KeyCode.UpArrow) && rigidbody2D.velocity.y >= 0 && playerData.CanJump())
		{
			Vector2 currentVector = rigidbody2D.velocity;
			currentVector.y = 0;
			playerData.SetJumpable(false);
			rigidbody2D.velocity = currentVector;
		}

		if (playerData.IsDashOver())
		{
			StartPhysics();
			playerData.ClearWaitTimer();
		}

		if (Input.GetKeyDown(KeyCode.Alpha1))
			playerData.LevelUp(PlayerData.Attribute.Move, 1);

		if (Input.GetKeyDown(KeyCode.Alpha2))
			playerData.LevelUp(PlayerData.Attribute.Move, 2);
		
		if (Input.GetKeyDown(KeyCode.Alpha3))
			playerData.LevelUp(PlayerData.Attribute.Move, 3);
	}

	// FixedUpdate is called once per physics tick
	void FixedUpdate ()
	{
		if (Input.GetKey(KeyCode.LeftArrow) && rigidbody2D.velocity.x > -playerData.GetMAX_SPEED())
			rigidbody2D.AddForce(-Vector2.right * playerData.GetMOVE_SPEED());
		//rigidbody2D.velocity = new Vector2 (-playerData.GetMOVE_SPEED(), rigidbody2D.velocity.y);

		if (Input.GetKey(KeyCode.RightArrow) && rigidbody2D.velocity.x < playerData.GetMAX_SPEED())
			rigidbody2D.AddForce(Vector2.right * playerData.GetMOVE_SPEED());
			//rigidbody2D.velocity = new Vector2 (playerData.GetMOVE_SPEED(), rigidbody2D.velocity.y);
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
			playerData.SetJumpable(true);
		return (grounded);
	}

}
