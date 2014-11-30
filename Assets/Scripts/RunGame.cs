using UnityEngine;
using System.Collections;

public class RunGame : MonoBehaviour {

	private float ADVENTURETIME = 540f; //time till final fight starts
	private float GAMETIME = 300f; //game time in seconds

	// Use this for initialization
	void Start () {
		Invoke("LastShop", ADVENTURETIME)
		Invoke("EndGame", GAMETIME);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void LastShop()
	{
		GameObject[] players = FindGameObjectsWithTag("Player");
		foreach (GameObject player in players)
		{
			player.GetComponent<Movement>().TeleportHome(new Vector3(0f, 0f, 0f)); //everyone goes to the shop before the fight
		}
	}

	private void EndGame()
	{
		int highestMoney = 0;
		GameObject bestPlayer = null;
		GameObject[] players = FindGameObjectsWithTag("Player");
		foreach (GameObject player in players)
		{
			if (player.GetComponent<PlayerData>().GetMoney() > highestMoney) //find best player
				bestPlayer = player;
		}
		print("Congratulations " + bestPlayer.name + "! you won with a total of " + highestMoney + " gold."); //print ending message
		Time.timescale = 0f;//stop game
	}
}
