using UnityEngine;
using System.Collections;

public class RunGame : MonoBehaviour {

	public GUITexture winner;

	private int FINALFIGHT = 90; //length of the final fight in seconds
	private int GAMETIME = 500; //game time in seconds
	private int secondsPassed = 0;

	private int bestPlayerNum = -1;

	// Use this for initialization
	void Start () {
		InvokeRepeating("GameTick", 1, 1); //game time goes up every second
	}

	private void GameTick()
	{
		secondsPassed++;
		if (SecondsRemaining() == FINALFIGHT)
			LastShop();
		if (SecondsRemaining() == 0)
			EndGame();
	}

	public int SecondsRemaining()
	{
		return GAMETIME - secondsPassed;
	}

	private void LastShop()
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players)
		{
			player.GetComponent<Movement>().TeleportHome(new Vector3(0f, 0f, 0f)); //everyone goes to the shop before the fight
		}
	}

	private void EndGame()
	{
		int highestMoney = 0;
		GameObject bestPlayer = null;
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players)
		{
			int thisMoney = player.GetComponent<PlayerData>().GetMoney();
			if ( thisMoney > highestMoney) //find best player
			{
				bestPlayer = player;
				highestMoney = thisMoney;
			}	
		}
		bestPlayerNum = bestPlayer.GetComponent<PlayerData>().GetPlayerNum();
		string playersLayer = "Player" + bestPlayerNum + "GUI";
		winner.gameObject.layer = LayerMask.NameToLayer (playersLayer);
		print("Congratulations " + bestPlayer.name + "! you won with a total of " + highestMoney + " gold."); //print ending message
		Time.timeScale = 0f;//stop game
	}

	public int GetBestPlayer()
	{
		return bestPlayerNum;
	}
}
