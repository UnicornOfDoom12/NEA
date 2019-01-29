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

	void Start(){
		FireRate = Weapon.EquippedWeapon.FireRate;
		CurrentAmmo = Weapon.Magazine;
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)){
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
		print("====");
		print(Weapon.Magazine);
		print(CurrentAmmo);
		print(Weapon.Category);
		if (Weapon.Category == ""){
			print("You got a knife so you melee attack");
			CanShoot = false;
			MeleeAttack();
		}
		else if (CurrentAmmo <= 0){
			print("No ammo so you reload");
			CanShoot = false;
			Reload();
		}
		if (CanShoot){
			// Do projectile thingies
			CurrentAmmo -= 1;
			// Play shoot sound
			print("BANG you have " + CurrentAmmo.ToString());

		}

	}
	void MeleeAttack(){
		// Play sound
	}
	void Reload(){
		// Play reload sound
		Animator.SetBool("IsReloading",true);
		CurrentAmmo = Weapon.Magazine;
		
	}
}
