﻿using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour 
{
	private int ShopPrice; // The amount of money the player will pay to get the shop's item

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
		IsPlayerGreenInShop = false;
		IsPlayerBlueInShop = false;
		IsPlayerYellowInShop = false;
		IsPlayerRedInShop = false;
		ShopPrice = 100; //Arbritary value for now.
		PlayerGreen = GameObject.Find ("Green_Char");
		PlayerBlue = GameObject.Find ("Blue_Char");
		PlayerYellow = GameObject.Find ("Yellow_Char");
		PlayerRed = GameObject.Find ("Red_Char");
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
		if(Player.gameObject.name=="Green_Char")
		{
			IsPlayerGreenInShop = true;
		}
		else if(Player.gameObject.name=="Blue_Char")
		{
			IsPlayerBlueInShop = true;
		}
		else if(Player.gameObject.name=="Yellow_Char")
		{
			IsPlayerYellowInShop = true;
		}
		else if(Player.gameObject.name=="Red_Char")
		{
			IsPlayerRedInShop = true;
		}
	}

	void OnTriggerExit2D(Collider2D Player)
	{
		if(Player.gameObject.name=="Green_Char")
		{
			IsPlayerGreenInShop = false;
		}
		else if(Player.gameObject.name=="Blue_Char")
		{
			IsPlayerBlueInShop = false;
		}
		else if(Player.gameObject.name=="Yellow_Char")
		{
			IsPlayerYellowInShop = false;
		}
		else if(Player.gameObject.name=="Red_Char")
		{
			IsPlayerRedInShop = false;
		}
	}
	
	// Checks to see if user has enough money to buy object, and if it does, subtracts the money from their
	// account and upgrades them appropriately.
	private void BuyObject(GameObject Player)
	{
		PlayerData MoneyInfo = Player.GetComponent<PlayerData> ();
		if (ShopPrice <= MoneyInfo.GetMoney ()) 
		{
			MoneyInfo.ChangeMoney (-ShopPrice);
			print ("Buy Successful! "+Player.name+" has $"+MoneyInfo.GetMoney());
			//Insert effect here.
		}
		else
		{
			print ("Buy Insuccessful, "+Player.name+" has $"+MoneyInfo.GetMoney () );
		}
	}


}