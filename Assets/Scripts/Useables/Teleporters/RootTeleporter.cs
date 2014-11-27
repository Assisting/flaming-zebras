using UnityEngine;
using System.Collections;

public class RootTeleporter : Usable {

	private float teleInvulnTime = 2f;

	public override void Use(GameObject caller)
	{
		PlayerData playerData = caller.GetComponent<PlayerData>();
		caller.transform.position = playerData.getLastTeleport();
		playerData.SetTeleportCooldown();
		caller.GetComponent<PlayerData>().MakeInvuln(teleInvulnTime);
	}
}
