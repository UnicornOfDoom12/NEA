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
	public float ProjectileSpeed = 100;
	public float FireRate;
	public float TimeSinceFire;

	void Start(){
		FireRate = Weapon.EquippedWeapon.FireRate;

	}
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)){
			if (Time.time >= TimeSinceFire){
				TimeSinceFire = Time.time + FireRate;
				Fire();
			}
			else{
				print("Attempted to fire " + Weapon.FireRate.ToString());
			}
		}
	}
	void Fire(){
		print("Fired");
	}
}
