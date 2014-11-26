using UnityEngine;
using System.Collections;

public class JumpShop : StatShop {

	// Use this for initialization
	public override void Start () {
		base.Start();
		stat = PlayerData.Attribute.Jump;
		DisplayData = 
			"Jump Shop\n" +
			"Level 1: No Air-Jump\n" +
			"Level 2: 1 Air-Jump\n" +
			"Level 3: 2 Air-Jumps\n" +
				"Price: ";
	}
}
