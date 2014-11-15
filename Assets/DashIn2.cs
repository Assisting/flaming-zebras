using UnityEngine;
using System.Collections;

public class DashIn2 : MonoBehaviour {

	public PlayerData playerData;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (playerData.isDash2Available ())
			guiTexture.enabled = true;
		else
			guiTexture.enabled = false;
	}
}
