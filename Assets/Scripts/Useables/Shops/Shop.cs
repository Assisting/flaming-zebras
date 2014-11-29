using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Shop : Usable {

	static Dictionary<int, GUIText> shopGUIs; //one Gui list for all shops

	protected string DisplayData;

	protected int price;

	public virtual void Start()
	{
		if (null == shopGUIs) //first shop in makes the dictionary
		{
			GameObject[] shops = GameObject.FindGameObjectsWithTag("Shop"); //initialize price GUIs
			foreach (GameObject shop in shops)
			{
				shop.GetComponent<Shop>().GetGUIs(); //populate dictionary with the four available GUIs (index by playerNum)
			}
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == "Player")
		{

			int playerNum = other.GetComponent<PlayerData>().GetPlayerNum();
			GUIText guiStorage;
			string playersLayer = "Player" + playerNum + "GUI";
			
			shopGUIs.TryGetValue(playerNum, out guiStorage); //try to get player's shoptext
			if (guiStorage != null) //if player record exists
			{
				guiStorage.text = DisplayData + GetPrice(other.gameObject).ToString(); //set layer and text
				guiStorage.gameObject.layer = LayerMask.NameToLayer (playersLayer);
			}
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			int playerNum = other.GetComponent<PlayerData>().GetPlayerNum();
			GUIText guiStorage;
			shopGUIs.TryGetValue(playerNum, out guiStorage); //try to get player's shoptext
			if (guiStorage != null) //if player record exists
			{
				guiStorage.gameObject.layer = LayerMask.NameToLayer ("NoGUI"); //GUI disappears
			}
		}
	}

	public void GetGUIs()
	{
		shopGUIs = new Dictionary<int, GUIText>();
		GameObject[] GUIs = GameObject.FindGameObjectsWithTag("ShopTag"); //find all the shop texts
		foreach (GameObject GUI in GUIs)
		{
			int playerNum = GUI.transform.parent.parent.Find("Player").GetComponent<PlayerData>().GetPlayerNum(); //get related playernum
			shopGUIs.Add(playerNum, GUI.guiText); //store in the global dictionary
		}
	}

	public abstract int GetPrice(GameObject caller);
}
