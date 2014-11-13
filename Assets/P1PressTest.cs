using UnityEngine;
using System.Collections;

public class P1PressTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("P1PressTest"))
			Application.LoadLevel("procGen");
	}
}
