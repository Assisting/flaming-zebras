using UnityEngine;
using System.Collections;

public class BoulderBreak : Actor {

	Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	protected override void Die()
	{
		Collider2D[] colliders = gameObject.GetComponents<Collider2D> ();
		foreach (Collider2D collider in colliders)
		{
			collider.enabled = false;
		}
		anim.SetTrigger("Die");
	}

	private void Break()
	{
		Destroy(gameObject);
	}
}
