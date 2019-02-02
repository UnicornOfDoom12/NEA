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
	public GameObject Barrel;
	public GameObject MuzzleAnimation;
	public bool Reloading;
	public float ReloadTime = 2;
	public float Timer = 0;
	public float Inaccuracy;
	public float MeleeTimer = 0;
	public float MeleeCooldown = 0.6f;
	public bool MeleeAttacking = false;
	public AudioClip MeleeClip;
	void Start(){
		FireRate = Weapon.FireRate;
		FireRate = FireRate / 60;
		FireRate = 1/FireRate;
		CurrentAmmo = Weapon.Magazine;
		Reloading = false;
		Inaccuracy = Weapon.Inaccuracy;
		
	}
	// Update is called once per frame
	void Update () {
		float Passed = Time.deltaTime;
		if (Reloading){
			Timer += Passed;
		}
		if(MeleeAttacking){
			MeleeTimer += Passed;
		}
		if (MeleeTimer >= MeleeCooldown){
			Animator.SetBool("IsMelee",false);
			MeleeAttacking = false;
		}
		if(Timer >= ReloadTime){
			Animator.SetBool("IsReloading",false);
		}

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
		if (Input.GetKeyDown(KeyCode.V)){
			MeleeAttack();
		}
		if (Input.GetKeyDown(KeyCode.R)){
			Reload();
		}
	}
	void Fire(){
		
		bool CanShoot = true;
		print("been reloading for " + Timer.ToString());
		if (Timer < ReloadTime){
			print("Still reloading");
			CanShoot = false;
		}
		else{
			Reloading = false;
		}
		if (MeleeTimer < MeleeCooldown){
			print("Still meleeing");
			CanShoot = false;
		}
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
			Rigidbody2D FiredBullet = Instantiate(Projectile, Barrel.transform.position,Quaternion.LookRotation(transform.position - mousePos, Vector3.forward)) as Rigidbody2D;
			Vector3 BulletDirection = transform.right;
			BulletDirection.x = BulletDirection.x + UnityEngine.Random.Range(-1 * (Inaccuracy/75),(Inaccuracy/75));
			BulletDirection.y = BulletDirection.y + UnityEngine.Random.Range(-1 * (Inaccuracy/75),(Inaccuracy/75));
			FiredBullet.AddForce(BulletDirection * ProjectileSpeed);
			CurrentAmmo -= 1;
			SoundSource.clip = ShootClip;
			SoundSource.Play();
			Animator.SetBool("IsShooting",true);
			print("BANG you have " + CurrentAmmo.ToString());
			var NewFlash = Instantiate(MuzzleAnimation, Barrel.transform.position,Barrel.transform.rotation);
			Destroy(NewFlash,0.183f);
			

		}

	}
	void MeleeAttack(){
		// Play sound
		print("doing melee");
		Animator.SetBool("IsMelee", true);
		MeleeAttacking = true;
		MeleeTimer = 0;
		SoundSource.clip = MeleeClip;
		SoundSource.Play();
		Vector2 RayPos = new Vector2(transform.position.x,transform.position.y);
		Vector2 RayDir = new Vector2(0,transform.rotation.z);
		RaycastHit2D Attack = Physics2D.Raycast(RayPos, RayDir);
		if (Attack.collider != null && Attack.distance <= 0.5f && Attack.collider.ToString() != "Player"){
			print("Hit a " + Attack.collider.ToString());
		}
	}
	void Reload(){
		Reloading = true;
		Timer = 0;
		print("reload old ammo = " + CurrentAmmo.ToString());
		SoundSource.clip = ReloadClip;
		SoundSource.Play();
		Animator.SetBool("IsReloading",true);
		CurrentAmmo = Weapon.Magazine;
		print("New ammo = " + CurrentAmmo.ToString());
		
	}
}
