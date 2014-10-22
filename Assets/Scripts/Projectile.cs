using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	protected GameObject ORIGIN;

	public void Origin(GameObject initialOrigin)
	{
		ORIGIN = initialOrigin;
	}
}
