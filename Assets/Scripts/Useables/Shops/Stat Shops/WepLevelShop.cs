using UnityEngine;
using System.Collections;

public class WepLevelShop : StatShop {

	// Use this for initialization
	public override void Start () {
		base.Start();
		stat = PlayerData.Attribute.WeaponLevel;
		DisplayData = 
			"Weapon Upgrade Shop\n" +
				"Upgrades your weapon\n" +
				"  -Weapon will receive\n" +
				"   bonuses for higher\n" +
				"   levels, as outlined\n" +
				"   in their shops\n" +
				"Price: ";
	}
}
