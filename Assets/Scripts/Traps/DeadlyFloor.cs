using UnityEngine;
using System.Collections;

public class DeadlyFloor : MonoBehaviour {

	//spikes that KILL you

	public void OnTriggerEnter2D(Collider2D other)
	{
		switch (other.tag)
		{
			case "Player":
			{
				PlayerData script = other.GetComponent<PlayerData>();
				script.LifeChange(-script.MAXLIFE);
				break;
			}
			case "Enemy":
			{
				Actor script = other.GetComponent<Actor>();
				script.LifeChange(-script.MAXLIFE);	
				break;
			}
			default:
			{
				break;
			}
		}
	}
}
