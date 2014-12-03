using UnityEngine;
using System.Collections;

public class Chest : Usable 
{

	protected int treasureAmount;
	private bool treasureTaken = false;
	private Animator anim;
	private GameObject opener = null;
	private float TREASURE_WAIT = 2f;

	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	public override void Use(GameObject caller)
	{
		if (opener == null && caller.tag == "Player" && !treasureTaken)
		{
			opener = caller;
			Invoke("GiveMoney", TREASURE_WAIT);
		}
	}

	private void GiveMoney()
	{
		if (opener != null)
		{
			opener.GetComponent<PlayerData>().ChangeMoney(treasureAmount);
			treasureTaken = true;
			anim.SetTrigger("Opened");
			audio.Play();
			Destroy (gameObject, .5f);
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject == opener)
		{
			opener = null;
		}
	}

}
