using UnityEngine;
using System.Collections;

public class DangerFloor : MonoBehaviour {

	//spikes that hurt you

	private int DAMAGE = 12;

	public void OnTriggerEnter2D(Collider2D other)
	{
		switch (other.tag)
		{
			case "Player":
			{
				PlayerData playerData = other.GetComponent<PlayerData>();
				if ( playerData.IsDashing() )
					other.GetComponent<Movement>().StopDash();
				playerData.LifeChange(-DAMAGE);
				break;
			}
			case "Enemy":
			{
				other.GetComponent<Actor>().LifeChange(-DAMAGE);
				break;
			}
			default:
			{
				break;
			}
		}
	}

	//returns true if we should stun to the right (not used with stunless spikes)
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
				return false;
			}
			default:
			{
				return other.transform.position.x - transform.position.x >= 0f; //move shorter distance to edge of spikes	
			}
		}
	}
}
