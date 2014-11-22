using UnityEngine;
using System.Collections;

public class HealthText : MonoBehaviour {

	public PlayerData playerData;
	public GUIText playerHealth;
	private int health;
	private int maxHealth;

	// Use this for initialization
	void Start () {
		setHealthText ();
	}
	
	// Update is called once per frame
	void Update () {
		setHealthText ();
	}

	private void setHealthText()
	{
		health = playerData.getHealth();
		maxHealth = playerData.getMaxHealth ();
		playerHealth.text = "Health: " + health.ToString () + "/" + maxHealth.ToString();
	}
}
