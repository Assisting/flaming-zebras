﻿using UnityEngine;
using System.Collections;

public class Shop : Usable {

	public GUIText ShopTextP1;
	public GUIText ShopTextP2;
	public GUIText ShopTextP3;
	public GUIText ShopTextP4;

	public GUITexture backgroundP1;
	public GUITexture backgroundP2;
	public GUITexture backgroundP3;
	public GUITexture backgroundP4;

	protected string DisplayData;

	public int price;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Use(GameObject caller){
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			int playerNum = other.GetComponent<PlayerData>().GetPlayerNum();
			switch (playerNum)
			{
			case 1 :
			{
				ShopTextP1.gameObject.layer = LayerMask.NameToLayer( "Player1GUI" );
				backgroundP1.gameObject.layer = LayerMask.NameToLayer( "Player1GUI" );
				ShopTextP1.text = DisplayData;
				break;
			}
			case 2 :
			{
				ShopTextP2.gameObject.layer = LayerMask.NameToLayer( "Player2GUI" );
				backgroundP2.gameObject.layer = LayerMask.NameToLayer( "Player2GUI" );
				ShopTextP2.text = DisplayData;
				break;
			}
			case 3 :
			{
				ShopTextP3.gameObject.layer = LayerMask.NameToLayer( "Player3GUI" );
				backgroundP3.gameObject.layer = LayerMask.NameToLayer( "Player3GUI" );
				ShopTextP3.text = DisplayData;
				break;
			}
			case 4 :
			{
				ShopTextP4.gameObject.layer = LayerMask.NameToLayer( "Player4GUI" );
				backgroundP4.gameObject.layer = LayerMask.NameToLayer( "Player4GUI" );
				ShopTextP4.text = DisplayData;
				break;
			}
			}
			
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			int playerNum = other.GetComponent<PlayerData>().GetPlayerNum();
			switch (playerNum)
			{
			case 1 :
			{
				ShopTextP1.gameObject.layer = LayerMask.NameToLayer ("NoGUI");
				backgroundP1.gameObject.layer = LayerMask.NameToLayer( "NoGUI" );
				break;
			}
			case 2 :
			{
				ShopTextP2.gameObject.layer = LayerMask.NameToLayer ("NoGUI");
				backgroundP2.gameObject.layer = LayerMask.NameToLayer( "NoGUI" );
				break;
			}
			case 3 :
			{
				ShopTextP3.gameObject.layer = LayerMask.NameToLayer ("NoGUI");
				backgroundP3.gameObject.layer = LayerMask.NameToLayer( "NoGUI" );
				break;
			}
			case 4 :
			{
				ShopTextP4.gameObject.layer = LayerMask.NameToLayer ("NoGUI");
				backgroundP4.gameObject.layer = LayerMask.NameToLayer( "NoGUI" );
				break;
			}
			}
		}
	}
}
