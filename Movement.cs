using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	private float MOVE_SPEED; // the lateral speed of the players (as a force for air-control)
	private float JUMP_FORCE; // strength of the impulse used to launch the player on a jump
	private float PUSH_HEIGHT; // abstract height above the ground where players can be considered "on the ground"
	private float DASH_STRENGTH; // stregth of impulse moving character laterally on a dash (to be replaced)
	private float SPAM_COOLDOWN; // number of seconds to wait between consecutive dashes
	private int USEABLE = -1; // readable value for cooldowns that are complete
	private float cooldown1; // 1st dash cooldown
	private float cooldown2; // 2nd dash cooldown
	private float spamcooler; // dash-spam cooldown

	// Use this for initialization
	void Start ()
	{
		MOVE_SPEED = 10f;
		JUMP_FORCE = 10f;
		PUSH_HEIGHT = 0.4f;
		DASH_STRENGTH = 6f;
		SPAM_COOLDOWN = 0.75f;
		cooldown1 = USEABLE;
		cooldown2 = USEABLE;
		spamcooler = USEABLE;

	}

	// FixedUpdate is called once per time unit
	void FixedUpdate ()
	{
		if ( Input.GetKey(KeyCode.LeftArrow) )
			rigidbody2D.AddForce(-Vector2.right * MOVE_SPEED);

		if ( Input.GetKey(KeyCode.RightArrow) )
			rigidbody2D.AddForce(Vector2.right * MOVE_SPEED);

		if ( IsGrounded() && Input.GetKey(KeyCode.UpArrow) )
			rigidbody2D.AddForce(Vector2.up * JUMP_FORCE, ForceMode2D.Impulse);

		if ( Input.GetKey(KeyCode.D) && CanDash() )
		{
			RemoveDashMarker();
			rigidbody2D.AddForce(Vector2.right * DASH_STRENGTH, ForceMode2D.Impulse);
		}

		if ( Input.GetKey(KeyCode.A) && CanDash() )
		{
			RemoveDashMarker();
			rigidbody2D.AddForce(-Vector2.right * DASH_STRENGTH, ForceMode2D.Impulse);
		}
	}

	// is the player on the ground (and able to jump)?
	// returns: true if player is close enough to the ground
	private bool IsGrounded()
	{
		RaycastHit2D result = Physics2D.Raycast(this.transform.position, -Vector2.up, PUSH_HEIGHT);

		return (null != result.collider);
	}

	// does the player have an available dash?
	// returns: true if there is one or more dashes available
	private bool CanDash()
	{
		if ( USEABLE != cooldown1 && cooldown1 <= Time.time)
			cooldown1 = USEABLE;
		if ( USEABLE != cooldown2 && cooldown2 <= Time.time)
			cooldown2 = USEABLE;
		if (USEABLE != spamcooler && spamcooler <= Time.time)
			spamcooler = USEABLE;
		return ( USEABLE == spamcooler && (USEABLE == cooldown2 || USEABLE == cooldown1) );
	}

	// sets one of the dash cooldowns (and the spam cooldown) before a dash.
	// this function assumes that you have already checked to see if a dash is available.
	// it will consume dashes from right to left (i.e. 2nd, then 1st)
	private void RemoveDashMarker()
	{
		spamcooler = Time.time + SPAM_COOLDOWN;
		if ( USEABLE != cooldown2 )
			cooldown1 = Time.time + 5;
		else
			cooldown2 = Time.time + 5;
	}



}
