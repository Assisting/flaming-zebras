using UnityEngine;
using System.Collections;

public class BulletShop : WeaponShop {

	// Use this for initialization
	void Start () {
		weapon = Weapon.WeaponType.Bullet;
		DisplayData = "1: Pistol\n2: Machine Gun\n3: Shotgun";
	}
}
