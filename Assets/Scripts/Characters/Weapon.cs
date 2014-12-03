using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

//-----Structs and Enums----------------------------------------------------------------------------------------------------

	public enum WeaponType { None, Bullet, Missile, Bomb, Laser, Melee, RUBBER_CHICKEN};

	public static float[] bulletReloads = new float[3] { 0.4f, 1.8f, 0.8f };
	public static int[] bulletClips = new int[3] { 1, 5, 8};

	public static float[] laserFades = new float[3] { 0.1f, 0.4f, 0.3f};

	public static int[] swordDamage = new int[3] { 35, 55, 55};
	public static float[] invulnTime = new float[3] { 2.0f, 2.0f, 2.0f};
	public static float[] shieldRegen = new float[3] { 20f, 20f, 10f};

	public static float[] chickenSpeed = new float[3] {0.6f, 0.3f, 0.15f};

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
	private PlayerSounds playerSounds;

	private float LASER_FADE; //time for laser to disappear
	private int LASER_DAMAGE = 26; //damage per laser burst
	private int LASER_BURN_DAMAGE = 2;
	private float LASER_BURN_TIME = 4f;

	private int MELEE_DAMAGE = 35;
	private float SHIELD_TIME = 2.0f;
	private float SHIELD_REGEN = 20f;
	private bool SHIELD_ACTIVE = true;
	
	private float RELOAD_WAIT = 0f; // time to wait in between clip regeneration
	private int MAX_BULLETS; //leveled maximum of shots between reload-waits
	private int BULLETS_FIRED = 0;

//-----Unity Functions--------------------------------------------------------------------------------------------------------

	void Awake ()
	{
		playerData = GetComponent<PlayerData>();
		keyBind = GetComponent<KeyBindings>();
		playerSounds = GetComponent<PlayerSounds>();
		rightMelee = transform.Find("RightSwordBox");
		leftMelee = transform.Find("LeftSwordBox");
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
				LASER_FADE = laserFades[wepLevel - 1];
				return;
			}

			case WeaponType.Melee :
			{
				RELOAD_WAIT = 0.6f;
				MAX_BULLETS = 1;
				MELEE_DAMAGE = swordDamage[wepLevel - 1];
				SHIELD_TIME = invulnTime[wepLevel - 1];
				SHIELD_REGEN = shieldRegen[wepLevel - 1];
				return;
			}

			case WeaponType.RUBBER_CHICKEN :
			{
				RELOAD_WAIT = chickenSpeed[wepLevel - 1];
				MAX_BULLETS = 1;
				MELEE_DAMAGE = 10;
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
			case WeaponType.RUBBER_CHICKEN :
			{
				Baawwk();
				return;
			}
		}
	}

	private void Reload()
	{
		BULLETS_FIRED = 0;
	}

	public void DropShield()
	{
		SHIELD_ACTIVE = false;
		Invoke("RaiseShield", SHIELD_REGEN);
	}

	private void RaiseShield()
	{
		SHIELD_ACTIVE = true;
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
		playerSounds.PlayBullet();		
	}

	private void FireMissile()
	{
		Rigidbody2D newMissile = Instantiate(missle, muzzle.position, muzzle.rotation) as Rigidbody2D;
		MissleHandler script = newMissile.GetComponent<MissleHandler>();
		script.Origin(gameObject);
		script.SetLevel( playerData.GetWeaponLevel() );
		playerSounds.PlayMissile();
	}
	
	private void FireBomb()
	{
		Transform newBomb = Instantiate(bomb, muzzle.position, muzzle.rotation) as Transform;
		BombHandler script = newBomb.GetComponent<BombHandler>();
		script.Origin(gameObject);
		script.SetLevel( playerData.GetWeaponLevel() );
		playerSounds.PlayBullet();
	}

	private void FireLaser()
	{
		int laserLevel = playerData.GetWeaponLevel();

		//draw new laser
		LineRenderer newLaser = Instantiate(laser) as LineRenderer;
		newLaser.SetPosition(0, muzzle.position);
		RaycastHit2D[] hitTargets;
		
		if ( playerData.IsMovingRight() ) //find targets
			hitTargets = Physics2D.RaycastAll(muzzle.position, Vector2.right, Mathf.Infinity, projectileTargets);
		else
			hitTargets = Physics2D.RaycastAll(muzzle.position, -Vector2.right, Mathf.Infinity, projectileTargets);

		int i = 0;
		foreach ( RaycastHit2D target in hitTargets)
		{
			Actor currentTarget = null;

			switch (target.transform.tag)
			{
				case "Platform":
				{
					break;
				}
				case "Player":
				{
					currentTarget = target.transform.GetComponent<PlayerData>();
					((PlayerData)currentTarget).SetLastHit(gameObject);
					break;
				}
				case "Enemy":
				{
					currentTarget = target.transform.GetComponent<Actor>();
					break;
				}
			}

			if (currentTarget != null)
			{
				currentTarget.StunDamage(LASER_DAMAGE, playerData.IsMovingRight());
				if (laserLevel > 1)
					currentTarget.Burn(LASER_BURN_DAMAGE, 0.5f, LASER_BURN_TIME);
				if (laserLevel < 3)
					break; //always stop at first object for level 1-2 lasers
			}
			else
				break;

			i ++;
		}

		// set laser endpoint
		if ( playerData.IsMovingRight() ) //set laser to appropriate endpoint
			newLaser.SetPosition( 1, new Vector3(muzzle.position.x + hitTargets[i].distance, muzzle.position.y, muzzle.position.z) );
		else
			newLaser.SetPosition( 1, new Vector3(muzzle.position.x - hitTargets[i].distance, muzzle.position.y, muzzle.position.z) );
		playerSounds.PlayLaser();
		Destroy(newLaser.gameObject, LASER_FADE);
			
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
		foreach (Collider2D hitTarget in hitTargets)
		{
			if (hitTarget.tag != "Platform")
			{
				Actor target = hitTarget.GetComponent<Actor>();
				if (hitTarget.tag == "Player")
					((PlayerData)target).SetLastHit(gameObject);
				target.StunDamage(MELEE_DAMAGE, playerData.IsMovingRight());
			}
		}

		playerSounds.PlayMelee();
	}

	private void Baawwk()
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
		foreach (Collider2D hitTarget in hitTargets)
		{
			if (hitTarget.tag != "Platform")
			{
				Actor target = hitTarget.GetComponent<Actor>();
				if (hitTarget.tag == "Player")
					((PlayerData)target).SetLastHit(gameObject);
				target.LifeChange(-MELEE_DAMAGE);
			}
		}
	}

//-----Getters and Setters---------------------------------------------------------------------------------------------------------------------------

	public bool ShieldUp()
	{
		return (playerData.GetWeaponType() == WeaponType.Melee && SHIELD_ACTIVE);

	}

	public float GetShieldTime()
	{
		return SHIELD_TIME;
	}

	public float GetReloadTime()
	{
		return RELOAD_WAIT;
	}

}
