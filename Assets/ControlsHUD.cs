using UnityEngine;
using System.Collections;

public class ControlsHUD : MonoBehaviour {

	public KeyBindings key;

	public GUIText controlsText;
	public GUITexture controlsBackground1;
	public GUITexture controlsBackground2;

	// Use this for initialization
	void Start () {
		setHUD ();
	}
	
	// Update is called once per frame
	void Update () {
		setHUD ();
	}

	void setHUD()
	{
		if (Input.GetButton (key.StartButton() ) )
		{
			controlsText.text = 
				"Controls: \n" +
				"Left Joystick to move\n" +
				"\"A\" Button to jump\n" +
				"\"B\" Button to \'Use\' \n" +
				"   (shops/chest)\n" +
				"\"X\" Button for Weapon\n" +
				"\"Y\" Button (hold) \n" +
				"   to see your money\n" +
				"Left Bumper to Dash Left\n" +
				"Right Bumper to Dash Right\n" +
				" -Watch how many dashes you have!\n\n" +
				"Player with most money when the game ends\n" +
				"  WINS!! So don't spend TOO much at shops,\n" +
				"  but keep yourself equipped to fight back.";

			controlsBackground1.guiTexture.pixelInset = new Rect (70f, -185f, 160f, 135f);

			controlsBackground2.enabled = true;
			controlsBackground2.guiTexture.pixelInset = new Rect (70f, -280f, 280f, 95f);

		}
		else 
		{
			controlsText.text = "Press \"Start\" for controls";
			controlsBackground1.guiTexture.pixelInset = new Rect (70f, -68f, 160f, 20f);
			controlsBackground2.enabled = false;
			controlsBackground2.guiTexture.pixelInset = new Rect (70f, -280f, 280f, 95f);
		}
	}
}
