using UnityEngine;
using System.Collections;

public class Rent : MonoBehaviour {

	private GameObject patron;

	private int RENT_COST = 20;
	private float GRACE = 25f;

	void Awake()
	{
		this.patron = patron;
		InvokeRepeating("RentPay", GRACE, 1f);
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
