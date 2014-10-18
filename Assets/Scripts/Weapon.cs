using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

//-----Structs and Enums----------------------------------------------------------------------------------------------------

	public enum WeaponType { None, Bullet, Missile, Bomb, Laser, Melee };

	public static float[] bulletReloads = new float[3] { 0.4f, 1.8f, 0.8f };
	public static int[] bulletClips = new int[3] { 1, 5, 8};

	public static float[] laserFades = new float[3] { 0.1f, 0.4f, 0.3f};

//-----Attribute Variables---------------------------------------------------------------------------------------------------

	public Transform leftMuzzle;
	public Transform rightMuzzle;
	public LayerMask projectileTargets;
	public Rigidbody2D bullet;
	public LineRenderer laser;

	private PlayerData playerData;
	private Transform rightMelee;
	private Transform leftMelee;
	private Transform muzzle;

	private float BULLET_VELOCITY = 15f; // speed of bullets in-game
	//private float BULLET_DAMAGE = 12f; //damage per bullet

	private float LASER_FADE; //time for laser to disappear
	//private float LASER_DAMAGE = 26f; //damage per laser burst

	private float RELOAD_WAIT = 0f; // time to wait in between clip regeneration
	private float reloadTimer; // timestamp for clip regeneration
	private int MAX_BULLETS; //leveled maximum of shots between reload-waits
	private int BULLETS_FIRED = 0;

//-----Unity Functions--------------------------------------------------------------------------------------------------------

	void Awake ()
	{
		playerData = GetComponent<PlayerData>();
		rightSword = transform.Find("RightSwordBox");
		leftSword = transform.Find("LeftSwordBox");
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
				RELOAD_WAIT = bulletReloads[wepLevel - 1];
				MAX_BULLETS = bulletClips[wepLevel - 1];
				return;
			}

			case WeaponType.Missile :
			{
				
				return;
			}
				
			case WeaponType.Bomb :
			{
				
				return;
			}

			case WeaponType.Laser :
			{
				RELOAD_WAIT = 2.1f;
				MAX_BULLETS = 1;
				LASER_FADE = laserFades[wepLevel-1];
				return;
			}

			case WeaponType.Melee :
			{
				RELOAD_WAIT = 0.6f;
				MAX_BULLETS = 1;
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

		if ( playerData.IsMovingRight() )
			muzzle = rightMuzzle;
		else
			muzzle = leftMuzzle;
		
		switch ( playerData.GetWeaponType() )
		{
			case WeaponType.Bullet :
			{
				FireBullet();
				return;
			}

			case WeaponType.Missile :
			{
				FireMissile();
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

	private void FireMissile()
	{
		
	}
	
	private void FireBomb()
	{
		
	}

	private void FireLaser()
	{
		LineRenderer newLaser = Instantiate(laser) as LineRenderer;
		Destroy(newLaser.gameObject, LASER_FADE);
		newLaser.SetPosition(0, muzzle.position);
		RaycastHit2D[] hitPoints;
		
		if ( playerData.IsMovingRight() )
			hitPoints = Physics2D.RaycastAll(muzzle.position, Vector2.right, projectileTargets);
		else
			hitPoints = Physics2D.RaycastAll(muzzle.position, -Vector2.right, projectileTargets);

		int i = 0;
		switch ( playerData.GetWeaponLevel() )
		{
			case 1 :
			{
				break;
			}
			case 2 :
			{
				break;
			}
			case 3 :
			{
				while (hitPoints[i].transform.tag != "Wall")
				{
					//TODO damage/burn players
					i ++;
				}
				break;
			}
		}

		if ( playerData.IsMovingRight() ) //set laser to appropriate endpoint
			newLaser.SetPosition( 1, new Vector3(muzzle.position.x + hitPoints[i].distance, muzzle.position.y, muzzle.position.z) );
		else
			newLaser.SetPosition( 1, new Vector3(muzzle.position.x - hitPoints[i].distance, muzzle.position.y, muzzle.position.z) );
			
	}

	private void SwingMelee()
	{
		if ( playerData.IsMovingRight() )
			rightSword.collider2D.enabled = true;
		else
			leftSword.collider2D.enabled = true;
	}
}
