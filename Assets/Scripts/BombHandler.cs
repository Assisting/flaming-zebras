using UnityEngine;
using System.Collections;

public class BombHandler : MonoBehaviour {

	private int DAMAGE = 20;

	private float CLOSE_RANGE = 1f;
	private float LONG_RANGE = 2f;

	public LayerMask targetTypes;

	private float timer;

	// Use this for initialization
	void Start () {
		timer = Time.time + 3f;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer < Time.time)
		{
			Explode();
			Destroy(gameObject);
		}
	}

	public void Explode()
	{
		//applies damage twice to inner people, outer people take "half damage" as a result
		Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, CLOSE_RANGE, targetTypes);
		for (int i = 0; i < targets.Length; i ++)
		{
			targets[i].gameObject.GetComponent<PlayerData>().LifeChange(-DAMAGE);
		}
		targets = Physics2D.OverlapCircleAll(transform.position, LONG_RANGE, targetTypes);
		for (int i = 0; i < targets.Length; i ++)
		{
			targets[i].gameObject.GetComponent<PlayerData>().LifeChange(-DAMAGE);
		}
	}
}
