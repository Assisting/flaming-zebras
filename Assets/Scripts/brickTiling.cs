using UnityEngine;
using System.Collections;

public class brickTiling : MonoBehaviour {

	// Use this for initialization
	void Start () {
		renderer.material.SetTextureScale("_MainTex", new Vector2(transform.lossyScale.x, transform.lossyScale.y));
	}
}
