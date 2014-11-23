using UnityEngine;
using System.Collections;

public class DashShop : StatShop {

	// Use this for initialization
	void Start () {
		price = 100;
		stat = PlayerData.Attribute.Dash;
		DisplayData = 
			"Dash Shop: \n" +
			"Increases Dash speed\n" +
				"Price: " + price.ToString();

	}
}
