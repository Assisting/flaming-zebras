using UnityEngine;
using System.Collections;

public class LevelGenerate : MonoBehaviour {

	private float HORIZONTAL_SEPARATION;
	private float VERTICAL_SEPARATION;

	void Awake()
	{
		Application.LoadLevelAdditive("nameHere");
		GameObject[] caveLevelsFindGameObjectsWithTag("CaveLevel"); 
	}
}
