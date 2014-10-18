using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

//-----Attribute Variables---------------------------------------------------------------------------------------------------

	private PlayerData playerData;

	private Vector2 endPoint; //endpoint of any particular dash

	public Transform groundCheck; // location of object from which to raycast toward the ground
	public LayerMask groundType; // what is ground?

	private float groundRadius = 0.07f; // abstract height above a collider where players can be considered "on the ground"
	private float jumpLag; // small wait to avoid being grounded while lifting off
	public bool wallLeft;
	public bool wallRight;

//-----Unity Functions--------------------------------------------------------------------------------------------------------

	void Awake()
	{
		playerData = GetComponent<PlayerData>();
	}

	// Use this for initialization
	void Start ()
	{	
	}

	// Update is called once per frame (used for non-physics and detecting single presses of buttons)
	void Update()
	{

		if ( !playerData.IsDashing() ) //only bother checking for jumps if player isn't in a dash
		{
			//jump
			if ( Input.GetButtonDown("Jump") && playerData.CanJump() )
			{
				playerData.IncrementJumpCounter();
				if ( playerData.IsGrounded() ) //ground jump
				{
					jumpLag = Time.time + 0.02f; //wait one frame before allowing the ground to reset jump counter
					playerData.SetGrounded(false);
					rigidbody2D.AddForce( Vector2.up * playerData.GetJUMP_FORCE() );
				}
				else //air jump
					rigidbody2D.velocity = Vector2.up * 3f;
			}

			//let go of jump
			if ( Input.GetButtonUp("Jump") && rigidbody2D.velocity.y >= 0)
			{
				Vector2 currentVector = rigidbody2D.velocity;
				currentVector.y = 0;
				rigidbody2D.velocity = currentVector;
			}
		}

		//dash right
		if ( Input.GetButtonDown("RDash") && playerData.CanDash() && !wallRight)
			DashRight();

		//dash left
		if ( Input.GetButtonDown("LDash") && playerData.CanDash() && !wallLeft)
			DashLeft();
	}

	// FixedUpdate is called once per physics tick
	void FixedUpdate ()
	{
		GroundCheck();
	
		if ( playerData.IsDashing() )
			continueDash();
		else //don't do any movement unless we are not dashing
		{
			//move left
			if ( Input.GetButton("Left") && !wallLeft && rigidbody2D.velocity.x > -playerData.GetMAX_SPEED() )
			{
				playerData.SetMovingRight(false);
				rigidbody2D.AddForce( -Vector2.right * playerData.GetMOVE_SPEED() );
			}

			//move right
			if ( Input.GetButton("Right") && !wallRight && rigidbody2D.velocity.x < playerData.GetMAX_SPEED() )
			{
				playerData.SetMovingRight(true);
				rigidbody2D.AddForce( Vector2.right * playerData.GetMOVE_SPEED() );
			}
		}
	}

//-----Custom Functions------------------------------------------------------------------------------------------------------

	// Set up the player to dash to the left
	public void DashLeft()
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
	public void StartDash ()
	{
		playerData.RemoveDashMarker();
		playerData.SetDashing(true);
		rigidbody2D.gravityScale = 0;
		rigidbody2D.velocity = Vector2.zero;
	}

	//turns on physics for return to normality after a dash
	public void StopDash ()
	{
		playerData.SetDashing(false);
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.gravityScale = 1;
	}

	//run in FixedUpdate() to update grounded status, animation, current platform etc.
	private void GroundCheck()
	{
		Collider2D floorType = Physics2D.Raycast(groundCheck.position, -Vector2.up, groundRadius, groundType).collider;
		playerData.SetGrounded(floorType != null);
		if ( playerData.IsGrounded())
		{
			if (jumpLag < Time.time)
				playerData.ResetJumpCounter();
		}
	}

	//flip player left/right for direction changes
	void Flip () {
		playerData.SetMovingRight( !playerData.IsMovingRight() );
		Vector3 currentScale = transform.localScale;
		currentScale.x *= -1;
		transform.localScale = currentScale;
	}
}
