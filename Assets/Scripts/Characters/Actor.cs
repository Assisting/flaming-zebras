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
	protected int POISON_DAMAGE;
	protected float POISON_TICK;	
	
	// Update is called once per frame
	void Update () {
		if (rigidbody2D.velocity.x > 0f)
			MOVING_RIGHT = true;
		else if (rigidbody2D.velocity.x < 0f)
			MOVING_RIGHT = false;
		if (CURLIFE <= 0)
			Die();
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

	private void Die()
	{
		Destroy(gameObject);
	}

	// Damage over time functions
	private void StartDot(string dotTick, float duration, string endDot)
	{
		CancelInvoke(endDot);
		Invoke(dotTick, 0.0f);
		// -1 Signifies that other action must be taken before the dot wears off; like exiting the cloud of poison
		if(-1.0f != duration){
			Invoke(endDot, duration);
		}
	}

	private void DotTick(int damageType, string tickType, float tickLength)
	{
		LifeChange (-damageType);
		Invoke (tickType, tickLength);
	}

	private void EndDot(string tickType)
	{
		CancelInvoke (tickType);
	}

	//Burn Functions
	public void Burn(int damage, float tick, float duration)
	{
		BURN_DAMAGE = damage;
		BURN_TICK = tick;
		StartDot ( "BurnTick", duration, "Extinguish");
	}

	protected void BurnTick()
	{
		DotTick (BURN_DAMAGE, "BurnTick", BURN_TICK);
	}
	
	protected void Extinguish()
	{
		EndDot ("BurnTick");
	}

	//Poison Functions
	public void Poison(int damage, float tick, float duration)
	{
		POISON_DAMAGE = damage;
		POISON_TICK = tick;
		StartDot ("PoisonTick", duration, "Squelch");
	}

	protected void PoisonTick()
	{
		DotTick (POISON_DAMAGE, "PoisonTick", POISON_TICK);
	}

	public void LeavePoison(float delay){
		//Here -1 signifies instant removal
		if(-1.0f == delay) {
			Squelch ();
		} else {
			Invoke("Squelch", delay);
		}
	}

	public void Squelch()
	{
		EndDot ("PoisonTick");
	}

	//Movement Functions
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
