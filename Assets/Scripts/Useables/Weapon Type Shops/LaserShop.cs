using UnityEngine;
using System.Collections;

public class LaserShop : WeaponShop {

	// Use this for initialization
	void Start () {
		weapon = Weapon.WeaponType.Laser;
		DisplayData = "1: Single target Laser\n2: Burning DOT\n3: Unstoppable Penetration";
	}
}
