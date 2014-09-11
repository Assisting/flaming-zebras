using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour 
{
	private int ShopPrice; // The amount of money the player will pay to get the shop's item
	private GameObject Player1; //This will let us reference Player 1's stats.
	private bool IsPlayer1InShop;



	// Use this for initialization
	void Start () 
	{
		IsPlayer1InShop = false;
		ShopPrice = 100; //Arbritary value for now.
		Player1=GameObject.Find ("Green_Char");
	}

	// FixedUpdate is called once per time unit
	void FixedUpdate ()
	{
		if (Input.GetKeyDown (KeyCode.B) && IsPlayer1InShop) 
		{
			BuyObject (Player1);
		}
	}

	//Stuff that happens when player enters a shop.
	void OnTriggerEnter2D(Collider2D other)
	{
		IsPlayer1InShop = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		IsPlayer1InShop = false;
	}
	
	// Checks to see if user has enough money to buy object, and if it does, subtracts the money from their
	// account and upgrades them appropriately.
	private void BuyObject(GameObject Player)
	{
		PlayerData MoneyInfo = Player.GetComponent<PlayerData> ();
		if (ShopPrice <= MoneyInfo.GetMoney ()) 
		{
			MoneyInfo.ChangeMoney (-ShopPrice);
			print ("Buy Successful! You have $"+MoneyInfo.GetMoney());
			//Insert effect here.
		}
		else
		{
			print ("Buy Insuccessful, you have $"+MoneyInfo.GetMoney () );
		}
	}


}
