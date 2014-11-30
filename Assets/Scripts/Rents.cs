using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rents : MonoBehaviour {

	public static Dictionary<int, Rent> rents = new Dictionary<int, Rent>();

	public static void AddRent(GameObject player)
	{
		Rent newRent = new Rent(player);
		rents.Add(player.GetComponent<PlayerData>().GetPlayerNum(), newRent); //make new Rent record
	}

	public static void RemoveRent(GameObject player)
	{
		Rent rent;
		int playerNum = player.GetComponent<PlayerData>().GetPlayerNum();
		rents.TryGetValue(playerNum, out rent); //get rent record
		rent.StopPay(); //stop payment
		rents.Remove(playerNum); //remove record
	}
}
