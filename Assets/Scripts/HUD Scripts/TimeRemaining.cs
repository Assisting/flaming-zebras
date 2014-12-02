using UnityEngine;
using System.Collections;

public class TimeRemaining : MonoBehaviour {

	private GameObject gameThing;
	private RunGame game;
	public GUIText text;

	// Use this for initialization
	void Awake () {
		gameThing = GameObject.FindGameObjectWithTag ("Game");
	}

	void Start ()
	{
		game = gameThing.GetComponent<RunGame>();
		updateTime ();
	}
	
	// Update is called once per frame
	void Update () {
		updateTime ();
	}

	void updateTime()
	{
		int seconds = game.SecondsRemaining() % 60;
		int minutes = game.SecondsRemaining() / 60;
		text.text = "Time Remaining: " + minutes + ':' + seconds;
		if(seconds < 10)
			text.text = "Time Remaining: " + minutes + ":0" + seconds;
	}
}
