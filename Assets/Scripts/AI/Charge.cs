using UnityEngine;
using System.Collections;

public class Charge : Enemy {

	public delegate void MyDelegate();
	public MyDelegate Do;

	public float CHARGE_ATTACK_SPEED_MODIFIER;
	public float CHARGE_ATTACK_DAMAGE_MODIFIER;


	void Start () {
		MOVING_RIGHT = true;
		ATTACK_DELAY = 1f;

		// These will all require much, much tweaking.
		SPEED = 0.8f;
		ATTACK_DAMAGE = 6;

		// How fast is a charge attack compared to normal movement?
		CHARGE_ATTACK_SPEED_MODIFIER = 1.33f;

		// How much more damage does a charge attack do to the player?
		CHARGE_ATTACK_DAMAGE_MODIFIER = 1.5f;

		// How much health, proportional to the player, should this enemy have?
		float lifeScaleFactor = 0.4f;

		MAXLIFE = (int)((float)MAXLIFE * lifeScaleFactor);
		CURLIFE = MAXLIFE;

		Do = testCanSeePlayer;

		LAST_ATTACK_TIME = Time.time;

		RANDOM_WALK_TIMER = Time.time;
		RANDOM_WALK_MIN = 5f;
		RANDOM_WALK_MAX = 10f;

		playerLayer = 1 << LayerMask.NameToLayer ("Player");
		groundLayer = 1 << LayerMask.NameToLayer ("Ground");
	}

	void FixedUpdate () {
		Do();
	}

	// Branch
	void testCanSeePlayer() {
		if(sightCheck()) {
			Do = testInChargeRange;
		}
		else {
			Do = testReturnToPatrol;
		}
	}

	// Branch
	void testPlayerInRange() {
		if(rangeCheck()) {
			Do = attack;
		}
		else {
			Do = testDistanceToGold;
		}
	}

	// Branch
	void testReturnToPatrol() {
		if(closeToGold()) {
			Do = testRandomWalk;
		}
		else {
			Do = returnToGold;
		}
	}

	// Branch
	void testRandomWalk() {
		if(randomWalk()) {
			Do = goForWalk;
		}
		else {
			Do = standStill;
		}
	}

	// Branch
	void testInChargeRange() {
		if(chargeRangeCheck()) {
			Do = chargeAttack;
		}
		else {
			Do = testPlayerInRange;
		}
	}

	// Branch
	void testDistanceToGold() {
		if(closeToGold()) {
			Do = approach;
		}
		else {
			Do = guard;
		}
	}

	// Terminal
	void attack() {

		if(!doINeedToTurn()) {
			rigidbody2D.velocity = new Vector2 (SPEED, rigidbody2D.velocity.y);
		}
		else {
			// This should literally never happen
			print ("Somehow I am registering a player in attack range but can't get to them");
				
			turnAround();
			rigidbody2D.velocity = new Vector2 (SPEED, rigidbody2D.velocity.y);
				
		}
		if(LAST_ATTACK_TIME + ATTACK_DELAY <= Time.time) {
			if(playerInCollider == true) {
				print ("Attack!");
				// The negation is just due to the differences in LifeChange and StunDamage.
				playerCollider.gameObject.GetComponent<Actor>().LifeChange(ATTACK_DAMAGE * -1);
				LAST_ATTACK_TIME = Time.time;
				Do = testCanSeePlayer;
			}	
		}

		// Escape if neccesary
		if(LAST_ATTACK_TIME + 5f <= Time.time) {
			Do = testCanSeePlayer;
		}
	}

	// Terminal
	void returnToGold() {
		// First off, how do we get back to the position
		bool amIRightOfTheGold = (this.transform.localPosition.x > 0);

		// Right of the gold, moving right; must turn around
		if(amIRightOfTheGold && MOVING_RIGHT) {
			turnAround();
			rigidbody2D.velocity = new Vector2 (SPEED, rigidbody2D.velocity.y);
		} // Right of the gold, moving left; must keep moving
		else if(amIRightOfTheGold && (!MOVING_RIGHT)) {
			rigidbody2D.velocity = new Vector2 (SPEED, rigidbody2D.velocity.y);
		} // Left of the gold, moving right; keep moving
		else if((!amIRightOfTheGold) && MOVING_RIGHT) {
			rigidbody2D.velocity = new Vector2 (SPEED, rigidbody2D.velocity.y);
		} //Left of the gold, moving left; must turn around
		else if((!amIRightOfTheGold) && (!MOVING_RIGHT)) {
			turnAround();
			rigidbody2D.velocity = new Vector2 (SPEED, rigidbody2D.velocity.y);
		} // ...what?
		else {
			print("Screwed up in the returnToGold() routine");
		}

		Do = testCanSeePlayer;
	}

	// Terminal
	void goForWalk() {
		if(!doINeedToTurn()) {
			rigidbody2D.velocity = new Vector2 (SPEED, rigidbody2D.velocity.y);
		}
		else {
			turnAround();
			rigidbody2D.velocity = new Vector2 (SPEED, rigidbody2D.velocity.y);
		}

		Do = testCanSeePlayer;
	}

	// Terminal
	void standStill() {
		rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);

		Do = testCanSeePlayer;
	}

	// Terminal
	void chargeAttack() {
		if(!doINeedToTurn()) {
			rigidbody2D.velocity = new Vector2 (SPEED*CHARGE_ATTACK_SPEED_MODIFIER, rigidbody2D.velocity.y);
		}
		else {
			// I really need to think of a better way to prevent this. In the mean time we'll just have to try to design around it.
			print ("Can't reach player in charge attack");

			turnAround();
			rigidbody2D.velocity = new Vector2 (SPEED, rigidbody2D.velocity.y);
		}
		// Charge attacks have a slightly longer cooldown
		if(LAST_ATTACK_TIME + ATTACK_DELAY * 1.2f <= Time.time) {
			if(playerInCollider == true) {
				print ("Charge Attack!");
				playerCollider.gameObject.GetComponent<Actor>().StunDamage(
					(int)((float)ATTACK_DAMAGE * CHARGE_ATTACK_DAMAGE_MODIFIER), MOVING_RIGHT);
				LAST_ATTACK_TIME = Time.time;
				Do = testCanSeePlayer;
			}	
		}


	}

	// Terminal
	void approach() {
		if(!doINeedToTurn()) {
			rigidbody2D.velocity = new Vector2 (SPEED*1.2f, rigidbody2D.velocity.y);
		}
		else {
			turnAround();
			rigidbody2D.velocity = new Vector2 (SPEED, rigidbody2D.velocity.y);
		}

		Do = testCanSeePlayer;
	}

	// Terminal
	void guard() {
		// At the moment guarding is just standing still until the player leaves, though at some later
		// date more logic may be added so that they guard for a set amount of time after the player leaves
		rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);

		Do = testCanSeePlayer;
	}

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Player"){
			playerInCollider = true;
			playerCollider = other.collider;
		}
	}
	
	void OnCollisionExit2D(Collision2D other){
		if(other.gameObject.tag == "Player"){
			playerInCollider = false;
			playerCollider = null;
		}
	}
}
