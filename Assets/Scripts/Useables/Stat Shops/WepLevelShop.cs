using UnityEngine;
using System.Collections;

public class WepLevelShop : StatShop {

	// Use this for initialization
	void Start () {
		stat = PlayerData.Attribute.WeaponLevel;
		DisplayData = 
			"Weapon Upgrade Shop\n" +
				"Upgrades your weapon\n" +
				"  -Weapon will receive\n" +
				"   bonuses for higher\n" +
				"   levels, as outlined\n" +
				"   in their shops\n" +
				"Price: " + price.ToString();
	}

	void setPrice(int weaponLevel)
	{
		switch (weapLevel)
		{
		
		}
	}
}
