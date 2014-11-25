using UnityEngine;
using System.Collections;

public class LevelGenerate : MonoBehaviour {

	void Awake()
	{
		Application.LoadLevelAdditive("nameHere");
		GameObject.Find("sameName");
	}
}
