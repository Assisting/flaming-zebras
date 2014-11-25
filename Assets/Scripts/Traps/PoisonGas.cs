using UnityEngine;
using System.Collections;

public class PoisonGas : MonoBehaviour {

	private int GAS_DAMAGE = 6;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			Actor playerScript = other.transform.root.gameObject.GetComponent<Actor>();
			playerScript.Poison(GAS_DAMAGE, 0.5f, -1.0f);
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			Actor playerScript = other.transform.root.gameObject.GetComponent<Actor>();
			playerScript.LeavePoison(2.0f);
		}
	}
}
