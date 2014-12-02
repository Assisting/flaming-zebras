using UnityEngine;
using System.Collections;

public class ChargerSpawn : MonoBehaviour {
	
	public Rigidbody2D charger;
	
	public float startTime;
	
	// Use this for initialization
	void Start () {
		startTime = Time.time + 1f;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > startTime) {
			
			Rigidbody2D newCharger = Instantiate(charger, this.transform.position, this.transform.rotation) as Rigidbody2D;
			
			Destroy(gameObject);
		}
		
		
	}
}