using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour 
{

	private int MoneyAmount; // The amount of money a player has

	// Use this for initialization
	void Start () 
	{
		MoneyAmount = 500;
	}

	// allows shops to see the amount of money a player has.
	public int GetMoney()
	{
		return this.MoneyAmount;
	}
	
	// subtracts or adds the amount of money specified to the player's moneyAmount.
	public void ChangeMoney(int change)
	{
		MoneyAmount = MoneyAmount + change;
	}
}
