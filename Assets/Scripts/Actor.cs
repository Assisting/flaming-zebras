using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {

	public int LIFE = 0;
	protected bool MOVING_RIGHT;

	protected float STUN_FORCE = 1f;

	protected bool burning = false;
	protected int BURN_DAMAGE;
	protected float BURN_TICK;
	protected float burnTicker;
	protected float burnDuration;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (rigidbody2D.velocity.x > 0f)
			MOVING_RIGHT = true;
		else if (rigidbody2D.velocity.x < 0f)
			MOVING_RIGHT = false;
	}

	void FixedUpdate ()
	{
		if (burning)
			if (burnTicker < Time.time)
			{
				LifeChange(-BURN_DAMAGE);
				burnTicker = Time.time + BURN_TICK;
			}
			if (burnDuration < Time.time)
				burning = false;
	}

	// give damage while also doing a knock-back effect
	public void StunDamage(int value)
	{
		rigidbody2D.AddForce(new Vector2(1f, 2f) * STUN_FORCE, ForceMode2D.Impulse);
		LifeChange(-value);
	}

	// Alter life total by the given amount
	public void LifeChange(int value)
	{
		LIFE += value;
	}

	public void Burn(int damage, float tick, float duration)
	{
		BURN_DAMAGE = damage;
		BURN_TICK = tick;
		burnTicker = Time.time + tick;
		burnDuration = Time.time + duration;
		burning = true;
	}

	public bool IsMovingRight()
	{
		return MOVING_RIGHT;
	}
}
