using UnityEngine;
using System.Collections;

public class HomeTeleporter : Usable {

	public override void Use(GameObject caller)
	{
		caller.transform.position = caller.GetComponent<PlayerData>().getLastTeleport();
		//TODO invincible
	}
}
