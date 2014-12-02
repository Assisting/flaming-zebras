using UnityEngine;
using System.Collections;

public class WeaponClassImage : MonoBehaviour {

	public PlayerData playerData;
	public Texture2D bomb;
	public Texture2D bullet;
	public Texture2D missile;
	public Texture2D sword;
	public Texture2D laser;
	public Texture2D none;
	public Texture2D chicken;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		switch (playerData.GetWeaponType())
		{
		case Weapon.WeaponType.Bomb:
		{
			guiTexture.texture = bomb;
			break;
		}
		case Weapon.WeaponType.Bullet:
		{
			guiTexture.texture = bullet;
			break;
		}
		case Weapon.WeaponType.Laser:
		{
			guiTexture.texture = laser;
			break;
		}
		case Weapon.WeaponType.Melee:
		{
			guiTexture.texture = sword;
			break;
		}
		case Weapon.WeaponType.RUBBER_CHICKEN:
		{
			guiTexture.texture = chicken;
			break;
		}
		case Weapon.WeaponType.Missile:
		{
			guiTexture.texture = missile;
			break;
		}
		case Weapon.WeaponType.None:
		{
			guiTexture.texture = none;
			break;
		}
		}
	}
}
