using UnityEngine;
using System.Collections;

public class BombHandler : Explosive {

	public Transform bomb;

	private float timer;

	// Use this for initialization
	void Start () {
		armingWait = Time.time + 0.8f;
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
				if (armingWait <= Time.time)
				{
					ExplodeCheck();
				}
				goto case 1;
			}
			case 3 : // timed explosion and bomb spawn
			{
				if (timer < Time.time)
				{
					BombHandler script;

					Transform newBomb1 = Instantiate(bomb, transform.position + new Vector3(-0.5f, 0.43f, 0f), transform.rotation) as Transform;
					newBomb1.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f); //half size
					script = newBomb1.GetComponent<BombHandler>();
					script.Origin(ORIGIN);
					script.SetLevel(1);
					
					Transform newBomb2 = Instantiate(bomb, transform.position + new Vector3(0.5f, 0.43f, 0f), transform.rotation) as Transform;
					newBomb2.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f); //half size
					script = newBomb2.GetComponent<BombHandler>();
					script.Origin(ORIGIN);
					script.SetLevel(1);
					
					Transform newBomb3 = Instantiate(bomb, transform.position + new Vector3(0f, -0.43f, 0f), transform.rotation) as Transform;
					newBomb3.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f); //half size
					script = newBomb3.GetComponent<BombHandler>();
					script.Origin(ORIGIN);
					script.SetLevel(1);
					
					Explode();
				}
				break;
			}
		}
	}
}
