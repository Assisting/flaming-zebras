using UnityEngine;
using System.Collections;

public class DartLauncher : Trap {

	public Rigidbody2D dart;
	public Transform muzzle;
	private bool firing;

	public override void Activate()
	{
		if (!firing)
		{
			firing = true;
			audio.Play();
			Invoke("Shoot", audio.clip.length);
		}
	}

	private void Shoot()
	{
		Instantiate(dart, muzzle.position, muzzle.rotation);
		firing = false;
	}
}
