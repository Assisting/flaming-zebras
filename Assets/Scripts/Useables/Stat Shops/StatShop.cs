using UnityEngine;
using System.Collections;

public class StatShop : Usable {

	protected PlayerData.Attribute stat;

	public override void Use(GameObject caller)
	{
		if (caller.tag == "player") //only acknowledge players
		{
			PlayerData playerData = caller.GetComponent<PlayerData>();
			int statLevel = playerData.GetAttributeLevel(stat);
			if (statLevel < 3)
				playerData.LevelUp(stat, statLevel + 1);
		}
	}
}
