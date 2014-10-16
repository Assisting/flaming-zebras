using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

//-----Structs and Enums----------------------------------------------------------------------------------------------------

	public enum WeaponType { None, Bullet, Missle, Bomb, Laser, Melee };

	public static float[] bulletReload = new float[3] { 0.4f, 1.8f, 0.8f };
	public static int[] bulletClip = new int[3] { 1, 5, 8};

//-----Attribute Variables---------------------------------------------------------------------------------------------------

	public Transform muzzle;
	public Rigidbody2D bullet;

	private PlayerData playerData;

	private float BULLET_VELOCITY = 15f; // speed of bullets in-game

	private float RELOAD_WAIT = 0f; // time to wait in between clip regeneration
	private float reloadTimer; // timestamp for clip regeneration
	private int MAX_BULLETS;
	private int BULLETS_FIRED = 0;

//-----Unity Functions--------------------------------------------------------------------------------------------------------

	void Awake ()
	{
		playerData = GetComponent<PlayerData>();
	}

	// Use this for initialization
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{
		if (reloadTimer <= Time.time)
			BULLETS_FIRED = 0;

		if (Input.GetButton("Fire1"))
			FireWeapon();
	}

//-----Custom Functions------------------------------------------------------------------------------------------------------

	public void UpdateLevel()
	{			
		int wepLevel = playerData.GetWeaponLevel();
	
		switch ( playerData.GetWeaponType() )
		{
			case WeaponType.Bullet :
			{
				RELOAD_WAIT = bulletReload[wepLevel - 1];
				MAX_BULLETS = bulletClip[wepLevel - 1];
				return;
			}

			case WeaponType.Missle :
			{
				
				return;
			}
				
			case WeaponType.Bomb :
			{
				
				return;
			}

			case WeaponType.Laser :
			{
				
				return;
			}

			case WeaponType.Melee :
			{
				
				return;
			}
		}
	}

	private void FireWeapon()
	{
		if (BULLETS_FIRED >= MAX_BULLETS)
			return;
			
		BULLETS_FIRED ++;
		reloadTimer = Time.time + RELOAD_WAIT;
		
		switch ( playerData.GetWeaponType() )
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

			case WeaponType.Melee :
			{
				SwingMelee();
				return;
			}
		}
	}

	private void FireBullet()
	{
		float rotateDegrees = 0f;
		bool shotgun = 3 == playerData.GetWeaponLevel();
	
		Rigidbody2D newBullet = Instantiate (bullet, muzzle.position, muzzle.rotation) as Rigidbody2D;
		if (shotgun)
		{
			rotateDegrees = Random.Range(-10f, 10f);
			newBullet.transform.rotation = Quaternion.Euler(0f, 0f, rotateDegrees);
		}

		if ( playerData.IsMovingRight() )
			newBullet.velocity = newBullet.transform.right * BULLET_VELOCITY;
		else
			newBullet.velocity = -newBullet.transform.right * BULLET_VELOCITY;

		if (shotgun)
			FireWeapon();
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

	private void SwingMelee()
	{
		
	}
}
