using UnityEngine;
using System.Collections;

public class BulletHandler : MonoBehaviour {

	private int BULLET_DAMAGE = 12; //damage per bullet

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		string tagHit = other.gameObject.tag;
		if (tagHit == "Player" || tagHit == "Enemy")
		{
			other.GetComponent<PlayerData>().StunDamage(-BULLET_DAMAGE);
			Destroy(gameObject);
		}
		if (tagHit == "Wall")
			Destroy(gameObject);
	}
}


