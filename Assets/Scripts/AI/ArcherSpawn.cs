using UnityEngine;
using System.Collections;

public class ArcherSpawn : MonoBehaviour {

	public Rigidbody2D archer;

	public float startTime;

	// Use this for initialization
	void Start () {
		startTime = Time.time + 1f;


	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > startTime) {
			
			Rigidbody2D newArcher = Instantiate(archer, this.transform.position, this.transform.rotation) as Rigidbody2D;

			Destroy(gameObject);
		}


	}
}
