using UnityEngine;
using System.Collections;

public class TChest : MonoBehaviour 
{

	private int treasureAmount;
	private bool treasureTaken=false;

	// Use this for initialization
	void Start () 
	{
		treasureAmount = 50;//arbitrary amount for now.
	}

	void OnTriggerEnter2D(Collider2D player)
	{
		if (player.gameObject.tag == "Player" && !treasureTaken)
		{
			PlayerData temp= player.GetComponent<PlayerData>();
			treasureTaken=true;
			temp.ChangeMoney(treasureAmount);
			Destroy (gameObject, .5f);
		}
	}

}
