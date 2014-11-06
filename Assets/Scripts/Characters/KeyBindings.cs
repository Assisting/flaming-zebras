using UnityEngine;
using System.Collections;

public class KeyBindings : MonoBehaviour {

	//Keybindings
	private string ATTACK_BUTTON; //mapping for attack/shoot button
	private string BUY_BUTTON; //mapping for buy button
	private string PLAYER_X_AXIS; //mapping for thumbstick
	private string JUMP_BUTTON; //mapping for jump button
	private string RDASH_BUTTON; //mapping for dash button (right)
	private string LDASH_BUTTON; //mapping for dash button (left)

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
				BUY_BUTTON = "P1Buy";
				PLAYER_X_AXIS = "P1Horizontal";
				JUMP_BUTTON = "P1Jump";
				RDASH_BUTTON = "P1RDash";
				LDASH_BUTTON = "P1LDash";
				break;
			}
			case 2:
			{
				ATTACK_BUTTON = "P2Fire1";
				BUY_BUTTON = "P2Buy";
				PLAYER_X_AXIS = "P2Horizontal";
				JUMP_BUTTON = "P2Jump";
				RDASH_BUTTON = "P2RDash";
				LDASH_BUTTON = "P2LDash";
				break;
			}
			case 3:
			{
				ATTACK_BUTTON = "P3Fire1";
				BUY_BUTTON = "P3Buy";
				PLAYER_X_AXIS = "P3Horizontal";
				JUMP_BUTTON = "P3Jump";
				RDASH_BUTTON = "P3RDash";
				LDASH_BUTTON = "P3LDash";
				break;
			}
			case 4:
			{
				ATTACK_BUTTON = "P4Fire1";
				BUY_BUTTON = "P4Buy";
				PLAYER_X_AXIS = "P4Horizontal";
				JUMP_BUTTON = "P4Jump";
				RDASH_BUTTON = "P4RDash";
				LDASH_BUTTON = "P4LDash";
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

	public string BuyButton()
	{
		return BUY_BUTTON;
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
}
