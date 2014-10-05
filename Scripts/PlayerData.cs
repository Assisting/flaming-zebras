using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour
{
	//---Structs and Enums----------------------------------------------------------------------------------------------

	//Structs and enums
	public enum Attribute { Move, Jump, Dash, WeaponType, WeaponLevel, Money }; //giving readable names to pass-in values 0-5

	private struct MoveLevel
	{
		public static float[] moveSpeed = new float[3] { 10f, 12f, 14f };
		public static float[] maxSpeed = new float[3] { 8f, 10f, 12f };
	}

	private struct DashLevel
	{
		public static float[] dashSpeed = new float[3] { 10f, 15f, 20f };
	}

	//---Custom Classes-------------------------------------------------------------------------------------------------

	//---Attribute Variables---------------------------------------------------------------------------------------------------

	//Levels
	private int MOVE_LEVEL;
	private int JUMP_LEVEL;
	private int DASH_LEVEL;
	private int WEAPON_LEVEL;

	//Moving
	private float MOVE_SPEED; // the lateral speed of the players (as a force for air-control)
	private float MAX_SPEED; // maximum lateral speed of the player
	private bool MOVING_RIGHT;

	//Jumping
	private readonly float JUMP_FORCE = 1000f; // speed of player's jump
	public readonly float PUSH_HEIGHT = 0.4f; // abstract height above a collider where players can be considered "on the ground"
	private bool GROUNDED; // Whether or not the player is on the ground
	private bool JUMP_AVAILABLE; // whether or not the player must wait to hit the ground before jumping again
	private int TIMES_JUMPED; // The number of times the player has air-jumped

	//Dashing
	private float DASH_SPEED; // the maximum speed to move at while dashing
	private readonly int USEABLE = -1; // human-readable value for cooldowns that are complete
	private float DASH_COOL_TIME = 5f;
	private float dashCooldown1; // 1st dash cooldown timestamp
	private float dashCooldown2; // 2nd dash cooldown timestamp
	private bool dashing; // Is the player currently in a dash?

	//Weapon
	private Weapon.WeaponType CURRENT_WEAPON;

	//Money
	private int moneyAmount; // The amount of money a player has

	//---Unity Functions------------------------------------------------------------------------------------------------

	// Use this for initialization
	void Start ()
	{
		LevelUp(Attribute.Move, 1);

		MOVING_RIGHT = true;

		LevelUp(Attribute.Jump, 1);
		JUMP_AVAILABLE = true;

		LevelUp(Attribute.Dash, 1);
		dashCooldown1 = USEABLE;
		dashCooldown2 = USEABLE;

		LevelUp(Attribute.WeaponType, Weapon.WeaponType.None);
		LevelUp(Attribute.WeaponLevel, 1);

		moneyAmount = 1000;
	}

	//---Custom Functions-----------------------------------------------------------------------------------------------

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
	public void LevelUp (Attribute attrib, Weapon.WeaponType weapon)
	{
		if (Attribute.WeaponType == attrib)
		{
			CURRENT_WEAPON = weapon;
		}
		else
			return;
	}

	private void UpdateMoveStats()
	{
		MOVE_SPEED = MoveLevel.moveSpeed[MOVE_LEVEL - 1];
		MAX_SPEED = MoveLevel.maxSpeed[MOVE_LEVEL - 1];
	}

	public bool CanJump()
	{
		return JUMP_AVAILABLE;
	}

	public void SetJumpable (bool setting)
	{
		JUMP_AVAILABLE = setting;
	}

	public bool IsGrounded ()
	{
		RaycastHit2D result = Physics2D.Raycast(transform.position, -Vector2.up, PUSH_HEIGHT);
		return null != result.collider;
	}

	public void IncrementJumpCounter ()
	{
		TIMES_JUMPED ++;
	}

	public int TimesJumped ()
	{
		return TIMES_JUMPED;
	}

	public void SetCooldown1()
	{
		dashCooldown1 = Time.time + DASH_COOL_TIME;
	}

	public void SetCooldown2 ()
	{
		dashCooldown2 = Time.time + DASH_COOL_TIME;
	}

	public void ClearCooldown1 ()
	{
		dashCooldown1 = USEABLE;
	}

	public void ClearCooldown2 ()
	{
		dashCooldown2 = USEABLE;
	}

	public float GetMOVE_SPEED ()
	{
		return MOVE_SPEED;
	}

	public float GetMAX_SPEED ()
	{
		return MAX_SPEED;
	}

	public bool IsMovingRight()
	{
		return MOVING_RIGHT;
	}

	public void SetMovingRight(bool value)
	{
		MOVING_RIGHT = value;
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
		if (USEABLE != dashCooldown1 && dashCooldown1 <= Time.time)
			ClearCooldown1();
		if (USEABLE != dashCooldown2 && dashCooldown2 <= Time.time)
			ClearCooldown2();
		return (USEABLE == dashCooldown2 || USEABLE == dashCooldown1);
	}

	// sets one of the dash cooldowns before a dash.
	// this function assumes that you have already checked to see if a dash is available.
	// it will consume dashes from right to left (i.e. 2nd, then 1st)
	public void RemoveDashMarker()
	{
		if (USEABLE != dashCooldown2)
			if (USEABLE != dashCooldown1)
				print("Dash Overconsumption");
			else
			SetCooldown1();
		else
			SetCooldown2();
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
		return this.moneyAmount;
	}
	
	// subtracts or adds the amount of money specified to the player's moneyAmount.
	public void ChangeMoney(int change)
	{
		moneyAmount = moneyAmount + change;
	}
}
