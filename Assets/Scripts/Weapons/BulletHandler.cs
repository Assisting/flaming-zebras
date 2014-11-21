using UnityEngine;
using System.Collections;

public class BulletHandler : Projectile {

	private int BULLET_DAMAGE = 12; //damage per bullet
	private float BULLET_VELOCITY = 15f; // speed of bullets in-game

	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = transform.right * BULLET_VELOCITY;
		Destroy(gameObject, 10);
	}

	// Bullet hits something
	void OnTriggerEnter2D (Collider2D other) {
		string tagHit = other.gameObject.tag;
		if (tagHit == "Player" || tagHit == "Enemy")
		{
			if (ORIGIN != other.gameObject)
			{
				other.GetComponent<Actor>().StunDamage(BULLET_DAMAGE, rigidbody2D.velocity.x > 0f);
				Destroy(gameObject);
			}
		}
		if (tagHit == "Platform")
			Destroy(gameObject);
	}
}


