using UnityEngine;
using System.Collections;

public class HudCenter : MonoBehaviour {

	public PlayerData player;
	public KeyBindings key;
	public DashIn1 dashes;
	public MoneyText moneyText;
	public HealthText healthText;
	public LevelsText levels;
	public HealthBar healthBar;
	public WeaponClassImage weaponClass;
	public ControlsHUD start;

	// Use this for initialization
	void Awake() {
		dashes.playerData = player.GetComponent<PlayerData>();
		moneyText.playerData = player.GetComponent<PlayerData>();
		moneyText.keybind = key.GetComponent<KeyBindings>();
		healthText.playerData = player.GetComponent<PlayerData> ();
		levels.playerData = player.GetComponent<PlayerData> ();
		healthBar.playerData = player.GetComponent<PlayerData> ();
		weaponClass.playerData = player.GetComponent<PlayerData> ();
		start.key = key.GetComponent<KeyBindings>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
