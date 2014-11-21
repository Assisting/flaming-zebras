using UnityEngine;
using System.Collections;

public class MissileShop : WeaponShop {
	
	// Use this for initialization
	void Start () {
		weapon = Weapon.WeaponType.Missile;
		DisplayData = "1: Unguided\n2: Guided\n3: Swarm";
	}
}
