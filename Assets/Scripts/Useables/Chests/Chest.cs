using UnityEngine;
using System.Collections;

public class Chest : Usable 
{

	protected int treasureAmount;
	private bool treasureTaken = false;

	public override void Use(GameObject caller)
	{
		if (caller.tag == "Player" && !treasureTaken)
		{
			caller.GetComponent<PlayerData>().ChangeMoney(treasureAmount);
			treasureTaken = true;
			Destroy (gameObject, .5f);
		}
	}

}
