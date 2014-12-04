using UnityEngine;
using System.Collections;

public class Chest : Usable 
{
	public AudioClip open;

	protected int treasureAmount;
	private bool treasureTaken = false;
	private Animator anim;
	private GameObject opener = null;
	private float TREASURE_WAIT = 2f;
	protected float treasureCooldown;

	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	public override void Use(GameObject caller)
	{
		if (opener == null && caller.tag == "Player" && !treasureTaken)
		{
			opener = caller;
			audio.PlayOneShot(open);
			Invoke("GiveMoney", TREASURE_WAIT);
		}
	}

	private void GiveMoney()
	{
		if (opener != null)
		{
			opener.GetComponent<PlayerData>().ChangeMoney(treasureAmount);
			treasureTaken = true;
			anim.SetBool("Opened", true);
			audio.Play();
			//Destroy (gameObject, .5f);
			Opened ();
		}
	}

	private void Opened()
	{
		gameObject.renderer.enabled = false;
		gameObject.collider2D.enabled = false;
		Invoke ("Refill", treasureCooldown);
	}

	private void Refill()
	{
		treasureTaken = false;
		anim.SetBool("Opened", false);
		gameObject.renderer.enabled = true;
		gameObject.collider2D.enabled = true;
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject == opener)
		{
			opener = null;
		}
	}

}
