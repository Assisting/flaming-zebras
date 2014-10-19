using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {

	public int LIFE = 0;
	protected bool MOVING_RIGHT;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (rigidbody2D.velocity.x > 0f)
			MOVING_RIGHT = true;
		else if (rigidbody2D.velocity.x < 0f)
			MOVING_RIGHT = false;
	}

	// Alter life total by the given amount
	public void LifeChange(int value)
	{
		LIFE += value;
	}

	public void Burn(int damage, float duration)
	{
		
	}

	public bool IsMovingRight()
	{
		return MOVING_RIGHT;
	}
}
