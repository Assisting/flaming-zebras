using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {

	public int MAXLIFE = 100;
	public int CURLIFE = 100;
	protected bool dead;
	protected bool MOVING_RIGHT;

	protected float STUN_FORCE = 3f;
	protected bool stunned = false;

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

	// give damage while also inducing a knock-back effect
	public virtual void StunDamage(int value, bool goRight)
	{
		rigidbody2D.velocity = Vector2.zero;
		if (goRight)
			rigidbody2D.AddForce(new Vector2(1f, 2f) * STUN_FORCE, ForceMode2D.Impulse);
		else
			rigidbody2D.AddForce(new Vector2(-1f, 2f) * STUN_FORCE, ForceMode2D.Impulse);
		stunned = true;
		LifeChange(-value);
	}

	// Alter life total by the given amount
	public virtual void LifeChange(int value)
	{
		CURLIFE += value;
		if (CURLIFE > MAXLIFE)
						CURLIFE = MAXLIFE;
		if (CURLIFE < 0)
		{
			CURLIFE = 0; 
			dead = true;
		}
	}

	// Do some Damage over Time (no knockback)
	public void Burn(int damage, float tick, float duration)
	{
		CancelInvoke("Extinguish");
		BURN_DAMAGE = damage;
		BURN_TICK = tick;
		Invoke("Extinguish", duration);
		//burning = true;
	}

	protected void Extinguish()
	{
		//burning = false;
		CancelInvoke("BurnTick");
	}

	protected void BurnTick()
	{
		LifeChange(-BURN_DAMAGE);
		Invoke("BurnTick", BURN_TICK);
	}

	public bool IsMovingRight()
	{
		return MOVING_RIGHT;
	}

	public bool isStunned()
	{
		return stunned;
	}

	public void setStunned(bool value)
	{
		stunned = value;
	}
}
