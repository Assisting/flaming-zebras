using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour
{
	//---Structs and Enums----------------------------------------------------------------------------------------------

	//Structs and enums
	public enum Attribute { Move, Jump, Dash, Money }; //giving readable names to pass-in values 0-4

	private struct MoveLevel
	{
		public static float[] moveSpeed = new float[3] { 10f, 12f, 15f };
	}

	private struct JumpLevel
	{
		public static float[] impulse = new float[3] { 4f, 5f, 6f };
		public static float[] force = new float[3] { 7f, 8f, 9f };
	}
	
	private struct DashLevel
	{
		public static float[] powerLevel = new float[3] { 25f, 50f, 100f };
		public static float[] waitTime = new float[3] { 0.2f, 0.1f, 0.05f };
	}

	//---Custom Classes-------------------------------------------------------------------------------------------------

	//---Attribute Variables---------------------------------------------------------------------------------------------------

	//Moving
	private float MOVE_SPEED; // the lateral speed of the players (as a force for air-control)

	//Jumping
	private float JUMP_IMPULSE; // strength of the impulse used to launch the player on a jump
	private float JUMP_FORCE; // force value that player experiences while jumping
	public readonly float PUSH_HEIGHT = 0.4f; // abstract height above the ground where players can be considered "on the ground"
	private bool JUMP_EXPIRED; // whether or not the player must wait to hit the ground before jumping again

	//Dashing
	private float DASH_POWER; // speed at which the dash is performed (inverse to WAIT_TIME)
	private float WAIT_TIME; // time after dash to wait before turning physics back on
	private readonly int USEABLE = -1; // human-readable value for cooldowns that are complete
	private float dashCoolTime;
	private float dashCooldown1; // 1st dash cooldown timestamp
	private float dashCooldown2; // 2nd dash cooldown timestamp
	private float dashWait; // timestamp to wait till dash completes

	//Money
	private int moneyAmount; // The amount of money a player has

	//---Unity Functions------------------------------------------------------------------------------------------------

	// Use this for initialization
	void Start ()
	{
		MOVE_SPEED = 10f;

		JUMP_IMPULSE = 4f;
		JUMP_FORCE = 7f;
		JUMP_EXPIRED = true;

		DASH_POWER = 25f;
		WAIT_TIME = 0.2f;
		dashCoolTime = 5f;
		dashCooldown1 = USEABLE;
		dashCooldown2 = USEABLE;
		dashWait = USEABLE;

		moneyAmount = 500;
	}

	//---Custom Functions-----------------------------------------------------------------------------------------------

	// functions to automatically call-up and set player stats to pre-defined level ranges
	public void LevelUp (Attribute attrib, int level)
	{
		switch (attrib)
		{
			case Attribute.Move :
			{
				MOVE_SPEED = MoveLevel.moveSpeed[level-1];
				return;
			}

			case Attribute.Jump :
			{
				JUMP_IMPULSE = JumpLevel.impulse[level-1];
				JUMP_FORCE = JumpLevel.force[level-1];
				return;
			}

			case Attribute.Dash :
			{
				
				DASH_POWER = DashLevel.powerLevel[level-1];
				WAIT_TIME = DashLevel.waitTime[level-1];
				return;
			}

			case Attribute.Money :
			{
				ChangeMoney(level);
				return;
			}
		}
	}

	public bool CanJump()
	{
		return !JUMP_EXPIRED;
	}

	public void SetJumpable (bool setting)
	{
		JUMP_EXPIRED = setting;
	}

	public void SetWaitTimer ()
	{
		dashWait = Time.time + WAIT_TIME;
	}

	public void SetCooldown1()
	{
		dashCooldown1 = Time.time + dashCoolTime;
	}

	public void SetCooldown2 ()
	{
		dashCooldown2 = Time.time + dashCoolTime;
	}

	public void ClearWaitTimer ()
	{
		dashWait = USEABLE;
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

	public float GetJUMP_IMPULSE ()
	{
		return JUMP_IMPULSE;
	}

	public float GetJUMP_FORCE ()
	{
		return JUMP_FORCE;
	}

	public float GetDASH_POWER ()
	{
		return DASH_POWER;
	}

	public bool IsDashOver ()
	{
		return USEABLE != dashWait && dashWait <= Time.time;
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