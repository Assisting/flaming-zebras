using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	protected GameObject ORIGIN;

	void Start() {
		Destroy(gameObject, 1);
	}

	public void Origin(GameObject initialOrigin)
	{
		ORIGIN = initialOrigin;
	}
}
