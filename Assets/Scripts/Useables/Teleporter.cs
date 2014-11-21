using UnityEngine;
using System.Collections;

public class Teleporter : Usable{

	private Vector2 rootTeleport; //the location of the shop teleporter

	// Use this for initialization
	void Start () {
		rootTeleport = GameObject.Find("RootTeleporter").transform.position;
	}
	
	public override void Use(GameObject caller)
	{
		caller.GetComponent<PlayerData>().setLastTeleport(transform.position);
		caller.transform.position = rootTeleport;
	}
}
