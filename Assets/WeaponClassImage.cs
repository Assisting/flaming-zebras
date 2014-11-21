using UnityEngine;
using System.Collections;

public class WeaponClassImage : MonoBehaviour {

	public PlayerData playerData;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		switch (playerData.GetWeaponType())
			case Weapon.WeaponType.Bomb;
	}
}
