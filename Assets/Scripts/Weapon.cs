using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public enum WeaponType { None, Bullet, Missle, Bomb, Laser };

	private PlayerData playerData;
	private Movement movement;

	// Use this for initialization
	void Start ()
	{
		playerData = GetComponent<PlayerData>();
		movement = GetComponent<Movement>();
	}



	// Update is called once per frame
	void Update ()
	{
		if ( Input.GetKey(KeyCode.Z) )
			FireWeapon();
	}



	private void FireWeapon()
	{
		switch (playerData.GetWeaponType())
		{
			case WeaponType.Bullet :
			{
				FireBullet();
				return;
			}

			case WeaponType.Missle :
			{
				FireMissle();
				return;
			}
				
			case WeaponType.Bomb :
			{
				FireBomb();
				return;
			}

			case WeaponType.Laser :
			{
				FireLaser();
				return;
			}
		}
	}

	private void FireBullet()
	{
		if ( playerData.IsMovingRight() )
		{
			
		}
	}

	private void FireMissle()
	{
		
	}
	
	private void FireBomb()
	{
		
	}

	private void FireLaser()
	{
		
	}
}
