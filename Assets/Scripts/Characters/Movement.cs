using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

//-----Attribute Variables---------------------------------------------------------------------------------------------------

	private PlayerData playerData;
	private KeyBindings keyBind;
	Animator anim;

	private Vector2 endPoint; //endpoint of any particular dash

	public Transform leftGroundCheck; // location of object from which to raycast toward the ground
	public Transform rightGroundCheck;
	public LayerMask groundType; // what is ground?

	private float groundRadius = 0.07f; // abstract height above a collider where players can be considered "on the ground"
	private float jumpLag; // small wait to avoid being grounded while lifting off
	public bool wallLeft;
	public bool wallRight;

	private float playerXAxisValue = 0f; // Value to store current thumbstick tilt
	

//-----Unity Functions--------------------------------------------------------------------------------------------------------

	void Awake()
	{
		playerData = GetComponent<PlayerData>();
		keyBind = GetComponent<KeyBindings>();
	}

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame (used for non-physics and detecting single presses of buttons)
	void Update()
	{

		if ( !playerData.IsDashing() && !playerData.isStunned()) //only bother checking for jumps if player isn't in a dash or stunned
		{
			//jump
			if ( Input.GetButtonDown( keyBind.JumpButton() ) && playerData.CanJump() )
			{
				anim.SetTrigger("Jump"); //calls vertical jump in animation
			}

			//let go of jump
			if ( Input.GetButtonUp( keyBind.JumpButton() ) && rigidbody2D.velocity.y >= 0f)
			{
				Vector2 currentVector = rigidbody2D.velocity;
				currentVector.y = 0f;
				rigidbody2D.velocity = currentVector;
			}

			anim.SetFloat("MoveSpeed", rigidbody2D.velocity.x);
			anim.SetFloat("VertSpeed", rigidbody2D.velocity.y);
		}

		//dash right
		if ( Input.GetButtonDown( keyBind.RDashButton() ) && playerData.CanDash() && !wallRight)
			DashRight();

		//dash left
		if ( Input.GetButtonDown( keyBind.LDashButton() ) && playerData.CanDash() && !wallLeft)
			DashLeft();
	}

	// FixedUpdate is called once per physics tick
	void FixedUpdate ()
	{
		GroundCheck();
	
		if ( playerData.IsDashing() )
			continueDash();
		else if( !playerData.isStunned() ) //don't do any movement unless we are not dashing (and aren't stunned)
		{
			playerXAxisValue = Input.GetAxis( keyBind.playerXAxis() );
			//move left
			if (playerXAxisValue < -0.7f)
			{
				if ( !wallLeft && !playerData.isStunned() && rigidbody2D.velocity.x >= -playerData.GetMAX_SPEED() )
				{
					rigidbody2D.AddForce( -Vector2.right * playerData.GetMOVE_SPEED() );
				}
			}

			//move right
			else if (playerXAxisValue > 0.7f)
			{
				if ( !wallRight && !playerData.isStunned() && rigidbody2D.velocity.x <= playerData.GetMAX_SPEED() )
				{
					rigidbody2D.AddForce( Vector2.right * playerData.GetMOVE_SPEED() );
				}
			}
		}
	}

//-----Custom Functions------------------------------------------------------------------------------------------------------

	//Called by the animator to make the character leave the ground
	void JumpStuff()
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
		playerData.RemoveDash();
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
		Collider2D leftFloorType = Physics2D.Raycast(leftGroundCheck.position, -Vector2.up, groundRadius, groundType).collider;
		Collider2D rightFloorType = Physics2D.Raycast(rightGroundCheck.position, -Vector2.up, groundRadius, groundType).collider;
		playerData.SetGrounded(leftFloorType != null || rightFloorType != null);
		if ( playerData.IsGrounded())
		{
			if (jumpLag < Time.time)
			{
				playerData.ResetJumpCounter();
				playerData.setStunned(false);
				anim.SetBool("Grounded", true);
			}
		}
		else
		{
			anim.SetBool("Grounded", false);
		}
		
	}
}