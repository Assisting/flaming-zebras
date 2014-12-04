using UnityEngine;
using System.Collections;

public class PoisonGas : MonoBehaviour {

	private int GAS_DAMAGE = 4;
	private bool flash = false;
	private int flashTimer = 5;
	private bool visible = true;

	public bool fadePoison = false;


	void Awake (){
		if (fadePoison) {
			Invoke ("Transition", 10.0f);		
		}
	}

	void OnTriggerStay2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			PlayerData playerScript = other.GetComponent<PlayerData>();
			playerScript.Poison(GAS_DAMAGE, 1.0f, 2.0f);
		}
	}

	/*void OnTriggerExit2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			PlayerData playerScript = other.GetComponent<PlayerData>();
			playerScript.LeavePoison(2.0f);
		}
	}*/


	private void Transition()
	{
		flash = true;
		gameObject.collider2D.enabled = false;
		Invoke ("SwitchVisible", 1.0f);
	}

	private void SwitchVisible()
	{
		flash = false;
		visible = !visible;
		//flash = visible;
		gameObject.renderer.enabled = visible;
		gameObject.collider2D.enabled = visible;
		Invoke ("Transition", 5.0f);
		/*flashTimer = 5;
		if (!visible) 
		{
			visible = true;
			//gameObject.GetComponent<Animator>().enabled = visible;
			gameObject.GetComponent<SpriteRenderer>().enabled = visible;		
		}*/

	}

	void FixedUpdate () 
	{
		if(flash)
		{
			if(0 == flashTimer)
			{
				//Flip the boolean to enable/disable accordingly
				gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled;
				//Flash every 5th physics frame
				flashTimer = 5;
			} else
			{
				flashTimer--;
			}
		}
	}

}