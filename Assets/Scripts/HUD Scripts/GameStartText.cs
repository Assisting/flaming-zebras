using UnityEngine;
using System.Collections;

public class GameStartText : MonoBehaviour {

	public GUIText text;
	public GUITexture background;
	public int timer; //time the message is up

	// Use this for initialization
	void Start () {
		timer = 20;
		text.text = 
			"You must choose a weapon!\n" +
			"But be fast, you have a limited time \n" +
			"in the shop before you start getting " +
			"charged rent!";
		Invoke ("removeText", timer);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void removeText()
	{
		text.enabled = false;
		background.enabled = false;
	}
}
