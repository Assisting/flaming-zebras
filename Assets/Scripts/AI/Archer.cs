using UnityEngine;
using System.Collections;

public class Archer : Enemy {

	public delegate void MyDelegate();
	public MyDelegate Do;

	private Animator animator;
	private Actor actor;

	public Transform Muzzle;
	public Rigidbody2D bullet;

	private int attackCount;

	float DELAY_BEFORE_FIRE;
	float DELAY_BETWEEN_ATTACKS;

	GameObject[] players;

	void Start () {
		attackCount = 0;
		actor = GetComponent<Actor>();
		animator = GetComponent<Animator>();
		actor.setInvuln(true);
		actor.setStunForce(0f);

		MOVING_RIGHT = true;
		ATTACK_DELAY = 0.8f;

		DELAY_BEFORE_FIRE = 0.5f;
		DELAY_BETWEEN_ATTACKS = 5f;

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

		// Sorry, but sometimes the hackiest solution really is just the best.
		players = GameObject.FindGameObjectsWithTag("Player");
	}
	
	void FixedUpdate () {
		Do();
	}
	
	// Branch
	void testCanSeePlayer() {
		turnToNearest();
		if(sightCheck()) {
			Do = attack;
		}
		else {
			Do = nothing;
		}
	}

	// Terminal
	void attack() {
		if((Time.time > LAST_ATTACK_TIME + DELAY_BETWEEN_ATTACKS) || actor.isInvuln() == false) {

			if(actor.isInvuln() == true) {
				LAST_ATTACK_TIME = Time.time + DELAY_BEFORE_FIRE;
			}

			actor.setInvuln(false);
			animator.SetBool ("up", true);

			if(attackCount < 3) {
				if(LAST_ATTACK_TIME + ATTACK_DELAY <= Time.time) {
					Rigidbody2D newBullet = Instantiate (bullet, Muzzle.position, Muzzle.rotation) as Rigidbody2D;
					newBullet.GetComponent<BulletHandler>().Origin(gameObject);
					LAST_ATTACK_TIME = Time.time;
					attackCount += 1;
				}
			}
			else {
				actor.setInvuln(true);
				animator.SetBool ("up", false);
				attackCount = 0;
				Do = testCanSeePlayer;
			}
		}
		else {
			Do = testCanSeePlayer;
		}
	}

	// Terminal
	void nothing() {
		Do = testCanSeePlayer;
	}

	void turnToNearest() {
		GameObject nearestPlayer = null;
		float distanceToNearestPlayer = Mathf.Infinity;
		Vector3 myPosition = transform.position;
		foreach(GameObject player in players) {
			Vector3 diff = player.transform.position - myPosition;
			float curDistance = diff.sqrMagnitude;
			if(curDistance < distanceToNearestPlayer) {
				distanceToNearestPlayer = curDistance;
				nearestPlayer = player;
			}
		}

		float deltaX = this.transform.position.x - nearestPlayer.transform.position.x;

		// Currently facing left, nearest player is to the right.
		if(deltaX < 0 && MOVING_RIGHT == false) {
			Muzzle.eulerAngles = new Vector3(0,0,0);
			turnAround ();
		}

		// Currently facing right, nearest player is left.
		if(deltaX > 0 && MOVING_RIGHT == true) {
			Muzzle.eulerAngles = new Vector3(0,0,180);
			turnAround ();
		}
	}
}
