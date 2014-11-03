using UnityEngine;
using System.Collections;

public class Explosive : Projectile {

	public LayerMask targetTypes;

	protected int level = 1;
	protected float CLOSE_RANGE;
	protected float LONG_RANGE;
	protected int HIGH_DAMAGE;
	protected int LOW_DAMAGE;
	protected float armingWait; // so proximity explosives don't immediately try to kill the firer

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
		for (int i = 0; i < targets.Length; i ++)
		{
			if (targets[i].gameObject != ORIGIN)
				goRight = (targets[i].transform.position - transform.position).x >= 0f;
				targets[i].GetComponent<Actor>().StunDamage(HIGH_DAMAGE, goRight);
		}
		targets = Physics2D.OverlapCircleAll(transform.position, LONG_RANGE, targetTypes); //hit far targets
		for (int i = 0; i < targets.Length; i ++)
		{
			if (targets[i].gameObject != ORIGIN)
				goRight = (targets[i].transform.position - transform.position).x >= 0f;
				targets[i].GetComponent<Actor>().StunDamage(LOW_DAMAGE, goRight);
		}
		Destroy(gameObject);
	}

	// shared code for checking proximity explosives
	protected void ExplodeCheck ()
	{
		Collider2D target = Physics2D.OverlapCircle(transform.position, CLOSE_RANGE, targetTypes);
		if (target != null && ORIGIN != target.gameObject)
			Explode();
	}
}
