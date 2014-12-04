using UnityEngine;
using System.Collections;

public class Explosive : Projectile {

	public LayerMask targetTypes;

	protected int level = 1;
	protected float CLOSE_RANGE;
	protected float LONG_RANGE;
	protected int HIGH_DAMAGE;
	protected int LOW_DAMAGE;
	protected bool armed = false;

	// Called after instantiation to allow levelled logic/functionality
	public void SetLevel(int value)
	{
		level = value;
	}

	// Destroys the explosive, damaging all players in the vicinity
	public void Explode()
	{
		// applies damage twice to inner people, they will therefore take both HIGH and LOW damage simultaneously
		Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, CLOSE_RANGE, targetTypes); //hit close targets
		bool goRight = true; //whether or not to stunDamage() to the right (or left)
		//Vector2 targetDirection = Vector2.zero; //direction to each target in explosion
		for (int i = 0; i < targets.Length; i ++)
		{
			if (targets[i].gameObject != ORIGIN)
			{
				//targetDirection = targets[i].transform.position - transform.position;
				//goRight = targetDirection.x >= 0f;
				if (targets[i].tag == "Player")
				{
					PlayerData target = targets[i].GetComponent<PlayerData>();
					target.SetLastHit(ORIGIN);
					target.StunDamage(HIGH_DAMAGE, goRight);
				}
				else
					targets[i].GetComponent<Actor>().LifeChange(-HIGH_DAMAGE);
			}
		}
		targets = Physics2D.OverlapCircleAll(transform.position, LONG_RANGE, targetTypes); //hit far targets
		for (int i = 0; i < targets.Length; i ++)
		{
			if (targets[i].gameObject != ORIGIN)
			{
				//targetDirection = targets[i].transform.position - transform.position;
				//goRight = targetDirection.x >= 0f;
				if (targets[i].tag == "Player")
				{
					PlayerData target = targets[i].GetComponent<PlayerData>();
					target.SetLastHit(ORIGIN);
					target.StunDamage(LOW_DAMAGE, goRight);
				}
				else
					targets[i].GetComponent<Actor>().LifeChange(-LOW_DAMAGE);
			}
		}
		GetComponent<Animator>().SetTrigger("Explode");
		audio.Play();
	}

	// shared code for checking proximity explosives
	protected void ExplodeCheck ()
	{
		Collider2D target = Physics2D.OverlapCircle(transform.position, CLOSE_RANGE, targetTypes);
		if (target != null && ORIGIN != target.gameObject)
		{
			CancelInvoke("Explode"); //to prevent null pointer errors
			Explode();
		}
	}

	//arm the explosive
	protected void Arm()
	{
		armed = true;
	}

	void Die()
	{
		Destroy(gameObject);
	}
}
