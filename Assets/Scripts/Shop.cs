using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour
{
	//Note to self: Once Multiple players are implemented, change bluechar greenchar redchar.


	//--Variables------------------------------------------------------------------------------------------------------

	//These will let the shop know how sales work.
	private int ShopPrice; 
	private string ShopType;

	//These will let us reference the Player's stats.
	private GameObject PlayerGreen; 
	private GameObject PlayerBlue;
	private GameObject PlayerYellow;
	private GameObject PlayerRed;

	//These will be used to recgonize whether the players are in this shop.
	private bool IsPlayerGreenInShop;
	private bool IsPlayerBlueInShop;
	private bool IsPlayerYellowInShop;
	private bool IsPlayerRedInShop;
	



	// Use this for initialization
	void Start () 
	{
		//Sets inital settings for shop..
		IsPlayerGreenInShop = false;
		IsPlayerBlueInShop = false;
		IsPlayerYellowInShop = false;
		IsPlayerRedInShop = false;

		//Grabs player's information for reference later...
		PlayerGreen = GameObject.Find ("Player");
		PlayerBlue = GameObject.Find ("Blue_Char");
		PlayerYellow = GameObject.Find ("Yellow_Char");
		PlayerRed = GameObject.Find ("Red_Char");

		//Sets the shop type...
		if(this.gameObject.name == "MoveShop")
		{
			ShopPrice=150;//Arbritary value for now.
			ShopType="Move";
		}
		else if(this.gameObject.name == "DashShop")
		{
			ShopPrice=200;//Arbritary value for now.
			ShopType="Dash";
		}
		else if(this.gameObject.name == "JumpShop")
		{
			ShopPrice=500;//Arbritary value for now.
			ShopType="Jump";
		}
		else if(this.gameObject.name == "LaserShop")
		{
			ShopPrice=200;//Arbritary value for now.
			ShopType="Laser";
		}
		else if(this.gameObject.name == "MissileShop")
		{
			ShopPrice=250;//Arbritary value for now.
			ShopType="Missile";
		}
		else if(this.gameObject.name == "GunShop")
		{
			ShopPrice=100;//Arbritary value for now.
			ShopType="Gun";
		}
		else if(this.gameObject.name == "BombShop")
		{
			ShopPrice=100;//Arbritary value for now.
			ShopType="Bomb";
		}
		else if(this.gameObject.name == "BombShop")
		{
			ShopPrice=100;//Arbritary value for now.
			ShopType="Bomb";
		}
		else if(this.gameObject.name == "WUpgradeShop")
		{
			ShopPrice=400;//Arbritary value for now.
			ShopType="WUpgrade";
		}
		else
		{
			ShopPrice=0;
			ShopType="Derp";
		}

	}

	// FixedUpdate is called once per time unit
	void FixedUpdate ()
	{
		if (Input.GetKeyDown (KeyCode.B) && IsPlayerGreenInShop) 
		{
			BuyObject (PlayerGreen);
		}
		else if (Input.GetKeyDown (KeyCode.B) && IsPlayerBlueInShop) 
		{
			BuyObject (PlayerBlue);
		}
		else if (Input.GetKeyDown (KeyCode.B) && IsPlayerYellowInShop) 
		{
			BuyObject (PlayerYellow);
		}
		else if (Input.GetKeyDown (KeyCode.B) && IsPlayerRedInShop) 
		{
			BuyObject (PlayerRed);
		}
	}

	//Stuff that happens when player enters a shop.
	void OnTriggerEnter2D(Collider2D Player)
	{
		if(Player.gameObject.name == "Player")
		{
			IsPlayerGreenInShop = true;
		}
		else if(Player.gameObject.name == "Blue_Char")
		{
			IsPlayerBlueInShop = true;
		}
		else if(Player.gameObject.name == "Yellow_Char")
		{
			IsPlayerYellowInShop = true;
		}
		else if(Player.gameObject.name == "Red_Char")
		{
			IsPlayerRedInShop = true;
		}
	}

	void OnTriggerExit2D(Collider2D Player)
	{
		if(Player.gameObject.name == "Player")
		{
			IsPlayerGreenInShop = false;
		}
		else if(Player.gameObject.name == "Blue_Char")
		{
			IsPlayerBlueInShop = false;
		}
		else if(Player.gameObject.name == "Yellow_Char")
		{
			IsPlayerYellowInShop = false;
		}
		else if(Player.gameObject.name == "Red_Char")
		{
			IsPlayerRedInShop = false;
		}
	}
	
	// Checks to see if user has enough money to buy object, and if it does, subtracts the money from their
	// account and upgrades them appropriately.
	private void BuyObject(GameObject Player)
	{
		PlayerData MoneyInfo = Player.GetComponent<PlayerData> ();
		if ( ShopPrice <= MoneyInfo.GetMoney () ) 
		{
			if(ShopType=="Move")
			{
				MoneyInfo.ChangeMoney (-ShopPrice);
				MoneyInfo.LevelUp(PlayerData.Attribute.Move, MoneyInfo.GetMoveLevel() + 1);
				print ("Buy Successful! " + Player.name + " has $" + MoneyInfo.GetMoney() + " after buying " + ShopType);
			}
			else if(ShopType=="Dash")
			{
				MoneyInfo.ChangeMoney (-ShopPrice);
				MoneyInfo.LevelUp(PlayerData.Attribute.Dash, MoneyInfo.GetDashLevel() + 1);
				print ("Buy Successful! " + Player.name + " has $" + MoneyInfo.GetMoney() + " after buying " + ShopType);
			}
			else if(ShopType=="Jump")
			{
				MoneyInfo.ChangeMoney (-ShopPrice);
				MoneyInfo.LevelUp (PlayerData.Attribute.Jump, MoneyInfo.GetJumpLevel() + 1);
				print ("Buy Successful! " + Player.name + " has $" + MoneyInfo.GetMoney() + " after buying " + ShopType);
			}
			else if(ShopType=="WUpgrade")
			{
				MoneyInfo.ChangeMoney (-ShopPrice);
				MoneyInfo.LevelUp (PlayerData.Attribute.WeaponLevel, MoneyInfo.GetWeaponLevel() + 1);
				print ("Buy Successful! " + Player.name + " has $" + MoneyInfo.GetMoney() + " after buying " + ShopType);
			}
			else if(ShopType=="Laser")
			{
				MoneyInfo.ChangeMoney (-ShopPrice);
				MoneyInfo.LevelUp (PlayerData.Attribute.WeaponType, Weapon.WeaponType.Laser);
				print ("Buy Successful! " + Player.name + " has $" + MoneyInfo.GetMoney() + " after buying " + ShopType);
			}
			else if(ShopType=="Gun")
			{
				MoneyInfo.ChangeMoney (-ShopPrice);
				MoneyInfo.LevelUp (PlayerData.Attribute.WeaponType, Weapon.WeaponType.Bullet);
				print ("Buy Successful! " + Player.name + " has $" + MoneyInfo.GetMoney() + " after buying " + ShopType);
			}
			else if(ShopType=="Bomb")
			{
				MoneyInfo.ChangeMoney (-ShopPrice);
				MoneyInfo.LevelUp (PlayerData.Attribute.WeaponType, Weapon.WeaponType.Bomb);
				print ("Buy Successful! " + Player.name + " has $" + MoneyInfo.GetMoney() + " after buying " + ShopType);
			}
			else
			{
				print ("Error," + " Shop set to " + ShopType);
			}
			
		}
		else
		{
			print ( "Buy Unsuccessful, " + Player.name + " has $" + MoneyInfo.GetMoney () );
		}
	}


}
