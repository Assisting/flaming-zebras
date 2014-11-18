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
	public Transform rightMelee;
	public Transform leftMelee;
	public LayerMask projectileTargets;
	public Rigidbody2D bullet;
	public LineRenderer laser;
	public Transform bomb;
	public Rigidbody2D missle;

	private PlayerData playerData;
	private KeyBindings keyBind;
	private Transform muzzle;

	private float LASER_FADE; //time for laser to disappear
	private int LASER_DAMAGE = 26; //damage per laser burst
	private int LASER_BURN_DAMAGE = 2;
	private float LASER_BURN_TIME = 4f;

	private int MELEE_DAMAGE = 60;
	
	private float RELOAD_WAIT = 0f; // time to wait in between clip regeneration
	private int MAX_BULLETS; //leveled maximum of shots between reload-waits
	private int BULLETS_FIRED = 0;

//-----Unity Functions--------------------------------------------------------------------------------------------------------

	void Awake ()
	{
		playerData = GetComponent<PlayerData>();
		keyBind = GetComponent<KeyBindings>();
		rightMelee = transform.Find("RightSwordBox");
		leftMelee = transform.Find("LeftSwordBox");
	}

	// Use this for initialization
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{
		if ( Input.GetButton( keyBind.AttackButton() ) && !playerData.isStunned() )
			FireWeapon();
	}

//-----Custom Functions------------------------------------------------------------------------------------------------------

	public void UpdateWeapon()
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
				RELOAD_WAIT = 2.4f;
				MAX_BULLETS = 1;
				return;
			}
				
			case WeaponType.Bomb :
			{
				RELOAD_WAIT = 1f;
				MAX_BULLETS = 1;
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
		CancelInvoke("Reload");
		Invoke("Reload", RELOAD_WAIT);

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

	private void Reload()
	{
		BULLETS_FIRED = 0;
	}

	private void FireBullet()
	{
		float rotateDegrees = 0f;
		bool shotgun = 3 == playerData.GetWeaponLevel();
	
		Rigidbody2D newBullet = Instantiate (bullet, muzzle.position, muzzle.rotation) as Rigidbody2D;
		newBullet.GetComponent<BulletHandler>().Origin(gameObject);
		if (shotgun) //spray bullets
		{
			rotateDegrees = Random.Range(-10f, 10f);
			newBullet.transform.Rotate( new Vector3(0f, 0f, rotateDegrees) );
			FireWeapon(); //shoot whole "clip" (shell)
		}			
	}

	private void FireMissile()
	{
		Rigidbody2D newMissile = Instantiate(missle, muzzle.position, muzzle.rotation) as Rigidbody2D;
		MissleHandler script = newMissile.GetComponent<MissleHandler>();
		script.Origin(gameObject);
		script.SetLevel( playerData.GetWeaponLevel() );
	}
	
	private void FireBomb()
	{
		Transform newBomb = Instantiate(bomb, muzzle.position, muzzle.rotation) as Transform;
		BombHandler script = newBomb.GetComponent<BombHandler>();
		script.Origin(gameObject);
		script.SetLevel( playerData.GetWeaponLevel() );
	}

	private void FireLaser()
	{
		int laserLevel = playerData.GetWeaponLevel();

		//draw new laser
		LineRenderer newLaser = Instantiate(laser) as LineRenderer;
		Destroy(newLaser.gameObject, LASER_FADE);
		newLaser.SetPosition(0, muzzle.position);
		RaycastHit2D[] hitTargets;
		
		if ( playerData.IsMovingRight() ) //find targets
			hitTargets = Physics2D.RaycastAll(muzzle.position, Vector2.right, projectileTargets);
		else
			hitTargets = Physics2D.RaycastAll(muzzle.position, -Vector2.right, projectileTargets);

		int i = 0;
		while (hitTargets[i].transform.tag != "Wall")
		{
			Actor currentTarget = hitTargets[i].transform.GetComponent<Actor>();
			currentTarget.StunDamage(LASER_DAMAGE, playerData.IsMovingRight());
			if (laserLevel > 1)
				currentTarget.Burn(LASER_BURN_DAMAGE, 0.5f, LASER_BURN_TIME);
			if (laserLevel < 3)
				break; //always stop at first object for level 1-2 lasers
			i ++;
		}

		// set laser endpoint
		if ( playerData.IsMovingRight() ) //set laser to appropriate endpoint
			newLaser.SetPosition( 1, new Vector3(muzzle.position.x + hitTargets[i].distance, muzzle.position.y, muzzle.position.z) );
		else
			newLaser.SetPosition( 1, new Vector3(muzzle.position.x - hitTargets[i].distance, muzzle.position.y, muzzle.position.z) );
			
	}

	private void SwingMelee()
	{
		Vector2 cornerDistance = new Vector2(0.325f, 0.4f); //distance from center to upper right corner of melee hitbox (for drawing overlap)
		Vector2 rightCenter = rightMelee.position;
		Vector2 leftCenter = leftMelee.position;

		// get targets
		Collider2D[] hitTargets;
		if ( playerData.IsMovingRight() )
			hitTargets = Physics2D.OverlapAreaAll(rightCenter - cornerDistance, rightCenter + cornerDistance, projectileTargets);
		else
			hitTargets = Physics2D.OverlapAreaAll(leftCenter - cornerDistance, leftCenter + cornerDistance, projectileTargets);

		// do damage
		for (int i = 0; i < hitTargets.Length; i ++)
		{
			if (hitTargets[i].tag != "Wall")
				hitTargets[i].GetComponent<Actor>().StunDamage(MELEE_DAMAGE, playerData.IsMovingRight());
		}
	}
}