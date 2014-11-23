using UnityEngine;
using System.Collections;

public class StatShop : Shop {

	protected PlayerData.Attribute stat;
	protected float scalingFactor = 2.5f;

	void Start()
	{
		price = 100;
	}

	public override void Use(GameObject caller)
	{
		if (caller.tag == "Player") //only acknowledge players
		{
			PlayerData playerData = caller.GetComponent<PlayerData>();
			int statLevel = playerData.GetAttributeLevel(stat);

			if (playerData.GetMoney() >= price && statLevel < 3)
			{
				playerData.LevelUp(stat, statLevel + 1);
				playerData.ChangeMoney((int)-(price* Mathf.Pow(scalingFactor,statLevel-1)));
			}
		}
	}
}
