using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Store : MonoBehaviour {

	private Dictionary<GameObject, Rent> rents;

	void Start()
	{
		rents = new Dictionary<GameObject, Rent>();
	}

	void onTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			PlayerData script = other.GetComponent<PlayerData>();
			script.CURLIFE = script.MAXLIFE; //full heal
			Rent newRent = new Rent();
			newRent.SetPatron(other.gameObject);
			rents.Add(other.gameObject, newRent);
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			Rent removeRent = null;
			rents.TryGetValue(other.gameObject, out removeRent);
			if (removeRent != null)
				removeRent.StopPay();
			rents.Remove(other.gameObject);
		}
	}
}

