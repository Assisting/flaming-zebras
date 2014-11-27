using UnityEngine;
using System.Collections;

public class RootTeleporter : Usable {

	private float teleInvulnTime = 2f;

	public override void Use(GameObject caller)
	{
		caller.transform.position = caller.GetComponent<PlayerData>().getLastTeleport();
		caller.GetComponent<PlayerData>().MakeInvuln(teleInvulnTime);
	}

	void onTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			PlayerData script = other.GetComponent<PlayerData>();
			script.CURLIFE = script.MAXLIFE; //full heal
		}
	}
}
