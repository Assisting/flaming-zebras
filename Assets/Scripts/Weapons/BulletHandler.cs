using UnityEngine;
using System.Collections;

public class BulletHandler : Projectile {

	protected int BULLET_DAMAGE = 12; //damage per bullet
	protected float BULLET_VELOCITY = 15f; // speed of bullets in-game

	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = transform.right * BULLET_VELOCITY;
		Destroy(gameObject, 6);
	}

	// Bullet hits something
	void OnTriggerEnter2D (Collider2D other) {
		string tagHit = other.tag;
		if (tagHit == "Player")
		{
			if (ORIGIN != other.gameObject)
			{
				PlayerData target = other.GetComponent<PlayerData>();
				target.SetLastHit(ORIGIN);
				target.LifeChange(-BULLET_DAMAGE);
				Destroy(gameObject);
			}
		}
		if (tagHit == "Enemy")
		{
			other.GetComponent<Actor>().StunDamage(BULLET_DAMAGE, rigidbody2D.velocity.x > 0f);
			Destroy(gameObject);
		}
		if (tagHit == "Platform")
			Destroy(gameObject);
	}
}


