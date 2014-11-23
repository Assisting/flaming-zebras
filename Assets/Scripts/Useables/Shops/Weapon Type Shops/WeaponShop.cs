using UnityEngine;
using System.Collections;

public class WeaponShop : Shop {

	protected Weapon.WeaponType weapon;
	protected int BASE_PRICE = 25;

	void Start()
	{
		price = 75;
	}

	public override void Use(GameObject caller)
	{
		if (caller.tag == "Player") //only acknowledge players
		{
			PlayerData playerData = caller.GetComponent<PlayerData>();
			if (playerData.GetMoney() >= price && weapon != playerData.GetWeaponType())
			{
				PlayerData script = caller.GetComponent<PlayerData>();
				script.LevelUp(PlayerData.Attribute.WeaponType, weapon);
				playerData.ChangeMoney(-( BASE_PRICE + (price * script.GetWeaponSwaps()) ));
				script.IncrementWeaponSwaps();
			}
		}
	}
}
