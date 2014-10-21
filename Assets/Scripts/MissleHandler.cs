using UnityEngine;
using System.Collections;

public class MissleHandler : Explosive {

	public Transform sensorPoint;
	public Rigidbody2D missile;

	private int IMPACT_DAMAGE = 5;
	private float MISSLE_SPEED = 12.5f;
	private float DETECTION_RADIUS = 2.5f;
	private float ROTATION_SPEED = 300f;

	private float armingWait; //so the missle doesn't immediately try to kill the firer

	// Use this for initialization
	void Start () {
		armingWait = Time.time + 0.4f;
		LOW_DAMAGE = 8;
		HIGH_DAMAGE = 15; //really 23
		CLOSE_RANGE = 0.9f;
		LONG_RANGE = 1.3f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//fly
		rigidbody2D.velocity = transform.right * MISSLE_SPEED;

		//armed functions
		if (armingWait == -1f || armingWait <= Time.time)
		{
			armingWait = -1f; // make armed (in case it wasn't)

			if (level > 1) // possibly do special things
			{
				Collider2D closestTarget = Physics2D.OverlapCircle(sensorPoint.position, DETECTION_RADIUS, targetTypes);
				if (closestTarget != null)
				{
					if (level > 2) //level 3 (swarm)
					{
						Rigidbody2D newMissile1 = Instantiate(missile, transform.position, transform.rotation) as Rigidbody2D;
						newMissile1.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f); //half size
						newMissile1.GetComponent<MissleHandler>().SetLevel(2);
						newMissile1.transform.rotation = Quaternion.Euler(0f, 0f, -20f); //counterclockwise
						
						Rigidbody2D newMissile2 = Instantiate(missile, transform.position, transform.rotation) as Rigidbody2D; //straight
						newMissile2.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
						newMissile2.GetComponent<MissleHandler>().SetLevel(2);
						
						Rigidbody2D newMissile3 = Instantiate(missile, transform.position, transform.rotation) as Rigidbody2D;
						newMissile3.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
						newMissile3.GetComponent<MissleHandler>().SetLevel(2);
						newMissile3.transform.rotation = Quaternion.Euler(0f, 0f, 20f); //clockwise

						Destroy(gameObject); //this level missle doesn't explode
					}
					else //level 2 (tracking)
					{
						Vector2 targetDirection = closestTarget.transform.position - transform.position;
						Quaternion targetRotation = Quaternion.FromToRotation(transform.right, targetDirection);
		    				transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * ROTATION_SPEED);
					}
				}
			}

			//possibly explode
			Collider2D target = Physics2D.OverlapCircle(transform.position, CLOSE_RANGE, targetTypes);
			if (target != null)
				Explode();
		}
	}

	public void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player" || other.tag == "Enemy" || other.tag == "Wall")
		{
			if (armingWait == -1f)
				Explode();
			else if (other.tag != "Wall")
			{
				other.GetComponent<PlayerData>().LifeChange(-IMPACT_DAMAGE);
			}
			Destroy(gameObject);
		}
	}
}
