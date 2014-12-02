using UnityEngine;
using System.Collections;

public class Rent : MonoBehaviour {

	private PlayerData playerData;

	public static int RENT_COST = 10;
	public static float GRACE = 20f; // time in seconds before it starts taking your money
	public static float TICKTIME = 1f;

	void Start()
	{
		playerData = GetComponent<PlayerData>();
		playerData.MakeInvuln(Mathf.Infinity); //invuln while in shop
		InvokeRepeating("RentPay", GRACE, TICKTIME);
	}

	private void RentPay()
	{
		if (playerData.GetMoney() >= RENT_COST) //if we have enough to pay rent
			playerData.ChangeMoney(-RENT_COST); //pay
		else
			KickOut();
	}

	public void StopPay()
	{
		CancelInvoke("RentPay");
		Destroy(this);
	}

	private void KickOut()
	{
		transform.position = new Vector3(0f, 0f, 0f); //move out of shop
		playerData.SetTeleportCooldown(); //can't come back for a while
		playerData.MakeInvuln(RootTeleporter.teleInvulnTime); //normal leaving invuln
		transform.parent.Find("GUIText").Find("Shop").gameObject.layer = LayerMask.NameToLayer ("NoGUI"); //turn off the shopGUI, just in case
		if (playerData.GetWeaponType() == Weapon.WeaponType.None) //if they didn't pick a weapon
			playerData.LevelUp(PlayerData.Attribute.WeaponType, Weapon.WeaponType.RUBBER_CHICKEN); //give them the secret one
		StopPay(); //stop the rent payment
	}
}
