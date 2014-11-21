using UnityEngine;
using System.Collections;

public class WepLevelShop : StatShop {

	// Use this for initialization
	void Start () {
		stat = PlayerData.Attribute.WeaponLevel;
		DisplayData = "Upgrade something, I assume.\nPerhaps it's the weapon.";
	}
}
