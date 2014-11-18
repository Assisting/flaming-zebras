﻿using UnityEngine;
using System.Collections;

public class MoneyText : MonoBehaviour {

	public PlayerData playerData;
	public GUIText playerMoney;
	private int money;
	
	// Use this for initialization
	void Start () {
		setMoneyText ();
	}
	
	// Update is called once per frame
	void Update () {
		setMoneyText ();
	}
	
	private void setMoneyText()
	{
		money = playerData.getMoney();
		playerMoney.text = "Money: " + money.ToString ();
	}
}