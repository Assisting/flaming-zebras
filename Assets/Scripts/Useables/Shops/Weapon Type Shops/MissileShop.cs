using UnityEngine;
using System.Collections;

public class MissileShop : WeaponShop {
	
	// Use this for initialization
	public override void Start () {
		base.Start();
		weapon = Weapon.WeaponType.Missile;
		DisplayData = 
			"Missile Shop\n" +
			" -Switch to Missile\n" +
			"1: Unguided\n" +
			"2: Guided\n" +
			"3: Swarm\n" +
				"Price: ";
	}
}
