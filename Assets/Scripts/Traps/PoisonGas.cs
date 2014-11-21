using UnityEngine;
using System.Collections;

public class PoisonGas : MonoBehaviour {

	private int GAS_DAMAGE = 4;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			//TODO start poison
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			//TODO stop poison
		}
	}
}
