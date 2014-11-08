using UnityEngine;
using System.Collections;

public class DangerFloor : MonoBehaviour {

	private float DAMAGE;

	// Use this for initialization
	void Start () {
	
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "Projectile") //projectiles are all that move that can't be hurt
		{
			other.GetComponent<Actor>().StunDamage(DAMAGE, other.transform.position.x - transform.position.x >= 0f); //move shorter distance to edge of spikes
		}
	}
}
