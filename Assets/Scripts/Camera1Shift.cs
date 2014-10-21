using UnityEngine;
using System.Collections;

public class Camera1Shift : MonoBehaviour {

	public Transform Player;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp (transform.position, Player.position, .1f);
		transform.position = new Vector3 (transform.position.x, transform.position.y, -10);
	}
}
