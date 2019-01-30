using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Mono.Data;
using Mono.Data.Sqlite;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using System.Threading; // imports including sqlite

public class PlayerShoot : MonoBehaviour {
	public Weapon Weapon;
	public Rigidbody2D Projectile;
	public Animator Animator;
	public float ProjectileSpeed = 100;
	public float FireRate;
	public float TimeSinceFire;
	public int CurrentAmmo;
	public AudioSource SoundSource;
	public AudioClip ReloadClip;
	public AudioClip ShootClip;
	public Vector3 BarrelPos;

	void Start(){
		FireRate = Weapon.EquippedWeapon.FireRate;
		CurrentAmmo = Weapon.Magazine;
		BarrelPos = new Vector3(transform.position.x + 1,transform.position.y - 0.35f,0);
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)){
			Animator.SetBool("IsShooting",false);
			if (Time.time >= TimeSinceFire){
				TimeSinceFire = Time.time + FireRate;
				Fire();
			}
			else{
				//print("Attempted to fire " + Weapon.FireRate.ToString());
			}
		}
		if (Input.GetKeyDown(KeyCode.R)){
			Reload();
		}
	}
	void Fire(){
		Animator.SetBool("IsReloading",false);
		bool CanShoot = true;
		if (Weapon.Category == ""){
			print("You got a knife so you melee attack");
			CanShoot = false;
			MeleeAttack();
		}
		else if (CurrentAmmo <= 0){
			
			CanShoot = false;
			Reload();
		}
		if (CanShoot){
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Rigidbody2D FiredBullet = Instantiate(Projectile, BarrelPos,Quaternion.LookRotation(transform.position - mousePos, Vector3.forward)) as Rigidbody2D;
			FiredBullet.AddForce(transform.right * ProjectileSpeed);
			CurrentAmmo -= 1;
			SoundSource.clip = ShootClip;
			SoundSource.Play();
			Animator.SetBool("IsShooting",true);
			print("BANG you have " + CurrentAmmo.ToString());
			

		}

	}
	void MeleeAttack(){
		// Play sound
	}
	void Reload(){
		print("reload old ammo = " + CurrentAmmo.ToString());
		// Play reload sound
		SoundSource.clip = ReloadClip;
		SoundSource.Play();
		Animator.SetBool("IsReloading",true);
		CurrentAmmo = Weapon.Magazine;
		print("New ammo = " + CurrentAmmo.ToString());
		
	}
}
