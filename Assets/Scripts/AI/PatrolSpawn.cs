using UnityEngine;
using System.Collections;

public class PatrolSpawn : MonoBehaviour {
	
	public Rigidbody2D patrol;
	
	// Use this for initialization
	void Start () {
		//Invoke ("spawn", 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void spawn() {
		Rigidbody2D newPatrol = Instantiate(patrol, this.transform.position, this.transform.rotation) as Rigidbody2D;
		
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Player") {
			spawn ();
		}
	}
}