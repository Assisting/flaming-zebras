using UnityEngine;
using System.Collections;

public class PlayerData : Actor
{
//-----Structs and Enums----------------------------------------------------------------------------------------------------

	public enum Attribute { Move, Jump, Dash, WeaponType, WeaponLevel, Money }; //giving readable names to pass-in values 0-5

	private struct MoveLevel
	{
		public static float[] moveSpeed = new float[3] { 30f, 35f, 40f };
		public static float[] maxSpeed = new float[3] { 8f, 10f, 12f };
	}

	private struct DashLevel
	{
		public static float[] dashSpeed = new float[3] { 15f, 20f, 25f };
	}

//-----Attribute Variables---------------------------------------------------------------------------------------------------

	//Player Number
	public int PLAYERNUM = 1; //assigning a new number to each player as they spawn

	public Rigidbody2D Player;

	//Interfaces
	private Weapon weapon;

	//Levels
	private int MOVE_LEVEL;
	private int JUMP_LEVEL;
	private int DASH_LEVEL;
	private int WEAPON_LEVEL;

	//Moving
	private float MOVE_SPEED; // the lateral speed of the players (as a force for air-control)
	private float MAX_SPEED; // maximum lateral speed of the player
	

	//Jumping
	private readonly float JUMP_FORCE = 875f; // speed of player's jump
	private bool GROUNDED; // Whether or not the player is on the ground
	private bool JUMP_AVAILABLE; // whether or not the player must wait to hit the ground before jumping again
	private int TIMES_JUMPED; // The number of times the player has air-jumped

	//Dashing
	private float DASH_SPEED; // the maximum speed to move at while dashing
	float DASH_COOL_TIME = 5f; // time to wait before using the same dash again
	private bool dash1Available = true; // 1st dash cooldown
	private bool dash2Available = true; // 2nd dash cooldown
	private bool dashing; // Is the player currently in a dash?

	//Weapon
	private Weapon.WeaponType CURRENT_WEAPON; // name of currently equipped weapon

	//Money
	private int moneyAmount; // The amount of money a player has

//-----Unity Functions--------------------------------------------------------------------------------------------------------

	void Awake ()
	{
		weapon = GetComponent<Weapon>();
	}

	// Use this for initialization
	void Start ()
	{	
		LevelUp(Attribute.Move, 1); //initialize moving
		MOVING_RIGHT = true;

		LevelUp(Attribute.Jump, 1); //initialize jump system
		GROUNDED = false;
		JUMP_AVAILABLE = false;

		LevelUp(Attribute.Dash, 1); //initialize dashing
		
		LevelUp (Attribute.WeaponLevel, 2);
		LevelUp(Attribute.WeaponType, Weapon.WeaponType.Bomb); //initialize weapons

		moneyAmount = 1000; //starting money total

		LIFE = 100; //starting life total

		PLAYERNUM++;

<<<<<<< HEAD
		// for making four players quick
		// Each time one of these functions is called, this script will run again, upon the new creation. 
		// This means Player 1 is creating player 2, P2 makes P3, and P3 makes P4
		if (PLAYERNUM == 2)
			Instantiate(Player, new Vector2(-5f, 5f), transform.rotation);
		if (PLAYERNUM == 3)
			Instantiate(Player, new Vector2(5f, 5f), transform.rotation);
		if (PLAYERNUM == 4)
			Instantiate(Player, new Vector2(0f, 8f), transform.rotation);
		// Once the instantiation is finished, set the PLAYERNUM back to what is needed to operate the correct player.
		// since the controls are bound to that character via the PLAYERNUM
		PLAYERNUM--;
=======
		//for making four players quick
		//if (PLAYERNUM < 5)
			//Instantiate(Player, new Vector2(0f, 0f), transform.rotation);
>>>>>>> origin/master
	}


//-----Custom Functions------------------------------------------------------------------------------------------------------

	// functions to automatically call-up and set player stats to pre-defined level ranges
	public void LevelUp (Attribute attrib, int level)
	{
		switch (attrib)
		{
			case Attribute.Move :
			{
				MOVE_LEVEL = level;
				UpdateMoveStats();
				return;
			}

			case Attribute.Jump :
			{
				JUMP_LEVEL = level;
				return;
			}

			case Attribute.Dash :
			{
				DASH_LEVEL = level;
				DASH_SPEED = DashLevel.dashSpeed[DASH_LEVEL - 1];
				return;
			}

			case Attribute.WeaponLevel :
			{
				WEAPON_LEVEL = level;
				weapon.UpdateWeapon();
				return;
			}

			case Attribute.Money :
			{
				ChangeMoney(level);
				return;
			}

			default :
			{
				return;
			}
		}
	}

	//special LevelUp function for weapon types
	public void LevelUp (Attribute attrib, Weapon.WeaponType newWeapon)
	{
		if (Attribute.WeaponType == attrib)
		{
			CURRENT_WEAPON = newWeapon;
			weapon.UpdateWeapon();
		}
		else
			return;
	}

	//functional encapsulation for changes in a move level
	private void UpdateMoveStats()
	{
		MOVE_SPEED = MoveLevel.moveSpeed[MOVE_LEVEL - 1];
		MAX_SPEED = MoveLevel.maxSpeed[MOVE_LEVEL - 1];
	}

//-----Getters and Setters---------------------------------------------------------------------------------------------------

	//used to get player number during instantiation (DOES NOT WORK AT RUNTIME!!)
	public int GetPlayerNum()
	{
		return PLAYERNUM;
	}

	public bool CanJump()
	{
		return JUMP_AVAILABLE;
	}

	public void IncrementJumpCounter ()
	{
		TIMES_JUMPED ++;
		if (TIMES_JUMPED >= JUMP_LEVEL)
			JUMP_AVAILABLE = false;
	}

	public void ResetJumpCounter ()
	{
		TIMES_JUMPED = 0;
		JUMP_AVAILABLE = true;
	}

	public void SetGrounded(bool value)
	{
		GROUNDED = value;
	}

	public bool IsGrounded()
	{
		return GROUNDED;
	}

	private void ResetDash1 ()
	{
		dash1Available = true;
	}

	private void ResetDash2 ()
	{
		dash2Available = true;
	}

	public float GetMOVE_SPEED ()
	{
		return MOVE_SPEED;
	}

	public float GetMAX_SPEED ()
	{
		return MAX_SPEED;
	}

	public float GetJUMP_FORCE ()
	{
		return JUMP_FORCE;
	}

	public float GetDASH_SPEED ()
	{
		return DASH_SPEED;
	}

	public void SetDashing (bool value)
	{
		dashing = value;
	}

	public bool IsDashing ()
	{
		return dashing;
	}
	
	// Does the player have an available dash?
	// returns: true if there is one or more dashes available
	public bool CanDash()
	{
		return (dash2Available || dash1Available);
	}

	// sets one of the dash cooldowns before a dash.
	// this function assumes that you have already checked to see if a dash is available.
	// it will consume dashes from right to left (i.e. 2nd, then 1st)
	public void RemoveDash()
	{
		if (dash2Available)
		{
			dash2Available = !dash2Available;
			Invoke("ResetDash2", DASH_COOL_TIME);
		}
		else if (dash1Available)
		{
			dash1Available = !dash1Available;
			Invoke("ResetDash1", DASH_COOL_TIME);
		}
	}

	public Weapon.WeaponType GetWeaponType()
	{
		return CURRENT_WEAPON;
	}

	public int GetMoveLevel()
	{
		return MOVE_LEVEL;
	}

	public int GetJumpLevel()
	{
		return JUMP_LEVEL;
	}

	public int GetDashLevel()
	{
		return DASH_LEVEL;
	}

	public int GetWeaponLevel()
	{
		return WEAPON_LEVEL;
	}
	
	// allows shops to see the amount of money a player has.
	public int GetMoney()
	{
		return moneyAmount;
	}
	
	// subtracts or adds the amount of money specified to the player's moneyAmount.
	public void ChangeMoney(int change)
	{
		moneyAmount += change;
	}
}
