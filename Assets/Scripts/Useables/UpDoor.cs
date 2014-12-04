using UnityEngine;
using System.Collections;

public class UpDoor : Door {

	public override void Use(GameObject caller){}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			if(other.GetComponent<PlayerData>().GetWeaponType() != Weapon.WeaponType.None)
			{
				other.GetComponent<Movement>().StopDash();
				other.transform.position = pairExit.position;
			}
			
		}
	}
}
