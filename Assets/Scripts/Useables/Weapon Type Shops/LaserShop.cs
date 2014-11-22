using UnityEngine;
using System.Collections;

public class LaserShop : WeaponShop {

	// Use this for initialization
	void Start () {
		weapon = Weapon.WeaponType.Laser;
		DisplayData = 
			"Laser Shop\n" +
			"1: Single target Laser\n" +
			"2: Burning DOT\n" +
			"3: Unstoppable Penetration\n" +
			"Price: ";
	}
}
