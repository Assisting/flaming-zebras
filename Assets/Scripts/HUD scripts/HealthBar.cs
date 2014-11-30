using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public PlayerData playerData;
	public Texture2D poisonBar;
	public Texture2D burnBar;
	public Texture2D invulnBar;
	public Texture2D HealthyBar;
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

	void SetColor()
	{
		if (playerData.isPoisoned())
		{

		}
	}
}






