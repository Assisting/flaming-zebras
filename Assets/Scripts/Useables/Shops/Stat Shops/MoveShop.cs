﻿using UnityEngine;
using System.Collections;

public class MoveShop : StatShop {

	// Use this for initialization
	void Start () {
		stat = PlayerData.Attribute.Move;
		price = 50;
		DisplayData = 
			"Move Shop\n" +
			"Move... FASTAR\n" +
				"Price: " + price.ToString();
	}
}