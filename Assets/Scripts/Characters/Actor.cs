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
	//These aren't used anywhere, commenting them out to avoid unity complaining about them not being used -- Thor
	//protected float burnTicker;
	//protected float burnDuration;
	

	// Use this for initialization
	void Start () {
	
	}
	
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
	private void StartDot(int damage, int damageType, float tick, float tickType, float duration, string dotTick, string endDot)
	{
		CancelInvoke(endDot);
		damageType = damage;
		tickType = tick;
		Invoke(dotTick, tickType);
		// -1 Signifies that other action must be taken before the dot wears off; like exiting the cloud of poison
		if(-1.0f != duration){
			Invoke(endDot, duration);
		}
	}

	private void DotTick(int damageType, string tickType)
	{
		LifeChange (-damageType);
		Invoke (tickType, damageType);
	}

	private void EndDot(string tickType)
	{
		CancelInvoke (tickType);
	}

	/*public void Burn(int damage, float tick, float duration)
	{
		CancelInvoke("Extinguish");
		BURN_DAMAGE = damage;
		BURN_TICK = tick;
		Invoke("BurnTick", BURN_TICK);
		Invoke("Extinguish", duration);
	}*/

	//Burn Functions
	public void Burn(int damage, float tick, float duration)
	{
		StartDot (damage, BURN_DAMAGE, tick, BURN_TICK, duration, "BurnTick", "Extinguish");
	}

	/*protected void BurnTick()
	{
		LifeChange(-BURN_DAMAGE);
		Invoke("BurnTick", BURN_TICK);
	}*/

	protected void BurnTick()
	{
		DotTick (BURN_DAMAGE, "BurnTick");
	}

	/*protected void Extinguish()
	{
		CancelInvoke("BurnTick");
	}*/
	
	protected void Extinguish()
	{
		EndDot ("BurnTick");
	}

	//Poison Functions
	public void Poison(int damage, float tick, float duration)
	{
		StartDot (damage, POISON_DAMAGE, tick, POISON_TICK, duration, "PoisonTick", "Squelch");
	}

	protected void PoisonTick()
	{
		DotTick (POISON_DAMAGE, "PoisonTick");
	}

	public void LeavePoison(float delay){
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
