using UnityEngine;
using System.Collections;

public class PatrolSpawn : MonoBehaviour {
	
	public Rigidbody2D patrol;
	
	public float startTime;
	
	// Use this for initialization
	void Start () {
		startTime = Time.time + 1f;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > startTime) {
			
			Rigidbody2D newPatrol = Instantiate(patrol, this.transform.position, this.transform.rotation) as Rigidbody2D;
			
			Destroy(gameObject);
		}
		
		
	}
}