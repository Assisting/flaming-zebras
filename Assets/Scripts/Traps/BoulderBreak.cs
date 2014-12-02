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
		gameObject.collider2D.enabled = false;
		anim.SetTrigger("Die");
	}

	private void Break()
	{
		Destroy(gameObject);
	}
}
