using UnityEngine;
using System.Collections;

public class PatrolAI : Enemy {

	public delegate void MyDelegate();
	public MyDelegate Do;

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();

		MOVING_RIGHT = true;
		ATTACK_DELAY = 1f;
		
		// These will all require much, much tweaking.
		SPEED = 0.9f;
		ATTACK_DAMAGE = 4;
		
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
			Do = testPlayerInRange;
		}
		else {
			Do = testAtObstacle;
		}
	}

	// Branch
	void testPlayerInRange() {
		if(rangeCheck()) {
			Do = attack;
		}
		else {
			Do = approach;
		}
	}

	// Branch
	void testAtObstacle() {
		if(doINeedToTurn()) {
			Do = atLedge;
		}
		else {
			Do = continuePatrol;
		}
	}

	// Branch
	void testCanGetToPlayer() {
		// Currently not implemented. This is basically for little "waist-high" obstacles and pits.
		// For now it's all just handled in approach()
	}

	// Branch
	void testRandomWalk() {
		// Handled through the atLedge() logic
	}

	// Terminal
	void attack() {
		animator.SetBool ("attacking", true);
		if(!doINeedToTurn()) {
			rigidbody2D.velocity = new Vector2 (SPEED, rigidbody2D.velocity.y);
		}
		else {
			// This should literally never happen

			
			turnAround();
			rigidbody2D.velocity = new Vector2 (SPEED, rigidbody2D.velocity.y);
			
		}
		if(LAST_ATTACK_TIME + ATTACK_DELAY <= Time.time) {
			if(playerInCollider == true) {

				// The negation is just due to the differences in LifeChange and StunDamage.
				playerCollider.gameObject.GetComponent<Actor>().LifeChange(ATTACK_DAMAGE * -1);
				LAST_ATTACK_TIME = Time.time;
				Do = testCanSeePlayer;
				animator.SetBool ("attacking", false);
			}	
		}
		
		// Escape if neccesary
		if(LAST_ATTACK_TIME + 5f <= Time.time) {
			Do = testCanSeePlayer;
			animator.SetBool ("attacking", false);
		}
	}

	// TODO: Add a slight pause before continuing
	void atLedge() {
		bool shouldIPause = (Random.value > 0.05f);
		if(shouldIPause == true) {
			animator.SetBool("walking", false);
			rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
		}
		else {
			turnAround();
			animator.SetBool("walking", true);
			rigidbody2D.velocity = new Vector2 (SPEED, rigidbody2D.velocity.y);
		}

		Do = testCanSeePlayer;
	}

	// Terminal
	void approach() {
		animator.SetBool ("walking", true);
		if(!doINeedToTurn()) {
			rigidbody2D.velocity = new Vector2 (SPEED*1.5f, rigidbody2D.velocity.y);
		}
		else {
			turnAround();
			rigidbody2D.velocity = new Vector2 (SPEED, rigidbody2D.velocity.y);
		}
		
		Do = testCanSeePlayer;
	}

	// Terminal
	void continuePatrol() {
		animator.SetBool ("walking", true);
		if(!doINeedToTurn()) {
			rigidbody2D.velocity = new Vector2 (SPEED, rigidbody2D.velocity.y);
		}
		else {
			turnAround();
			rigidbody2D.velocity = new Vector2 (SPEED, rigidbody2D.velocity.y);
		}
		
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
