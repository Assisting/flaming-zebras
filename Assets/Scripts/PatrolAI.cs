using UnityEngine;
using System.Collections;
using System;

public class PatrolAI : MonoBehaviour {

	public float speed;
	private int playerLayer;

	public Transform wallSightStart, wallSightEnd;
	public Transform dropSightStart, dropSightEnd;
	public Transform playerSightStart, playerSightEnd;

	public float eventTime;

	public bool shouldITurn, canSeePlayer;
	private bool impendingWall, impendingDrop;
	private bool shouldWalk;

	private System.Random RNG;

	// Use this for initialization
	void Start () {
		speed = 1f;
		shouldITurn = false;
		shouldWalk = false;

		// Get the player's layer for the raycast; 8 is currently the layer the player is on
		playerLayer = 1 << LayerMask.NameToLayer ("Player");
		eventTime = Time.time;

		RNG = new System.Random ();
	}

	void turnAround() {
		transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
		speed = speed * -1;
	}
	
	// Update is called once per frame
	void Update () {
		// Let's make sure we don't smack into walls!
		impendingWall = Physics2D.Linecast (wallSightStart.position, wallSightEnd.position);

		// Let's make sure we don't fall to our death!
		impendingDrop = Physics2D.Linecast (dropSightStart.position, dropSightEnd.position);
		// A lack of collision is actually what signals we need to turn, thus the Not.
		impendingDrop = !impendingDrop;

		shouldITurn = (impendingWall || impendingDrop);

		if (shouldITurn == true) {
			turnAround ();
		}

		if (Time.time > eventTime) {
			eventTime += RNG.Next(3, 8);
			// Uncomment for debug output
			print (eventTime);
			shouldWalk = !shouldWalk;
			if(eventTime % 2 == 0) {
				turnAround ();
			}
			print ("I'm going for a walk:" + shouldWalk);
		}

		canSeePlayer = Physics2D.Linecast(playerSightStart.position, playerSightEnd.position, playerLayer);
		if (canSeePlayer == true) {
			rigidbody2D.velocity = new Vector2 ((speed * 2), rigidbody2D.velocity.y);
		}
		else if (shouldWalk == true){
			rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
		}
		else {
			rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
		}
	}
}
