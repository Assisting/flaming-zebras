using UnityEngine;
using System.Collections;

public class FallingBoulder : Trap {

	//falling boulder trap kills you by flattening

	public override void Activate()
	{
		rigidbody2D.isKinematic = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (!rigidbody2D.isKinematic)
		{
			if (other.tag == "Player")
			{
				PlayerData script = other.GetComponent<PlayerData>();
				script.LifeChange(-script.MAXLIFE);
			}
			else if (other.tag == "Enemy")
			{
				Actor script = other.GetComponent<Actor>();
				script.LifeChange(-script.MAXLIFE);
			}
		}
	}
}
