using UnityEngine;
using System.Collections;

public class Camera1Shift : MonoBehaviour {

	public Transform Player;
	public PlayerData playerData;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp (transform.position, Player.position, .1f);
		transform.position = new Vector3 (transform.position.x, transform.position.y, -10);

		if (playerData.GetPlayerNum() == 1)
		{
			Camera.main.rect = new Rect(0f, .5f, .498f, .5f);
		}
		else if (playerData.GetPlayerNum() == 2)
		{
			Camera.main.rect = new Rect(0.502f, .5f, 1f, 1f);
		}
		else if (playerData.GetPlayerNum() == 3)
		{
			Camera.main.rect = new Rect(-0.002f, 0f, .5f, .498f);
		}
		else if (playerData.GetPlayerNum() == 4)
		{
			Camera.main.rect = new Rect(0.502f, 0f, .5f, .498f);
		}
	}
}
