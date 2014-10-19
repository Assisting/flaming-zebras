using UnityEngine;
using System.Collections;

public class CameraSelect : MonoBehaviour {

	public float maxX;
	public float maxY;
	public float minX;
	public float minY;

	public GameObject camera1;
	public GameObject camera2;

	public Transform player;

	// Use this for initialization
	void Start () {
		maxX = 50f;
		maxY = 50f;
		minX = -7f;
		minY = -50f;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.position.x < minX)
		{
			camera1.active = false;
			camera2.active = true;
		}
		else 
		{
			camera1.active = true;
			camera2.active = false;
		}
	}
}
