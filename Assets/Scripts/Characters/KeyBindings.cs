using UnityEngine;
using System.Collections;

public class KeyBindings : MonoBehaviour {

	//Keybindings
	private string ATTACK_BUTTON; //mapping for attack/shoot button
	private string USE_BUTTON; //mapping for "USE" button
	private string SHOW_MONEY_BUTTON; //mapping for showing the amoun of money you have. 
										// hidden for screen-peeking reasons
	private string PLAYER_X_AXIS; //mapping for thumbstick
	private string JUMP_BUTTON; //mapping for jump button
	private string RDASH_BUTTON; //mapping for dash button (right)
	private string LDASH_BUTTON; //mapping for dash button (left)
	private string START_BUTTON;

	//Interfaces
	private PlayerData playerData;

	void Awake()
	{
		playerData = GetComponent<PlayerData>();
		switch ( playerData.GetPlayerNum() )
		{
			case 1:
			{
				ATTACK_BUTTON = "P1Fire1";
				USE_BUTTON = "P1Use";
				SHOW_MONEY_BUTTON = "P1ShowMoney";
				PLAYER_X_AXIS = "P1Horizontal";
				JUMP_BUTTON = "P1Jump";
				RDASH_BUTTON = "P1RDash";
				LDASH_BUTTON = "P1LDash";
				START_BUTTON = "P1Start";
				break;
			}
			case 2:
			{
				ATTACK_BUTTON = "P2Fire1";
				USE_BUTTON = "P2Use";
				SHOW_MONEY_BUTTON = "P2ShowMoney";
				PLAYER_X_AXIS = "P2Horizontal";
				JUMP_BUTTON = "P2Jump";
				RDASH_BUTTON = "P2RDash";
				LDASH_BUTTON = "P2LDash";
				START_BUTTON = "P2Start";
				break;
			}
			case 3:
			{
				ATTACK_BUTTON = "P3Fire1";
				USE_BUTTON = "P3Use";
				SHOW_MONEY_BUTTON = "P3ShowMoney";
				PLAYER_X_AXIS = "P3Horizontal";
				JUMP_BUTTON = "P3Jump";
				RDASH_BUTTON = "P3RDash";
				LDASH_BUTTON = "P3LDash";
				START_BUTTON = "P3Start";
				break;
			}
			case 4:
			{
				ATTACK_BUTTON = "P4Fire1";
				USE_BUTTON = "P4Use";
				SHOW_MONEY_BUTTON = "P4ShowMoney";
				PLAYER_X_AXIS = "P4Horizontal";
				JUMP_BUTTON = "P4Jump";
				RDASH_BUTTON = "P4RDash";
				LDASH_BUTTON = "P4LDash";
				START_BUTTON = "P4Start";
				break;
			}
		}
	}

	// Use this for initialization
	void Start () {

	}

	public string AttackButton()
	{
		return ATTACK_BUTTON;
	}

	public string UseButton()
	{
		return USE_BUTTON;
	}

	public string ShowMoneyButton()
	{
		return SHOW_MONEY_BUTTON;
	}

	public string playerXAxis()
	{
		return PLAYER_X_AXIS;
	}

	public string JumpButton()
	{
		return JUMP_BUTTON;
	}

	public string RDashButton()
	{
		return RDASH_BUTTON;
	}

	public string LDashButton()
	{
		return LDASH_BUTTON;
	}

	public string StartButton()
	{
		return START_BUTTON;
	}
}
