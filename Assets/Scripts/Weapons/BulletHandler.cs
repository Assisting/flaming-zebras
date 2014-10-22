using UnityEngine;
using System.Collections;

public class BulletHandler : Projectile {

	private int BULLET_DAMAGE = 12; //damage per bullet
	private float BULLET_VELOCITY = 15f; // speed of bullets in-game

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody2D.velocity = transform.right * BULLET_VELOCITY;
	}

	// Bullet hits something
	void OnTriggerEnter2D (Collider2D other) {
		string tagHit = other.gameObject.tag;
		if (tagHit == "Player" || tagHit == "Enemy")
		{
			if (ORIGIN != other.gameObject)
			{
				other.GetComponent<Actor>().StunDamage(BULLET_DAMAGE);
				Destroy(gameObject);
			}
		}
		if (tagHit == "Wall")
			Destroy(gameObject);
	}
}


