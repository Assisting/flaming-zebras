using UnityEngine;
using System.Collections;

public class Chest : Usable 
{

	protected int treasureAmount;
	private bool treasureTaken = false;
	private Animator anim;

	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	public override void Use(GameObject caller)
	{
		if (caller.tag == "Player" && !treasureTaken)
		{
			caller.GetComponent<PlayerData>().ChangeMoney(treasureAmount);
			treasureTaken = true;
			anim.SetTrigger("Opened");
			audio.Play();
			Destroy (gameObject, .5f);
		}
	}

}
