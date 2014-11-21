using UnityEngine;
using System.Collections;

public class WeaponShop : Usable {

	protected Weapon.WeaponType weapon;

	public override void Use(GameObject caller)
	{
		if (caller.tag == "Player") //only acknowledge players
		{
			caller.GetComponent<PlayerData>().LevelUp(PlayerData.Attribute.WeaponType, weapon);
		}
	}
}
