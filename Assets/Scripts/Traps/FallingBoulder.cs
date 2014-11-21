using UnityEngine;
using System.Collections;

public class FallingBoulder : Trap {

	public override void Activate()
	{
		rigidbody2D.isKinematic = true;
	}
}
