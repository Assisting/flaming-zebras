using UnityEngine;
using System.Collections;

public class DashIn1 : MonoBehaviour {

	public PlayerData playerData;
	public int numDashes;
	public GUITexture Dash1;
	public GUITexture Dash2;

	// Use this for initialization
	void Start () {
		setNumDashes ();
	}
	
	// Update is called once per frame
	void Update () {
		setNumDashes ();
		if (playerData.numDashesAvailable () == 0)
		{
			Dash1.guiTexture.enabled = false;
			Dash2.guiTexture.enabled = false;
		}
		else if (playerData.numDashesAvailable() == 1)
		{
			Dash1.guiTexture.enabled = false;
			Dash2.guiTexture.enabled = true;
		}
		else
		{
			Dash1.guiTexture.enabled = true;
			Dash2.guiTexture.enabled = true;
		}
	}

	void setNumDashes()
	{
		numDashes = playerData.numDashesAvailable ();
	}
}
