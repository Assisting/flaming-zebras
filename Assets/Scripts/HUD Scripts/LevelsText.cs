using UnityEngine;
using System.Collections;

public class LevelsText : MonoBehaviour {

	public PlayerData playerData;
	public GUIText Levels;
	private int weaponLv;
	private int dashLv;
	private int jumpLv;
	private int moveLv;

	// Use this for initialization
	void Start () {
		setLevelsText ();
	}
	
	// Update is called once per frame
	void Update () {
		setLevelsText ();
	}

	void setLevelsText()
	{
		weaponLv = playerData.GetWeaponLevel ();
		dashLv = playerData.GetDashLevel ();
		jumpLv = playerData.GetJumpLevel ();
		moveLv = playerData.GetMoveLevel ();
		Levels.text = 
			"Weapon Level: " + weaponLv + "    " + 
						"Dash Level: " + dashLv + "        \n" +
						"Jump Level: " + jumpLv + "       " +
						"Move Level: " + moveLv + "        ";
	}
}
