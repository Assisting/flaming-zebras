using UnityEngine;
using System.Collections;

public class BombHandler : MonoBehaviour {

	private float MAX_DAMAGE;
	private float MINOR_DAMAGE;

	private float CLOSE_RANGE = 1f;
	private float LONG_RANGE = 2f;

	public LayerMask targetTypes;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Explode()
	{
		Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, CLOSE_RANGE, targetTypes);
		for (int i = 0; i < targets.Length; i ++)
		{
			
		}
	}
}
