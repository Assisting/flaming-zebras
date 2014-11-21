using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public Transform pairExit;
	public Transform thisExit;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			other.transform.position = pairExit.position;
		}
	}
}
