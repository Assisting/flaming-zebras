using UnityEngine;
using System.Collections;

public class Archer : Enemy {

	public delegate void MyDelegate();
	public MyDelegate Do;

	public Transform Muzzle;
	public Rigidbody2D bullet;

	void Start () {
		MOVING_RIGHT = true;
		ATTACK_DELAY = 2f;
		
		// These will all require much, much tweaking.
		SPEED = 0.9f;
		ATTACK_DAMAGE = 4;
		
		// How much health, proportional to the player, should this enemy have?
		float lifeScaleFactor = 0.3f;
		
		MAXLIFE = (int)((float)MAXLIFE * lifeScaleFactor);
		CURLIFE = MAXLIFE;
		
		Do = testCanSeePlayer;
		
		LAST_ATTACK_TIME = Time.time;
		
		RANDOM_WALK_TIMER = Time.time;
		RANDOM_WALK_MIN = 3f;
		RANDOM_WALK_MAX = 7f;
		
		playerLayer = 1 << LayerMask.NameToLayer ("Player");
		groundLayer = 1 << LayerMask.NameToLayer ("Ground");
	}
	
	void FixedUpdate () {
		Do();
	}
	
	// Branch
	void testCanSeePlayer() {
		if(sightCheck()) {
			Do = attack;
		}
		else {
			Do = nothing;
		}
	}

	// Terminal
	void attack() {
		if(LAST_ATTACK_TIME + ATTACK_DELAY <= Time.time) {
			print ("Attack!");

			Rigidbody2D newBullet = Instantiate (bullet, Muzzle.position, Muzzle.rotation) as Rigidbody2D;
			newBullet.GetComponent<BulletHandler>().Origin(gameObject);
			LAST_ATTACK_TIME = Time.time;
			Do = testCanSeePlayer;
		}
		
		// Escape if neccesary
		if(LAST_ATTACK_TIME + 5f <= Time.time) {
			Do = testCanSeePlayer;
		}
	}

	// Terminal
	void nothing() {
		Do = testCanSeePlayer;
	}
}
