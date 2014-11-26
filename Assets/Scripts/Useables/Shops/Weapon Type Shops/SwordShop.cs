using UnityEngine;
using System.Collections;

public class SwordShop : WeaponShop {

	// Use this for initialization
	public override void Start () {
		base.Start();
		weapon = Weapon.WeaponType.Melee;
		DisplayData = 
			"Sword Shop\n" +
			" -Switch to Sword+Shield\n" +
			"1: Sword+Shield\n" +
			"2: +Damage\n" +
			"3: Low Shield Cooldown\n" +
			"Price: ";
	}
}
