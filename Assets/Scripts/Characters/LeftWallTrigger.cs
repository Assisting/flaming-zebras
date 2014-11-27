using UnityEngine;
using System.Collections;

public class LeftWallTrigger : MonoBehaviour {

	private PlayerData playerData;
	private Movement movement;

	void Awake()
	{
		playerData = transform.parent.GetComponent<PlayerData>();
		movement = transform.parent.GetComponent<Movement>();
	}

	//stop player from moving or dashing towards a wall they are touching
	void OnTriggerStay2D (Collider2D other)
	{
		if (other.tag == "Platform" || other.tag == "Player" || other.tag == "Enemy")
		{
			movement.wallLeft = true;
			if ( playerData.IsDashing() )
				movement.StopDash();
		}
		if (other.tag == "Ramp")
		{
			if ( playerData.IsDashing() )
				movement.StopDash();
		}	
	}

	//allow movement and dashing agin on exiting wall collision
	void OnTriggerExit2D (Collider2D other)
	{
		if (other.tag == "Platform" || other.tag == "Player" || other.tag == "Enemy")
			movement.wallLeft = false;
	}
}
