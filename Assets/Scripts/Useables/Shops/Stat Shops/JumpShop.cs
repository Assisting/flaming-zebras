using UnityEngine;
using System.Collections;

public class JumpShop : StatShop {

	// Use this for initialization
	void Start () {
		price = 100;
		stat = PlayerData.Attribute.Jump;
		DisplayData = 
			"Jump Shop\n" +
			"Level 1: No Air-Jump\n" +
			"Level 2: 1 Air-Jump\n" +
			"Level 3: 2 Air-Jumps\n" +
				"Price: " + price.ToString();
	}
}
