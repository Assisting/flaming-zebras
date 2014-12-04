using UnityEngine;
using System.Collections;

public class FallingBoulder : Trap{

	private GameObject activator;

	//falling boulder trap kills you by flattening

	public override void Activate(GameObject user)
	{
		rigidbody2D.isKinematic = false;
		activator = user;
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (!rigidbody2D.isKinematic)
		{
			if (other.tag == "Player")
			{
				PlayerData script = other.GetComponent<PlayerData>();
				if (script.IsGrounded())
				{
					script.LifeChange(-script.MAXLIFE);
					if (other.gameObject != activator)
						script.SetLastHit(activator);
				}	
			}
			else if (other.tag == "Enemy")
			{
				Actor script = other.GetComponent<Actor>();
				script.LifeChange(-script.MAXLIFE);
			}
		}
	}
}
