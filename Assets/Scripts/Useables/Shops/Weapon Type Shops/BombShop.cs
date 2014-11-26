using UnityEngine;
using System.Collections;

public class BombShop : WeaponShop {

	// Use this for initialization
	public override void Start () {
		base.Start();
		weapon = Weapon.WeaponType.Bomb;
		DisplayData =
			"Bomb Shop: \n" +
			" -Switch to Bomb\n" +
			"Level 1: Timer\n" +
			"Level 2: Proximity\n" +
			"Level 3: Cluster\n" +
				"Price: ";
	}
}
