using UnityEngine;
using System.Collections;

public class SwordShop : WeaponShop {

	// Use this for initialization
	void Start () {
		//price = 200;
		price = 75;
		weapon = Weapon.WeaponType.Melee;
		DisplayData = 
			"Sword Shop\n" +
			" -Switch to Sword+Shield\n" +
			"1: Sword+Shield\n" +
			"2: +Damage\n" +
			"3: Low Shield Cooldown\n" +
			"Price: " + price.ToString();
	}
}
