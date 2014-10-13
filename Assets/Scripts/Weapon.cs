using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

//-----Structs and Enums----------------------------------------------------------------------------------------------------

	public enum WeaponType { None, Bullet, Missle, Bomb, Laser };

//-----Attribute Variables---------------------------------------------------------------------------------------------------

	public Transform muzzle;
	public Rigidbody2D bullet;

	private PlayerData playerData;
	private Movement movement;

	private float BULLET_VELOCITY = 15f;

//-----Unity Functions--------------------------------------------------------------------------------------------------------

	// Use this for initialization
	void Start ()
	{
		playerData = GetComponent<PlayerData>();
		movement = GetComponent<Movement>();
	}

	// Update is called once per frame
	void Update ()
	{
		if ( Input.GetButton("Fire1") )
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

//-----Custom Functions------------------------------------------------------------------------------------------------------

	private void FireBullet()
	{

		Rigidbody2D newBullet = Instantiate (bullet, muzzle.position, muzzle.rotation) as Rigidbody2D;
		if ( playerData.IsMovingRight() )
			newBullet.velocity = Vector2.right * BULLET_VELOCITY;
		else
			newBullet.velocity = -Vector2.right * BULLET_VELOCITY;
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
