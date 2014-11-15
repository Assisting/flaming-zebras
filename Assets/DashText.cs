using UnityEngine;
using System.Collections;

public class DashText : MonoBehaviour {

	public PlayerData playerData;
	public GUIText playerDashes;
	//private int dashes;

	// Use this for initialization
	void Start () {
		setNumDashesText();
	}
	
	// Update is called once per frame
	void Update () {
		setNumDashesText();
	}

	void setNumDashesText()
	{
		//dashes = playerData.numDashesAvailable ();
		playerDashes.text = "Dashes: ";
	}
}
