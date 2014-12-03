using UnityEngine;
using System.Collections;

public class PlayerSounds : MonoBehaviour {

	public AudioClip jump;
	public AudioClip landing;
	public AudioClip dash;
	public AudioClip melee;
	public AudioClip laser;
	public AudioClip bullet;
	public AudioClip missile;

	private void PlayJump()
	{
		print("Jump", 0.85f);
		audio.PlayOneShot(jump);
	}

	private void PlayLanding()
	{
		print("Land");
		audio.PlayOneShot(landing);
	}

	private void PlayDash()
	{
		audio.PlayOneShot(dash);
	}

	private void PlayMelee()
	{
		audio.PlayOneShot(melee);
	}

	private void PlayLaser()
	{
		audio.PlayOneShot(laser);
	}

	private void PlayBullet()
	{
		audio.PlayOneShot(bullet);
	}

	private void PlayMissile()
	{
		audio.PlayOneShot(missile);
	}
}
