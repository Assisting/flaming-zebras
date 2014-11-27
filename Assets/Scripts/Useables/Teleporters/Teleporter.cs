using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Teleporter : Usable{

	private static Dictionary<GameObject, Rent> rents;

	void Start()
	{
		if (rents != null)
			rents = new Dictionary<GameObject, Rent>(); //first teleporter in makes the dictionary
	}
	
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
