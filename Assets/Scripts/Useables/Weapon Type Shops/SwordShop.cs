using UnityEngine;
using System.Collections;

public class SwordShop : WeaponShop {

	// Use this for initialization
	void Start () {
		weapon = Weapon.WeaponType.Melee;
		DisplayData = 
			"Sword Shop\n" +
			"1: Sword+Shield\n" +
			"2: +Damage\n" +
			"3: Low Shield Cooldown\n" +
			"Price: ";
	}
}
