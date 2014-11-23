using UnityEngine;
using System.Collections;

public class DangerFloor : MonoBehaviour {

	private int DAMAGE = 12;

	public void OnTriggerEnter2D(Collider2D other)
	{
		switch (other.tag)
		{
			case "Player":
			{
				other.GetComponent<PlayerData>().StunDamage(DAMAGE, FindRight(other));
				break;
			}
			case "Enemy":
			{
				other.GetComponent<Actor>().StunDamage(DAMAGE, FindRight(other));	
				break;
			}
			default:
			{
				break;
			}
		}
	}

	//returns true if we should stun to the right
	private bool FindRight(Collider2D other)
	{
		int rotation = (int)transform.rotation.eulerAngles.z;
		switch(rotation)
		{
			case (270): //face right
			{
				return true;
			}
			case (90): //face left
			{
				return true;
			}
			default:
			{
				return other.transform.position.x - transform.position.x >= 0f; //move shorter distance to edge of spikes	
			}
		}
	}
}
