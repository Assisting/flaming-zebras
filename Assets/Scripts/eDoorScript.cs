using UnityEngine;
using System.Collections;

public class eDoorScript : MonoBehaviour {

	//The room to be spawned
	private GameObject ohGodHackJob;
	/*For this PoC, we only have 2 possible rooms, and they are set in the unity UI.
	*Later we intend to grab a set of each type of room (determined by presence/lack of doors in each of
	*the 4 directions on the xy axis) from Resources, and keep them in static
	*arrays for these scripts to pull from
	*/
	public GameObject ohGodHackJob1;
	public GameObject ohGodHackJob2;
	//Whether or not a room has already been spawned at this door
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
			//Offset is based on the size of the room, makes sure it spawns such that there is no gap or overlap.
			Instantiate (ohGodHackJob, new Vector3(transform.position.x+3.2f, transform.position.y), transform.rotation);
			activated = true;
		}
	}
}