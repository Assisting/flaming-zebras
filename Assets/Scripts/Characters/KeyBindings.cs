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

	// Use this for initialization
	void Start () {
		//TODO get player name and set mappings
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
