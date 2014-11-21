using UnityEngine;
using System.Collections;

public class Teleporter : Usable{

	public Vector2 homeTeleport; //the location of the shop teleporter

	// Use this for initialization
	void Start () {
		homeTeleport = GameObject.Find("HomeTeleport").transform.position;
	}
	
	public override void Use(GameObject caller)
	{
		caller.GetComponent<PlayerData>().setLastTeleport(transform.position);
		caller.transform.position = homeTeleport;
	}
}
