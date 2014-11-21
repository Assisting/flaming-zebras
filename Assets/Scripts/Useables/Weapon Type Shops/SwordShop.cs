using UnityEngine;
using System.Collections;

public class SwordShop : WeaponShop {

	// Use this for initialization
	void Start () {
		weapon = Weapon.WeaponType.Melee;
		DisplayData = "1: Sword+Shield\n2: +Damage\n3: Low Shield Cooldown";
	}
}
