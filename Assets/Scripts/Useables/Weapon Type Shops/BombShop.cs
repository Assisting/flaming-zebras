using UnityEngine;
using System.Collections;

public class BombShop : WeaponShop {

	public GUIText BombShopText;

	// Use this for initialization
	void Start () {
		weapon = Weapon.WeaponType.Bomb;
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
					BombShopText.gameObject.layer = LayerMask.NameToLayer( "Player1GUI" );
					break;
				}
				case 2 :
				{
					BombShopText.gameObject.layer = LayerMask.NameToLayer( "Player2GUI" );
					break;
				}
				case 3 :
				{
					BombShopText.gameObject.layer = LayerMask.NameToLayer( "Player3GUI" );
					break;
				}
				case 4 :
				{
					BombShopText.gameObject.layer = LayerMask.NameToLayer( "Player4GUI" );
					break;
				}
			}
			BombShopText.text = "1: Timer\n2: Proximity\n3: Cluster"; 
		}
	}
}
