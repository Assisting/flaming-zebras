using UnityEngine;
using System.Collections;

public class MissileShop : WeaponShop {
	
	// Use this for initialization
	void Start () {
		weapon = Weapon.WeaponType.Missile;
		DisplayData = 
			"Missile Shop\n" +
			"1: Unguided\n" +
			"2: Guided\n" +
			"3: Swarm\n" +
			"Price: ";
	}
}
