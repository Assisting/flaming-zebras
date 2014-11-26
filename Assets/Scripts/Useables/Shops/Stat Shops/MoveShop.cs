using UnityEngine;
using System.Collections;

public class MoveShop : StatShop {

	// Use this for initialization
	public override void Start () {
		base.Start();
		stat = PlayerData.Attribute.Move;
		DisplayData = 
			"Move Shop\n" +
			"Move... FASTAR\n" +
				"Price: ";
	}
}
