using UnityEngine;
using System.Collections;

public class DeadlyFloor : MonoBehaviour {

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "Projectile") //projectiles are all that move that can't be hurt
		{
			Actor script = other.GetComponent<Actor>();
			script.LifeChange(-script.MAXLIFE);
	}
}
