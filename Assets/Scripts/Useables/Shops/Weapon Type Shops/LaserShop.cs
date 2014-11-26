using UnityEngine;
using System.Collections;

public class LaserShop : WeaponShop {

	// Use this for initialization
	public override void Start () {
		base.Start();
		weapon = Weapon.WeaponType.Laser;
		DisplayData = 
			"Laser Shop\n" +
			" -Switch to Laser\n" +
			"1: Single target Laser\n" +
			"2: Burning DOT\n" +
			"3: Unstoppable Penetration\n" +
				"Price: ";
	}
}
