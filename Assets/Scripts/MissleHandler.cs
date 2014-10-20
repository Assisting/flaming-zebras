using UnityEngine;
using System.Collections;

public class MissleHandler : MonoBehaviour {

	public LayerMask targetTypes;
	public Transform sensorPoint;

	private int EXPLOSION_DAMAGE = 23;
	private int IMPACT_DAMAGE = 12;
	private float MISSLE_SPEED = 12.5f;
	private float DETECTION_RADIUS = 2.5f;
	private float ROTATION_SPEED = 300f;
	private float EXPLOSION_RADIUS = 0.9f;

	private float armingWait; //so the missle doesn't kill the firer

	// Use this for initialization
	void Start () {
		armingWait = Time.time + 0.4f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//fly
		rigidbody2D.velocity = transform.right * MISSLE_SPEED;

		if (armingWait == -1f || armingWait <= Time.time)
		{
			armingWait = -1f;

			//rotate
			Collider2D closestTarget = Physics2D.OverlapCircle(sensorPoint.position, DETECTION_RADIUS, targetTypes);
			if (closestTarget != null)
			{
				Vector2 targetDirection = closestTarget.transform.position - transform.position;
				Quaternion targetRotation = Quaternion.FromToRotation(transform.right, targetDirection);
    				transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * ROTATION_SPEED);
			}

			//possibly explode
			Explode();
		}
	}

	public void Explode()
	{
		Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, EXPLOSION_RADIUS, targetTypes);
		if (targets.Length != 0)
		{
			for (int i = 0; i < targets.Length; i ++)
			{
				targets[i].gameObject.GetComponent<PlayerData>().LifeChange(-EXPLOSION_DAMAGE);
			}
			Destroy(gameObject);
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
