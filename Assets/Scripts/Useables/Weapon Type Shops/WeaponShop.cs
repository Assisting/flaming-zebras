using UnityEngine;
using System.Collections;

public class WeaponShop : Shop {

	protected Weapon.WeaponType weapon;

	public override void Use(GameObject caller)
	{
		if (caller.tag == "Player") //only acknowledge players
		{
			PlayerData playerData = caller.GetComponent<PlayerData>();
			if (playerData.GetMoney() >= price)
			{
				caller.GetComponent<PlayerData>().LevelUp(PlayerData.Attribute.WeaponType, weapon);
				playerData.ChangeMoney(-(price));
			}
		}
	}
}
