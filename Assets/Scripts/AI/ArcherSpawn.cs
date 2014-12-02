using UnityEngine;
using System.Collections;

public class ArcherSpawn : MonoBehaviour {

	public Rigidbody2D archer;

	// Use this for initialization
	void Start () {
		//Invoke ("spawn", 1);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void spawn() {
		Rigidbody2D newArcher = Instantiate(archer, this.transform.position, this.transform.rotation) as Rigidbody2D;
			
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D other) {
		spawn ();
	}
}
