using UnityEngine;
using System.Collections;

public class LevelGenerate : MonoBehaviour {

	void Awake()
	{
		for (int i = 0; i < 2; i++)
		{
			Application.LoadLevelAdditive("TeleRoom");
		}
	}
}
