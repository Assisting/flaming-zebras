using UnityEngine;
using System.Collections;

//This code is from a top down view game

public class movement : MonoBehaviour {


	public float moveSpeed;
	private Vector2 move;
	private Animator anim;
	private float horiz;
	private float vert;

	// Use this for initialization
	void Start () {
		moveSpeed = 5.0f;
		move = new Vector2 (0.0f, 0.0f);
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate () {
		
		//Quickly builds up from 0.0 to 1.0 as the player holds a key down
		horiz = Input.GetAxis ("Horizontal");
		vert = Input.GetAxis ("Vertical");
		
		//Emulates a degree of acceleration well whilst not causing input to seem unresponsive.
		move.x = moveSpeed * horiz;
		move.y = moveSpeed * vert;
		
		rigidbody2D.velocity = move;
		
		rigidbody2D.velocity = move;
		anim.SetFloat("XSpeed", horiz);
		anim.SetFloat("YSpeed", vert);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
