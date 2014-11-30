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
		playerData.MakeInvuln(Mathf.Infinity);
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
			playerData.MakeInvuln(RootTeleporter.teleInvulnTime);
			if (playerData.GetWeaponType() == Weapon.WeaponType.None)
				playerData.LevelUp(PlayerData.Attribute.WeaponType, Weapon.WeaponType.RUBBER_CHICKEN);
			StopPay();
		}
	}

	public void StopPay()
	{
		CancelInvoke("RentPay");
		Destroy(this);
	}
}
