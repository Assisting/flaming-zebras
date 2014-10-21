using UnityEngine;
using System.Collections;

public class BombHandler : Explosive {

	public Transform bomb;

	private float timer;

	// Use this for initialization
	void Start () {
		timer = Time.time + 3f;
		LOW_DAMAGE = 15;
		HIGH_DAMAGE = 30; //really 45
		CLOSE_RANGE = 1f;
		LONG_RANGE = 2f;
	}
	
	// Update is called once per frame
	void Update () {
		switch (level)
		{
			case 1 : // time bomb
			{
				if (timer < Time.time)
				{
					Explode();
				}
				break;
			}
			case 2 : // proximity bomb (still explodes on timer)
			{
				Collider2D target = Physics2D.OverlapCircle(transform.position, CLOSE_RANGE, targetTypes);
				if (target != null)
					Explode();
				goto case 1;
			}
			case 3 : // timed explosion and bomb spawn
			{
				if (timer < Time.time)
				{
					Transform newBomb1 = Instantiate(bomb, transform.position + new Vector3(0.5f, 0.5f, 0f), transform.rotation) as Transform;
					newBomb1.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f); //half size
					newBomb1.GetComponent<BombHandler>().SetLevel(1);
					
					Transform newBomb2 = Instantiate(bomb, transform.position + new Vector3(0f, 0.7f, 0f), transform.rotation) as Transform;
					newBomb2.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f); //half size
					newBomb2.GetComponent<BombHandler>().SetLevel(1);
					
					Transform newBomb3 = Instantiate(bomb, transform.position + new Vector3(-0.5f, 0.5f, 0f), transform.rotation) as Transform;
					newBomb3.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f); //half size
					newBomb3.GetComponent<BombHandler>().SetLevel(1);
					
					Explode();
				}
				break;
			}
		}
	}
}
