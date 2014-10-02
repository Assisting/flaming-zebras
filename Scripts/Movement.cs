using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	private PlayerData playerData;

	private Vector2 endPoint;

	// Use this for initialization
	void Start ()
	{
		playerData = GetComponent<PlayerData>();
	}

	// Update is called once per frame (used for non-physics and detecting single presses of buttons)
	void Update()
	{
		
		if ( Input.GetKeyDown(KeyCode.UpArrow) && playerData.CanJump() )
		{
			if ( playerData.IsGrounded() )
			{
				rigidbody2D.AddForce( Vector2.up * playerData.GetJUMP_FORCE() );
			}
			else if (playerData.TimesJumped() < playerData.GetJumpLevel())
			{
				playerData.IncrementJumpCounter();
				rigidbody2D.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
			}
		}

		if ( Input.GetKeyDown(KeyCode.D) && playerData.CanDash() )
			DashRight();

		if ( Input.GetKeyDown(KeyCode.A) && playerData.CanDash() )
			DashLeft();

		if ( Input.GetKeyUp(KeyCode.UpArrow) && rigidbody2D.velocity.y >= 0 && playerData.CanJump() )
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
		else
		{
			if ( Input.GetKey(KeyCode.LeftArrow) && rigidbody2D.velocity.x > -playerData.GetMAX_SPEED() )
			{
				playerData.SetMovingRight(false);
				rigidbody2D.AddForce( -Vector2.right * playerData.GetMOVE_SPEED() );
			}
	
			if ( Input.GetKey(KeyCode.RightArrow) && rigidbody2D.velocity.x < playerData.GetMAX_SPEED() )
			{
				playerData.SetMovingRight(true);
				rigidbody2D.AddForce( Vector2.right * playerData.GetMOVE_SPEED() );
			}
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

	private void StartDash ()
	{
		playerData.RemoveDashMarker();
		playerData.SetDashing(true);
		rigidbody2D.isKinematic = true;
	}

	private void StopDash ()
	{
		playerData.SetDashing(false);
		rigidbody2D.isKinematic = false;
	}
}
