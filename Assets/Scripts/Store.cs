using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Store : MonoBehaviour {

	private int RENT_COST = 20;
	private float GRACE = 25f;

	private LinkedList<Rent> rents;

	void Start()
	{
		patrons = new LinkedList<Rent>();
	}
	
	void onTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			PlayerData script = other.GetComponent<PlayerData>();
			script.CURLIFE = script.MAXLIFE; //full heal
			rents.AddLast(new Rent(other.gameObject));
			InvokeRepeating("Rent", GRACE, 1f);
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			CancelInvoke("Rent");
		}
	}

	private void Rent()
	{
		foreach (GameObject patron in patrons)
		{
			
		}
	}
}
