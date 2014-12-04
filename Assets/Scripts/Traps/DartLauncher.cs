using UnityEngine;
using System.Collections;

public class DartLauncher : Trap {

	public GameObject dart;
	public Transform muzzle;
	private bool firing;
	private GameObject activator;

	public override void Activate(GameObject user)
	{
		if (!firing)
		{
			firing = true;
			audio.Play();
			activator = user;
			Invoke("Shoot", audio.clip.length);
		}
	}

	private void Shoot()
	{
		GameObject newDart = Instantiate(dart, muzzle.position, muzzle.rotation) as GameObject;
		newDart.GetComponent<DartHandler>().Origin(activator);
		firing = false;
	}
}
