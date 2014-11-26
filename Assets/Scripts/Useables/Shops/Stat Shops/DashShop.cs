using UnityEngine;
using System.Collections;

public class DashShop : StatShop {

	// Use this for initialization
	public override void Start () {
		base.Start();
		stat = PlayerData.Attribute.Dash;
		DisplayData = 
			"Dash Shop: \n" +
			"Increases Dash speed\n" +
				"Price: ";

	}
}
