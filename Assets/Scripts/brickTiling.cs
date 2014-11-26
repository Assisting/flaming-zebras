using UnityEngine;
using System.Collections;

public class brickTiling : MonoBehaviour {

	// for tiling thick platforms
	void Start () {
		renderer.material.SetTextureScale("_MainTex", new Vector2(transform.lossyScale.x, transform.lossyScale.y));
	}
}
