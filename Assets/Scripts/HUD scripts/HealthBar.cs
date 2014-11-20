using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public PlayerData playerData;
	private int curHealth;
	private int maxHealth;
	
	// Use this for initialization
	void Start () {
		setHealth();
	}
	
	// Update is called once per frame
	void Update () {
		setHealth();
	}
	
	private void setHealth()
	{
		curHealth = playerData.getHealth();
		maxHealth = playerData.getMaxHealth();
		float lifeBar = ((float)curHealth / (float)maxHealth) * 100f;
		if (lifeBar > 100f)
						lifeBar = 100f;
		guiTexture.pixelInset = new Rect (10f, -17f, lifeBar, 10);
	}
}