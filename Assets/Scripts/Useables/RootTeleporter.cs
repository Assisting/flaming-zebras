using UnityEngine;
using System.Collections;

public class RootTeleporter : Usable {

	public override void Use(GameObject caller)
	{
		caller.transform.position = caller.GetComponent<PlayerData>().getLastTeleport();
		//TODO invincible
	}
}
