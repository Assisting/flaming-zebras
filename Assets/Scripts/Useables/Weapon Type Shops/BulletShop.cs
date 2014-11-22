using UnityEngine;
using System.Collections;

public class BulletShop : WeaponShop {

	// Use this for initialization
	void Start () {
		price = 150;
		weapon = Weapon.WeaponType.Bullet;
		DisplayData = 
			"Bullet Shop\n" +
			" -Switch to Bullet\n" +
			"1: Pistol\n" +
			"2: Machine Gun\n" +
			"3: Shotgun\n" +
				"Price: " + price.ToString();
	}
}
