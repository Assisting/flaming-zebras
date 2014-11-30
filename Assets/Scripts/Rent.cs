using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rent : MonoBehaviour {

	public static Dictionary<int, Rent> rents;

	public static int RENT_COST = 10;
	public static float GRACE = 2f;
	public static float TICKTIME = 1f;

	public static void AddRent(GameObject player)
	{
		
	}

//	void Start()
//	{
//		InvokeRepeating("RentPay", GRACE, TICKTIME);
//	}

//	private void RentPay()
//	{
//		patron.GetComponent<PlayerData>().ChangeMoney(-RENT_COST);
//	}

//	public void StopPay()
//	{
//		CancelInvoke("RentPay");
//	}
}
