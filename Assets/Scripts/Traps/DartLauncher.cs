using UnityEngine;
using System.Collections;

public class DartLauncher : Trap {

	public Rigidbody2D dart;
	public Transform muzzle;

	public override void Activate()
	{
		Instantiate(dart, muzzle.position, muzzle.rotation);
	}
}
