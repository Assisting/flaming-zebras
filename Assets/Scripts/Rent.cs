using UnityEngine;
using System.Collections;

public class Rent : MonoBehaviour {

	private GameObject patron;

	private int RENT_COST = 10;
	private float GRACE = 2f;
	private float TICKTIME = 1f;

	void Start()
	{
		InvokeRepeating("RentPay", GRACE, TICKTIME);
	}

	public void SetPatron(GameObject patron)
	{
		this.patron = patron;
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
