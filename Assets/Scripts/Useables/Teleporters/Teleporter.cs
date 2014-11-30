using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Teleporter : Usable{
	
	public override void Use(GameObject caller)
	{
		PlayerData playerData = caller.GetComponent<PlayerData>();
		Movement movement = caller.GetComponent<Movement>();

		if (playerData.CanTeleport())
		{
			playerData.SetTeleportCooldown();
			movement.TeleportHome(transform.position);
		}
	}
}
