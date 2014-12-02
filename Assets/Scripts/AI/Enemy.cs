using UnityEngine;
using System.Collections;

public abstract class Enemy : Actor
{
	public Transform eye, sight, range, charge, wall, ground, hindsight;
	public bool canISeePlayer, amICloseToGold, shouldIRandomWalk, playerInRange, inChargeRange;
	public int playerLayer, groundLayer;

	// This is gonna require a looooot of tweaking
	float FAR_FROM_GOLD = 1f;

	public int ATTACK_DAMAGE;
	public float ATTACK_DELAY;
	public float SPEED;

	public float RANDOM_WALK_TIMER;
	// Timer bounds
	public float RANDOM_WALK_MIN;
	public float RANDOM_WALK_MAX;

	public float LAST_ATTACK_TIME;

	public bool playerInCollider;
	public Collider2D playerCollider;

	public bool checkForFloor;
	public bool checkForWall;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public bool doINeedToTurn() {
		checkForFloor = Physics2D.Linecast (eye.position, ground.position, groundLayer);
		checkForWall = Physics2D.Linecast (eye.position, wall.position, groundLayer);

		// The negation is because a lack of floor signals a pit
		return ((!checkForFloor) || checkForWall);
	}

	// Test
	public bool rangeCheck() {
		playerInRange = Physics2D.Linecast (eye.position, range.position, playerLayer);
		
		return playerInRange;
	}
	
	// Test
	public bool chargeRangeCheck() {
		inChargeRange = Physics2D.Linecast (charge.position, sight.position, playerLayer);
		
		return inChargeRange;
	}
	
	// Test
	public bool randomWalk() {
		// We only want to change this if it's been a while since the last change in randomwalk.
		if(RANDOM_WALK_TIMER <= Time.time) {
			shouldIRandomWalk = (Random.value > 0.25f);

			RANDOM_WALK_TIMER = Time.time + Random.Range(RANDOM_WALK_MIN, RANDOM_WALK_MAX);
		}

		return shouldIRandomWalk;
	}
	
	// Test
	public bool sightCheck() {
		bool behindMe = Physics2D.Linecast (eye.position, hindsight.position, playerLayer);

		if(behindMe == true) {
			turnAround();
		}

		canISeePlayer = Physics2D.Linecast (eye.position, sight.position, playerLayer);

		return canISeePlayer;
	}
	
	// Test
	public bool closeToGold() {
		amICloseToGold = ((System.Math.Abs(this.transform.localPosition.x)) <= FAR_FROM_GOLD);
		
		return amICloseToGold;
	}

	public void turnAround() {
		transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
		SPEED = SPEED * -1;
		MOVING_RIGHT = (!MOVING_RIGHT);
	}

}

