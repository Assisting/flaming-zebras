using UnityEngine;
using System.Collections;

public class HealthText : MonoBehaviour {

	public PlayerData playerData;
	public GUIText playerHealth;
	private int health;

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
		playerHealth.text = "Health: " + health.ToString ();
	}
}
