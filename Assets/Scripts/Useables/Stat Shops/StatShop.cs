using UnityEngine;
using System.Collections;

public class StatShop : Usable {

	protected PlayerData.Attribute stat;
	public GUIText ShopText;
	protected string DisplayData;

	public override void Use(GameObject caller)
	{
		if (caller.tag == "Player") //only acknowledge players
		{
			PlayerData playerData = caller.GetComponent<PlayerData>();
			int statLevel = playerData.GetAttributeLevel(stat);
			if (statLevel < 3)
				playerData.LevelUp(stat, statLevel + 1);
		}
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			int playerNum = other.GetComponent<PlayerData>().GetPlayerNum();
			switch (playerNum)
			{
			case 1 :
			{
				ShopText.gameObject.layer = LayerMask.NameToLayer( "Player1GUI" );
				break;
			}
			case 2 :
			{
				ShopText.gameObject.layer = LayerMask.NameToLayer( "Player2GUI" );
				break;
			}
			case 3 :
			{
				ShopText.gameObject.layer = LayerMask.NameToLayer( "Player3GUI" );
				break;
			}
			case 4 :
			{
				ShopText.gameObject.layer = LayerMask.NameToLayer( "Player4GUI" );
				break;
			}
			}
			ShopText.text = DisplayData;
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		ShopText.gameObject.layer = LayerMask.NameToLayer ("NoGUI");
	}
}
