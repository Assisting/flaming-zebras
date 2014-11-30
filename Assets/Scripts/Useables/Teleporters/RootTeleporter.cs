using UnityEngine;
using System.Collections;

public class RootTeleporter : Usable {

	public static float teleInvulnTime = 2f;

	public override void Use(GameObject caller)
	{
		PlayerData playerData = caller.GetComponent<PlayerData>();
		if (playerData.GetWeaponType() != Weapon.WeaponType.None)
		{
			caller.transform.position = playerData.getLastTeleport();
			playerData.SetTeleportCooldown();
			caller.GetComponent<PlayerData>().MakeInvuln(teleInvulnTime);
			caller.GetComponent<Rent>().StopPay();
		}	
	}
}
