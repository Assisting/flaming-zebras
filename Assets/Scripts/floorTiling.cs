using UnityEngine;
using System.Collections;

public class floorTiling : MonoBehaviour {

	// Use this for initialization
	void Start () {
		renderer.material.SetTextureScale("_MainTex", new Vector2(transform.lossyScale.x, 1f));
	}
}
