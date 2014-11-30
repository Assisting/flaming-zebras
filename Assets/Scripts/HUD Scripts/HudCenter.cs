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
	public StatusIcons stats;

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
		stats.playerData = player.GetComponent<PlayerData> ();

		// we want to make sure every element of the player's hud (except for shop, that's dealt with elsewhere)
		//   is rendering to the correct layer. 
		Transform[] allGUI = GetComponentsInChildren<Transform>(); // first get a collection of all the gui elements
		foreach (Transform gui in allGUI)
		{
			int playerNum = player.GetComponent<PlayerData>().GetPlayerNum(); //get the player info to set it to
			string playersLayer = "Player" + playerNum + "GUI";
			if(gui.name == "shop") // because, as we mentioned, this gets dealt with elsewhere
			{
				gui.gameObject.layer = LayerMask.NameToLayer (playersLayer); // and do the setting of the thing. 
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
