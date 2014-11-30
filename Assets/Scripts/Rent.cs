using UnityEngine;
using System.Collections;

public class Rent : MonoBehaviour {

	private GameObject patron;

	public static int RENT_COST = 10;
	public static float GRACE = 20f;
	public static float TICKTIME = 1f;

	void Start()
	{
		InvokeRepeating("RentPay", GRACE, TICKTIME);
	}

	public Rent(GameObject newPatron)
	{
		patron = newPatron;
	}

	private void RentPay()
	{
		patron.GetComponent<PlayerData>().ChangeMoney(-RENT_COST);
	}

	public void StopPay()
	{
		CancelInvoke("RentPay");
	}
}
