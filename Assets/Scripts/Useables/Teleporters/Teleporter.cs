using UnityEngine;
using System.Collections;

public class Teleporter : Usable{
	
	public override void Use(GameObject caller)
	{
		Vector2 rootTeleport = GameObject.Find("RootTeleporter").transform.position;
		caller.GetComponent<PlayerData>().setLastTeleport(transform.position);
		caller.transform.position = rootTeleport;
	}
}
