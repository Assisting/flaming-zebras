using UnityEngine;
using System.Collections;

public class wDoorScript : MonoBehaviour {

	private GameObject ohGodHackJob;
	public GameObject ohGodHackJob1;
	public GameObject ohGodHackJob2;
	private bool activated;
	
	// Use this for initialization
	void Start () {
		activated = false;
		float randNum = Random.value;
		if(0.51 < randNum){
			ohGodHackJob = ohGodHackJob1;
		} else {
			ohGodHackJob = ohGodHackJob2;
		}
	}
	
	void OnTriggerEnter2D() {
		if(!activated){
			Instantiate (ohGodHackJob, new Vector3(transform.position.x-3.2f, transform.position.y), transform.rotation);
			activated = true;
		}
	}
}