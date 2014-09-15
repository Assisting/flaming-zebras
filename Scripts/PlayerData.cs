using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour
{
	//---Structs and Enums----------------------------------------------------------------------------------------------

	//Structs and enums
	public enum Attribute { Move, Jump, Dash, Money }; //giving readable names to pass-in values 0-4

	private struct MoveLevel
	{
		public static float[] moveSpeed = new float[3] { 10f, 12f, 14f };
		public static float[] maxSpeed = new float[3] { 8f, 10f, 12f };
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
	private float MAX_SPEED; // maximum lateral speed of the player

	//Jumping
	private readonly float JUMP_FORCE = 500f; // speed of player's jump
	public readonly float PUSH_HEIGHT = 0.4f; // abstract height above the ground where players can be considered "on the ground"
	private bool JUMP_AVAILABLE; // whether or not the player must wait to hit the ground before jumping again

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
		MAX_SPEED = 6f;

		JUMP_AVAILABLE = false;

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
				MAX_SPEED = MoveLevel.maxSpeed[level-1];
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
		return JUMP_AVAILABLE;
	}

	public void SetJumpable (bool setting)
	{
		JUMP_AVAILABLE = setting;
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

	public float GetMAX_SPEED ()
	{
		return MAX_SPEED;
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