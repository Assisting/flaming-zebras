using UnityEngine;
using System.Collections;

public class JumpShop : StatShop {

	// Use this for initialization
	void Start () {
		stat = PlayerData.Attribute.Jump;
		DisplayData = "I think this makes you able to jump more times...\n" +
						"Don't blame me if it's wrong though";
	}
}
