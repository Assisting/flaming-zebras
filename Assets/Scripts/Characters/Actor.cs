using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {

//-----Attributes------------------------------------------------------------------------------------------------------------------------------------

	public int MAXLIFE = 100;
	public int CURLIFE = 100;
	protected bool dead;
	public bool MOVING_RIGHT;

	public float STUN_FORCE = 3f;
	protected bool stunned = false;
	private bool flash = false;
	private int flashCount = 5;
	private bool visible = true;

	public bool INVULNERABLE = false;

	protected int BURN_DAMAGE;
	protected float BURN_TICK;
	protected bool BURNING;
	protected int POISON_DAMAGE;
	protected float POISON_TICK;
	protected bool POISONED;

	private delegate void DotDelegate ();

//-----Unity Functions-------------------------------------------------------------------------------------------------------------------------------

	// Update is called once per frame
	void Update () {
		if (rigidbody2D.velocity.x > 0f)
			MOVING_RIGHT = true;
		else if (rigidbody2D.velocity.x < 0f)
			MOVING_RIGHT = false;
		if (CURLIFE > MAXLIFE)
			CURLIFE = MAXLIFE; //its MAX life
		if (dead)
			Die();
	}

	void FixedUpdate () 
	{
		if(flash)
		{
			if(0 == flashCount)
			{
				visible = !visible;
				//Flip the boolean to enable/disable accordingly
				gameObject.GetComponent<Animator>().enabled = visible;
				gameObject.GetComponent<SpriteRenderer>().enabled = visible;
				//Flash every 5th physics frame
				flashCount = 5;
			} else
			{
				flashCount--;
			}
		}
	}

//-----Custom Functions------------------------------------------------------------------------------------------------------------------------------

	// give damage while also inducing a knock-back effect
	public virtual void StunDamage(int value, bool goRight)
	{
		if (!INVULNERABLE)
		{
			rigidbody2D.velocity = Vector2.zero;
			if (goRight)
				rigidbody2D.AddForce(new Vector2(1f, 2f) * STUN_FORCE, ForceMode2D.Impulse);
			else
				rigidbody2D.AddForce(new Vector2(-1f, 2f) * STUN_FORCE, ForceMode2D.Impulse);
			stunned = true;
			LifeChange(-value);
		}
	}

	// Alter life total by the given amount
	public virtual void LifeChange(int value)
	{
		if (!INVULNERABLE)
		{
			CURLIFE += value;
			if (CURLIFE > MAXLIFE)
			{
				CURLIFE = MAXLIFE;
			}
						
			if (CURLIFE <= 0)
			{
				CURLIFE = 0; 
				dead = true;
			}

			if (0 >= value)
			{
				//Start flashing to indicate damage is taken
				flash = true;
				//Duration of flashing state
				CancelInvoke("EndFlashing");
				Invoke ("EndFlashing", 1.0f);
			}
		}
	}

	protected virtual void Die()
	{
		Destroy(gameObject);
	}

	private void EndFlashing(){
		flash = false;
		flashCount = 5;
		if (!visible) 
		{
			visible = true;
			gameObject.GetComponent<Animator>().enabled = visible;
			gameObject.GetComponent<SpriteRenderer>().enabled = visible;		
		}
	}
	
	// Damage over time functions
	private void StartDot(DotDelegate dotTick, string endDot, float duration, bool ticking)
	{
		if(ticking){
			CancelInvoke(endDot);
			return;
		}
		dotTick ();
		// -1 Signifies that other action must be taken before the dot wears off; 
		//    like exiting the cloud of poison
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
		StartDot ( BurnTick, "Extinguish", duration, BURNING);
	}

	protected void BurnTick()
	{
		DotTick (BURN_DAMAGE, "BurnTick", BURN_TICK);
		BURNING = true;
	}
	
	protected void Extinguish()
	{
		EndDot ("BurnTick");
		BURNING = false;
	}
	
	public bool isBurning(){
		return BURNING;
	}

	// Poison Functions
	// Squelch turns off poison
	public void Poison(int damage, float tick, float duration)
	{
		POISON_DAMAGE = damage;
		POISON_TICK = tick;
		StartDot (PoisonTick, "Squelch", duration, POISONED);
	}

	protected void PoisonTick()
	{
		DotTick (POISON_DAMAGE, "PoisonTick", POISON_TICK);
		POISONED = true;
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
		POISONED = false;
	}
	
	public bool isPoisoned(){
		return POISONED;
	}

//-----Getters and Setters---------------------------------------------------------------------------------------------------------------------------

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

	public int GetMaxLife()
	{
		return MAXLIFE;
	}

	public int GetCurLife()
	{
		return CURLIFE;
	}
}
