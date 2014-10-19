using UnityEngine;
using System.Collections;

public class MissleHandler : MonoBehaviour {

	public LayerMask targetTypes;

	private int DAMAGE = 23;
	private float MISSLE_SPEED = 9f;
	private float DETECTION_RADIUS = 2f;
	private float ROTATION_SPEED = 12f;
	private float EXPLOSION_RADIUS =1.2f;

	private float armingWait; //so the missle doesn't kill the firer

	// Use this for initialization
	void Start () {
		armingWait = Time.time + 0.6f;
	}
	
	// Update is called once per frame
	void Update () {

		//fly
		rigidbody2D.velocity = transform.right * MISSLE_SPEED;

		if (armingWait == -1f || armingWait <= Time.time)
		{
			armingWait = -1f;

			//rotate
			Collider2D closestTarget = Physics2D.OverlapCircle(transform.position, DETECTION_RADIUS, targetTypes);
			if (closestTarget != null)
			{
				Vector2 vectorToTarget = transform.position - closestTarget.transform.position;
				float angle = -Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, angle), ROTATION_SPEED);
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
				targets[i].gameObject.GetComponent<PlayerData>().LifeChange(-DAMAGE);
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
				other.GetComponent<PlayerData>().LifeChange(-DAMAGE);
			}
			Destroy(gameObject);
		}
	}
}
