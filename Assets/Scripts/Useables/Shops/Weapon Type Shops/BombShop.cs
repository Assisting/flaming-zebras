using UnityEngine;
using System.Collections;

public class BombShop : WeaponShop {

	// Use this for initialization
	void Start () {
		//price = 100;
		price = 75;
		weapon = Weapon.WeaponType.Bomb;
		DisplayData =
			"Bomb Shop: \n" +
			" -Switch to Bomb\n" +
			"Level 1: Timer\n" +
			"Level 2: Proximity\n" +
			"Level 3: Cluster\n" +
				"Price: " + price.ToString();

		}
}
