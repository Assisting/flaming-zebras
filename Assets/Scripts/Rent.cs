using UnityEngine;
using System.Collections;

public class Rent : MonoBehaviour {

	private PlayerData playerData;

	public static int RENT_COST = 10;
	public static float GRACE = 20f;
	public static float TICKTIME = 1f;

	void Start()
	{
		playerData = GetComponent<PlayerData>();
		InvokeRepeating("RentPay", GRACE, TICKTIME);
	}

	private void RentPay()
	{
		if (playerData.GetMoney() >= RENT_COST)
			playerData.ChangeMoney(-RENT_COST);
		else
		{
			transform.position = new Vector3(0f, 0f, 0f);
			playerData.SetTeleportCooldown();
			StopPay();
		}
	}

	public void StopPay()
	{
		CancelInvoke("RentPay");
		Destroy(this);
	}
}
