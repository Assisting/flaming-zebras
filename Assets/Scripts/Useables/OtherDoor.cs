using UnityEngine;
using System.Collections;

public class OtherDoor : Door {

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
