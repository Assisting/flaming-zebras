using UnityEngine;
using System.Collections;

public class ChargerSpawn : MonoBehaviour {
	
	public Rigidbody2D charger;
	
	// Use this for initialization
	void Start () {
		//Invoke ("spawn", 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void spawn() {
		Rigidbody2D newCharger = Instantiate(charger, this.transform.position, this.transform.rotation) as Rigidbody2D;
		
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D other) {
		spawn ();
	}
}