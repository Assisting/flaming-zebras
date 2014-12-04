using UnityEngine;
using System.Collections;

public class MissleHandler : Explosive {

	public Transform sensorPoint;
	public Rigidbody2D missile;

	private int IMPACT_DAMAGE = 5;
	private float MISSLE_SPEED = 12.5f;
	private float DETECTION_RADIUS = 2.5f;
	private float ROTATION_SPEED = 300f;

	bool exploding = false;

	// Use this for initialization
	void Start () {
		Invoke("Arm", 0.05f);
		LOW_DAMAGE = 8;
		HIGH_DAMAGE = 15; //really 23
		CLOSE_RANGE = 0.9f;
		LONG_RANGE = 1.3f;
		Destroy(gameObject, 10);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//fly
		if (!exploding)
			rigidbody2D.velocity = transform.right * MISSLE_SPEED;

		//armed functions
		if (armed)
		{
			if (level > 1) // possibly do special things
			{
				Collider2D[] closestTargets = Physics2D.OverlapCircleAll(sensorPoint.position, DETECTION_RADIUS, targetTypes);
				Collider2D closestTarget = null;
				for (int i = 0; closestTargets[i].tag != "Player"; i++)
				{
					closestTarget = closestTargets[i];
				}
				if (closestTarget != null && ORIGIN != closestTarget.gameObject)
				{
					if (level > 2) //level 3 (swarm)
					{
						MissleHandler script;
					
						Rigidbody2D newMissile1 = Instantiate(missile, transform.position, transform.rotation) as Rigidbody2D;
						newMissile1.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f); //half size
						script = newMissile1.GetComponent<MissleHandler>();
						script.Origin(ORIGIN);
						script.SetLevel(2);
						newMissile1.transform.Rotate(new Vector3(0f, 0f, -15f)); //counterclockwise
						
						Rigidbody2D newMissile2 = Instantiate(missile, transform.position, transform.rotation) as Rigidbody2D; //straight
						newMissile2.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
						script = newMissile2.GetComponent<MissleHandler>();
						script.Origin(ORIGIN);
						script.SetLevel(2);
						
						Rigidbody2D newMissile3 = Instantiate(missile, transform.position, transform.rotation) as Rigidbody2D;
						newMissile3.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
						script = newMissile3.GetComponent<MissleHandler>();
						script.Origin(ORIGIN);
						script.SetLevel(2);
						newMissile3.transform.Rotate(new Vector3(0f, 0f, 15f)); //clockwise

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
		}
	}

	// For missiles that hit walls, or something else before being armed
	public void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player" || other.tag == "Enemy" || other.tag == "Platform")
		{
			if (armed)
			{
				exploding = true;
				rigidbody2D.velocity = Vector2.zero;
				Explode();
			}
			else if (other.tag != "Platform")
			{
				other.GetComponent<PlayerData>().LifeChange(-IMPACT_DAMAGE);
				Destroy(gameObject);
			}
		}
	}
}
