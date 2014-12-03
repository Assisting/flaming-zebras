﻿using UnityEngine;
using System.Collections;

public class PlayerSounds : MonoBehaviour {

	public AudioClip jump;
	public AudioClip landing;
	public AudioClip dash;
	public AudioClip melee;
	public AudioClip shield;
	public AudioClip laser;
	public AudioClip bullet;
	public AudioClip missile;
	public AudioClip hurt;
	public AudioClip chicken1;
	public AudioClip chicken2;

	public void PlayJump()
	{
		audio.PlayOneShot(jump, 0.85f);
	}

	public void PlayLanding()
	{
		audio.PlayOneShot(landing);
	}

	public void PlayDash()
	{
		audio.PlayOneShot(dash);
	}

	public void PlayMelee()
	{
		audio.PlayOneShot(melee);
	}

	public void PlayShield()
	{
		audio.PlayOneShot(shield);
	}

	public void PlayLaser()
	{
		audio.PlayOneShot(laser);
	}

	public void PlayBullet()
	{
		audio.PlayOneShot(bullet, 0.6f);
	}

	public void PlayMissile()
	{
		audio.PlayOneShot(missile, 0.65f);
	}

	public void PlayHurt()
	{
		audio.PlayOneShot(hurt);
	}

	public void PlayChicken1()
	{
		audio.PlayOneShot(chicken1);
	}

	public void PlayChicken2()
	{
		audio.PlayOneShot(chicken2);
	}
}
