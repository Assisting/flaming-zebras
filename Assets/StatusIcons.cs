using UnityEngine;
using System.Collections;

public class StatusIcons : MonoBehaviour {

	public PlayerData playerData;
	public Weapon weapon;
	public GUITexture fire;
	public GUITexture poison;
	public GUITexture invuln;


	// Use this for initialization
	void Start () {
		fire.gameObject.SetActive(false);
		poison.gameObject.SetActive(false);
		invuln.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (playerData.isBurning())
			fire.gameObject.SetActive(true);
		else 
			fire.gameObject.SetActive(false);

		if (playerData.isPoisoned())
			poison.gameObject.SetActive(true);
		else 
			poison.gameObject.SetActive(false);

		if (playerData.IsInvuln() || weapon.ShieldUp())
			invuln.gameObject.SetActive(true);
		else 
			invuln.gameObject.SetActive(false);
	}
}
