using UnityEngine;
using System.Collections;

public class WeaponShop : Shop {

	protected Weapon.WeaponType weapon;
	protected int BASE_PRICE = 25;

	public override void Start()
	{
		base.Start();
		price = 75;
	}

	public override void Use(GameObject caller)
	{
		if (caller.tag == "Player") //only acknowledge players
		{
			PlayerData playerData = caller.GetComponent<PlayerData>();
			if (playerData.GetMoney() >= GetPrice(caller) && weapon != playerData.GetWeaponType())
			{
				PlayerData script = caller.GetComponent<PlayerData>();
				playerData.ChangeMoney(-GetPrice(caller));
				script.LevelUp(PlayerData.Attribute.WeaponType, weapon);
				script.IncrementWeaponSwaps();
				audio.Play();
			}
		}
	}

	public override int GetPrice(GameObject caller)
	{
		return  BASE_PRICE + (price * caller.GetComponent<PlayerData>().GetWeaponSwaps());
	}
}
