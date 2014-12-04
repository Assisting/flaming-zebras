using UnityEngine;
using System.Collections;

public class Door : Usable {

	//this class controls the portals between level tiles

	public Transform pairExit;
	public Transform thisExit;

	public override void Use(GameObject caller)
	{
		if (caller.tag == "Player")
		{
			if(caller.GetComponent<PlayerData>().GetWeaponType() != Weapon.WeaponType.None)
			{
				caller.GetComponent<Movement>().StopDash();
				caller.transform.position = pairExit.position;
			}
			
		}
	}
}
