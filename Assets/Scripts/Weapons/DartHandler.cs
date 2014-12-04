using UnityEngine;
using System.Collections;

public class DartHandler : BulletHandler {

	// Use this for initialization
	void Start () {
		BULLET_DAMAGE = 18;
		BULLET_VELOCITY = 30f;
		rigidbody2D.velocity = transform.right * BULLET_VELOCITY;
		Destroy(gameObject, 6);
	}
}
