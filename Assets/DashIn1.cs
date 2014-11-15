using UnityEngine;
using System.Collections;

public class DashIn1 : MonoBehaviour {

	public PlayerData playerData;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (playerData.isDash1Available ())
						guiTexture.enabled = true;
				else
						guiTexture.enabled = false;
	}
}
