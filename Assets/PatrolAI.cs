using UnityEngine;
using System.Collections;

public class PatrolAI : MonoBehaviour {

	public float speed;

	public Transform wallSightStart, wallSightEnd;
	public Transform dropSightStart, dropSightEnd;

	public bool shouldITurn;

	// Use this for initialization
	void Start () {
		speed = 1f;
		shouldITurn = false;
	}
	
	// Update is called once per frame
	void Update () {
		bool impendingWall, impendingDrop;
	
		rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);

		// Let's make sure we don't smack into walls!
		impendingWall = Physics2D.Linecast (wallSightStart.position, wallSightEnd.position);

		// Let's make sure we don't fall to our death!
		impendingDrop = Physics2D.Linecast (dropSightStart.position, dropSightEnd.position);
		// A lack of collision is actually what signals we need to turn, thus the Not.
		impendingDrop = !impendingDrop;

		shouldITurn = (impendingWall || impendingDrop);

		if (shouldITurn == true) {
			transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
			speed = speed * -1;
		}
	}
}
