using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	public Transform shop; //the location of the shop teleporter

	// Use this for initialization
	void Start () {
		shop = GameObject.Find("HomeTeleport").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
