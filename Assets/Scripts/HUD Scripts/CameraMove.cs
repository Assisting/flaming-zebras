using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	public LayerMask wallLayer;
	public Transform player;
	private Vector3 topLeft;
	private Vector3 bottomRight;
	private Vector3 horizontalRadius;
	private Vector3 verticalRadius;

	private bool limitRight;
	private bool limitLeft;
	private bool limitUp;
	private bool limitDown;

	void Start()
	{
		horizontalRadius = new Vector2(1f, 0f); //fill the 1 in with distance from center to right edge
		verticalRadius = new Vector2(0f, 1f); //fill in 1 with the distance from center to top edge
		topLeft = verticalRadius - horizontalRadius;
		bottomRight = horizontalRadius - verticalRadius;
	}

	void Update()
	{
		limitDown = false;
		limitLeft = false;
		limitRight = false;
		limitUp = false;
		
		Collider2D[] hits = Physics2D.OverlapAreaAll( (transform.position + topLeft), (transform.position + bottomRight), wallLayer );
		for (int i = 0; i < hits.Length; i ++)
		{
			if (hits[i].tag == "Wall")
			{

				if (hits[i].transform.rotation.eulerAngles.z != 0) //time saving
				{
					RaycastHit2D rightCheck = Physics2D.Raycast(transform.position + horizontalRadius, Vector2.right, wallLayer);
					if (rightCheck.collider == hits[i])
					{
						limitRight = true;
						continue;
					}
					RaycastHit2D leftCheck = Physics2D.Raycast(transform.position - horizontalRadius, -Vector2.right, wallLayer);
					if (leftCheck.collider == hits[i])
					{
						limitLeft = true;
						continue;
					}
				}
				else
				{
					RaycastHit2D upCheck = Physics2D.Raycast(transform.position + verticalRadius, Vector2.up, wallLayer);
					if (upCheck.collider == hits[i])
					{
						limitUp = true;
						continue;
					}
					RaycastHit2D downCheck = Physics2D.Raycast(transform.position - horizontalRadius, -Vector2.up, wallLayer);
					if (downCheck.collider == hits[i])
					{
						limitDown = true;
					}
				}
				
			}
		}

		Vector3 targetDirection = player.position - transform.position;
		if ( (limitDown && targetDirection.y < 0f) || (limitUp && targetDirection.y > 0f) )
			targetDirection.y = 0f;
		if ( (limitLeft && targetDirection.x < 0f) || (limitRight && targetDirection.x > 0f) )
			targetDirection.x = 0f;
		transform.position += targetDirection;
		
	}
}
