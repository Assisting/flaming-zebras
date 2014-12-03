using UnityEngine;
using System.Collections;

public class PatrolSpawn : MonoBehaviour {
	
	public Rigidbody2D patrol;

	private bool firstRun;
	
	float canSpawn;
	
	// Use this for initialization
	void Start () {
		firstRun = true;
		canSpawn = Time.time + 2f;
		//Invoke ("spawn", 1);
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void spawn() {
		Rigidbody2D newPatrol = Instantiate(patrol, this.transform.position, this.transform.rotation) as Rigidbody2D;

		newPatrol.transform.parent = this.transform;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if((firstRun == true) && (Time.time > canSpawn)) {
			if(other.gameObject.tag == "Player") {
				spawn ();
				firstRun = false;
			}
		}
	}
}