using UnityEngine;
using System.Collections;

public class bulletHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		string tagHit = other.gameObject.tag;
		if (tagHit == "Wall")
			Destroy(gameObject);
	}
}
