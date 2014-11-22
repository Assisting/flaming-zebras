using UnityEngine;
using System.Collections;

public class BulletShop : WeaponShop {

	// Use this for initialization
	void Start () {
		weapon = Weapon.WeaponType.Bullet;
		DisplayData = 
			"Bullet Shop\n" +
			"1: Pistol\n" +
			"2: Machine Gun\n" +
			"3: Shotgun\n" +
			"Price: ";
	}
}
