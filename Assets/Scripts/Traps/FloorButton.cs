using UnityEngine;
using System.Collections;

public class FloorButton : MonoBehaviour {

	//this button can activate any trap

	public GameObject[] triggers;
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") //only players push buttons
		{
			audio.Play();
			foreach (GameObject trigger in triggers)
			{
				if (trigger != null)
					trigger.GetComponent<Trap>().Activate(other.gameObject);
			}
		}
	}
}
