using UnityEngine;
using System.Collections;

public class DashShop : StatShop {

	// Use this for initialization
	void Start () {
		stat = PlayerData.Attribute.Dash;
		DisplayData = "Dash Shop Probably does something...\nTrust me\nBecause I would never lie to you" +
			"\nAnd I'm the only one you can trust";
	}
}
