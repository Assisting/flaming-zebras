using UnityEngine;
using System.Collections;

public class BombShop : WeaponShop {

	// Use this for initialization
	void Start () {
		weapon = Weapon.WeaponType.Bomb;
		DisplayData = "1: Timer\n2: Proximity\n3: Cluster";
		}
}
