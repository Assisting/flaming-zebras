﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Shop : Usable {

	private Dictionary<int, GUIText> shopGUIs;

	protected string DisplayData;

	protected int price;

	public virtual void Start()
	{
		GameObject[] shops = GameObject.FindGameObjectsWithTag("Shop"); //initialize price GUIs
		foreach (GameObject shop in shops)
		{
			shop.GetComponent<Shop>().GetGUIs();
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == "Player")
		{

			int playerNum = other.GetComponent<PlayerData>().GetPlayerNum();
			GUIText guiStorage;
			string playersLayer = "Player" + playerNum + "GUI";
			
			shopGUIs.TryGetValue(playerNum, out guiStorage);
			if (guiStorage != null)
			{
				guiStorage.text = DisplayData + GetPrice(other.gameObject).ToString();
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
			shopGUIs.TryGetValue(playerNum, out guiStorage);
			if (guiStorage != null)
			{
				guiStorage.gameObject.layer = LayerMask.NameToLayer ("NoGUI");
			}
		}
	}

	public void GetGUIs()
	{
		shopGUIs = new Dictionary<int, GUIText>();
		GameObject[] GUIs = GameObject.FindGameObjectsWithTag("ShopTag");
		foreach (GameObject GUI in GUIs)
		{
			int playerNum = GUI.transform.parent.parent.Find("Player").GetComponent<PlayerData>().GetPlayerNum();
			shopGUIs.Add(playerNum, GUI.guiText);
		}
	}

	public abstract int GetPrice(GameObject caller);
}
