using UnityEngine;
using System.Collections;

public class UsableShop : Usable {

	private Weapon.WeaponType weapon;

	public override void Use(GameObject caller)
	{
		if (caller.tag == "player") //only acknowledge players
		{
			caller.GetComponent<PlayerData>().LevelUp(PlayerData.Attribute.WeaponType, weapon);
		}
	}
}
