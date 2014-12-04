using UnityEngine;
using System.Collections;

public class PlayerData : Actor
{
//-----Structs and Enums----------------------------------------------------------------------------------------------------

	public enum Attribute { Move, Jump, Dash, WeaponType, WeaponLevel, Money }; //giving readable names to pass-in values 0-5

	private struct MoveLevel
	{
		public static float[] moveSpeed = new float[3] { 45f, 50f, 55f };
		public static float[] maxSpeed = new float[3] { 10f, 11f, 12f };
	}

	private struct DashLevel
	{
		public static float[] dashSpeed = new float[3] { 15f, 20f, 25f };
	}

//-----Attribute Variables---------------------------------------------------------------------------------------------------

	//Player Number

	public static int NUMPLAYERS = 0; //global notification of number of players
	private int PLAYERNUM = 0; //the number of the current player (local)


	//TEST - spawning
	public Rigidbody2D Player;

	//LayerMask
	public LayerMask usableLayer;

	//Interfaces
	private Weapon weapon;
	private KeyBindings keyBind;
	private Movement movement;
	private PlayerSounds playerSounds;

	//Levels
	private int MOVE_LEVEL;
	private int JUMP_LEVEL;
	private int DASH_LEVEL;
	private int WEAPON_LEVEL;

	//Combat
	private float LAST_HIT_COOLDOWN = 10f; //Time it takes for "last hit" to bleed off
	private GameObject lastHit; //player that last hit you

	//Moving
	private float MOVE_SPEED; // the lateral speed of the players (as a force for air-control)
	private float MAX_SPEED; // maximum lateral speed of the player

	//Jumping
	private readonly float JUMP_FORCE = 2000f; // speed of player's jump
	private bool grounded; // Whether or not the player is on the ground
	private bool jumpAvailable; // whether or not the player must wait to hit the ground before jumping again
	private int timesJumped; // The number of times the player has air-jumped

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
	private float PKLOSS = 0.3f; //the fraction of money to lose after being killed by a player
	private float EKLOSS = 0.2f; //the fraction of money to lose when killed by the environment
	//private float KILLTAX = 0.8f; //the fraction of money lost that goes to the player that killed you

	//Teleporting
	private float TELEPORT_COOLDOWN = 50f; //time to wait between "recalls"
	private bool canTeleport = true; //whether or not the player is allowed to recall
	private Vector2 lastTeleport; //the last teleporter that the player walked through
	private bool canUse = true; //to prevent b spam on teleporters

	//Shopping
	private int weaponSwaps = 0; //weapon swaps become progressively more expensive
	

//-----Unity Functions--------------------------------------------------------------------------------------------------------

	void Awake ()
	{
		//initialize interfaces
		weapon = GetComponent<Weapon>();
		keyBind = GetComponent<KeyBindings>();
		movement = GetComponent<Movement>();
		playerSounds = GetComponent<PlayerSounds>();

		//set player Number (must be awake for keybinding)
		NUMPLAYERS ++;
		PLAYERNUM = NUMPLAYERS;
	}

	// Use this for initialization
	void Start ()
	{	
	
		LevelUp(Attribute.Move, 1); //initialize moving
		MOVING_RIGHT = true;

		LevelUp(Attribute.Jump, 1); //initialize jump system
		grounded = false;
		jumpAvailable = false;

		LevelUp(Attribute.Dash, 1); //initialize dashing
		
		LevelUp (Attribute.WeaponLevel, 1);
		LevelUp(Attribute.WeaponType, Weapon.WeaponType.None); //initialize weapons

		moneyAmount = 25; //starting money total

		MAXLIFE = 100; //starting life total
		CURLIFE = 100; // current for testing

		/*PLAYERNUM++;
		
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
		PLAYERNUM--;*/
	}

	void Update()
	{
		if (CURLIFE <= 0)
			Die();
		
		if (Input.GetButtonDown( keyBind.UseButton() ) && CanUse()) //"USE" button
		{
			Vector2 hitboxSize = collider2D.bounds.extents;
			Vector2 topLeft = transform.position + new Vector3(-hitboxSize.x, hitboxSize.y, 0f);
			Vector2 bottomRight = transform.position + new Vector3(hitboxSize.x, -hitboxSize.y, 0f);
			Collider2D[] usables = Physics2D.OverlapAreaAll(topLeft, bottomRight, usableLayer);
			for (int i = 0; i < usables.Length; i ++)
			{
				usables[i].SendMessage("Use", gameObject);
			}
		}
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

	public override void StunDamage(int value, bool goRight)
	{
		if (CURRENT_WEAPON == Weapon.WeaponType.Melee && weapon.ShieldUp())
		{
			MakeInvuln(weapon.GetShieldTime());
			weapon.DropShield();
			return;
		}
		else if (INVULNERABLE)
		{
			playerSounds.PlayDeflect();
			return;
		}
		else
		{
			movement.SetJumpLag();
			base.StunDamage(value, goRight);
			playerSounds.PlayHurt();
		}
	}

	public override void LifeChange(int value)
	{
		if (CURRENT_WEAPON == Weapon.WeaponType.Melee && weapon.ShieldUp())
		{
			MakeInvuln(weapon.GetShieldTime());
			weapon.DropShield();
			return;
		}
		else if ( (!INVULNERABLE && value < 0) || value >= 0)
		{
			base.LifeChange(value);
			playerSounds.PlayHurt();
		}
		else
		{
			playerSounds.PlayDeflect();
		}
	}

	protected override void Die()
	{
		movement.TeleportHome(new Vector3(0f, 0f, 0f)); //go to shop, leave to origin level
		movement.StopDash();
		Squelch();
		Extinguish();
		if (lastHit != null) //give player money
		{
			int lostMoney = (int)(moneyAmount*PKLOSS);
			lastHit.GetComponent<PlayerData>().ChangeMoney(lostMoney + GetBounty());
			moneyAmount -= (lostMoney);
			lastHit = null;
		}
		else //just lose money
		{
			moneyAmount -= (int)(moneyAmount*EKLOSS);
		}
	}

	private int GetBounty()
	{
		int bounty = 0;
		bounty += WEAPON_LEVEL * 35;
		int otherlevels = 0;
		otherlevels += JUMP_LEVEL + MOVE_LEVEL + DASH_LEVEL;
		bounty += otherlevels * 20;
		return bounty;
	}

//-----Getters and Setters---------------------------------------------------------------------------------------------------

	public int GetPlayerNum()
	{
		return PLAYERNUM;
	}

	public int GetNumPlayers()
	{
		return NUMPLAYERS;
	}

	public void SetMovingRight()
	{
		MOVING_RIGHT = true;
	}

	public void SetMovingLeft()
	{
		MOVING_RIGHT = false;
	}

	public bool CanJump()
	{
		return jumpAvailable;
	}

	public void IncrementJumpCounter ()
	{
		timesJumped ++;
		if (timesJumped >= JUMP_LEVEL)
			jumpAvailable = false;
	}

	public void ResetJumpCounter ()
	{
		timesJumped = 0;
		jumpAvailable = true;
	}

	public void SetGrounded(bool value)
	{
		grounded = value;
	}

	public bool IsGrounded()
	{
		return grounded;
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

	public int GetAttributeLevel(Attribute attribute)
	{
		switch (attribute)
		{
			case Attribute.Move:
			{
				return MOVE_LEVEL;
			}
			case Attribute.Jump:
			{
				return JUMP_LEVEL;
			}
			case Attribute.Dash:
			{
				return DASH_LEVEL;
			}
			case Attribute.WeaponLevel:
			{
				return WEAPON_LEVEL;
			}
			case Attribute.Money:
			{
				return moneyAmount;
			}
			default:
			{
				return 0;
			}
		}
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
 
	public int getHealth()
	{
		return CURLIFE;
	}
	public int getMaxHealth()
	{
		return MAXLIFE;
	}

	public int numDashesAvailable()
	{
		if (!dash1Available && !dash2Available)
						return 0;
		else if ((dash1Available || dash2Available) && !(dash1Available && dash2Available))
						return 1;
		else if (dash1Available && dash2Available)
						return 2;
		else return 0;
	}
	public bool isDash1Available()
	{
		return dash1Available;
	}
	public bool isDash2Available()
	{
		return dash2Available;
	}

	public Vector2 getLastTeleport()
	{
		return lastTeleport;
	}

	public void SetLastTeleport(Vector2 teleporter)
	{
		lastTeleport = teleporter;
	}

	public void IncrementWeaponSwaps()
	{
		weaponSwaps++;
	}

	public int GetWeaponSwaps()
	{
		return weaponSwaps;
	}

	public void SetLastHit(GameObject hitter)
	{
		CancelInvoke("ResetLastHit");
		lastHit = hitter;
		Invoke("ResetLastHit", LAST_HIT_COOLDOWN);
	}

	public void SetTeleportCooldown()
	{
		CancelInvoke("MakeTeleportable");
		canTeleport = false;
		Invoke("MakeTeleportable", TELEPORT_COOLDOWN);
	}

	private void MakeTeleportable()
	{
		canTeleport = true;
	}

	public bool CanTeleport()
	{
		return canTeleport;
	}

	public void DelayUse(float time)
	{
		canUse = false;
		Invoke("ResetUse", time);
	}

	public void ResetUse()
	{
		canUse = true;
	}

	public bool CanUse()
	{
		return canUse;
	}

	public void ResetLastHit()
	{
		lastHit = null;
	}
}
