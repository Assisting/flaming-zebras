using UnityEngine;
using System.Collections;

public class FloorButton : MonoBehaviour {

	public GameObject[] triggers;
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") //only players push buttons
		{
			foreach (GameObject trigger in triggers)
			{
				trigger.SendMessage("Activate");
			}
		}
	}
}
