using UnityEngine;
using System.Collections;

public class PoisonGas : MonoBehaviour {

	private int GAS_DAMAGE = 4;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			PlayerData playerScript = other.GetComponent<PlayerData>();
			playerScript.Poison(GAS_DAMAGE, 1.0f, -1.0f);
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			PlayerData playerScript = other.GetComponent<PlayerData>();
			playerScript.LeavePoison(2.0f);
		}
	}
}
