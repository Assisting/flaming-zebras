using UnityEngine;
using System.Collections;

public class BoulderBreak : MonoBehaviour {

	void Awake()
	{
		Destroy(gameObject, 1.5f);
	}

	void Break()
	{
		Destroy(gameObject);
	}
}
