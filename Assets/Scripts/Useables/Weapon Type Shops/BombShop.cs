using UnityEngine;
using System.Collections;

public class BombShop : WeaponShop {

	// Use this for initialization
	void Start () {
		weapon = Weapon.WeaponType.Bomb;
		DisplayData =
			"Bomb Shop: \n" +
			"Level 1: Timer\n" +
			"Level 2: Proximity\n" +
			"Level 3: Cluster\n" +
			"Price: ";

		}
}
