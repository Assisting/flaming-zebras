using UnityEngine;
using System.Collections;

public class DashShop : StatShop {

	// Use this for initialization
	void Start () {
		stat = PlayerData.Attribute.Dash;
		price = 50;
		DisplayData = 
			"Dash Shop: \n" +
			"Increases Dash speed\n" +
				"Price: " + price.ToString();

	}
}
