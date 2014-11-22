using UnityEngine;
using System.Collections;

public class StatShop : Shop {

	protected PlayerData.Attribute stat;

	protected int weapLevel;

	public override void Use(GameObject caller)
	{
		if (caller.tag == "Player") //only acknowledge players
		{
			PlayerData playerData = caller.GetComponent<PlayerData>();
			int statLevel = playerData.GetAttributeLevel(stat);

			if (playerData.GetMoney() >= price && statLevel < 3)
			{
				playerData.ChangeMoney(-(price));
				playerData.LevelUp(stat, statLevel + 1);
			}
		}
	}
}
