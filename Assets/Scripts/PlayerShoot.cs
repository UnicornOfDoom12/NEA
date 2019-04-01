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
	public float ProjectileSpeed;
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
	public float Timer = 2.1f;
	public float Inaccuracy;
	public float MeleeTimer = 0.7f;
	public float MeleeCooldown = 0.6f;
	private bool MeleeAttacking = false;
	public AudioClip MeleeClipHit;
	public AudioClip MeleeClipMiss;
	public Collider2D PlayerCollider;
	public Text AmmoCounter;
	public ExitHandler ExitHandler;
	public CordinateHandler CordinateHandler;
	public AudioClip OpenDoor;
	void Start(){ // Run at the start
		FireRate = Weapon.FireRate;
		FireRate = FireRate / 60;
		FireRate = 1/FireRate; // Changes the firerate from shots per minute into time between shots
		CurrentAmmo = Weapon.Magazine; // Set to full at the start of the scene
		Reloading = false; // set to false at the start
		Inaccuracy = Weapon.Inaccuracy; // Determines the inaccuracy
		MeleeAttacking = false; // Set to false at the start
		AmmoCounter.text = "Ammo = " + CurrentAmmo.ToString() + " / " + Weapon.Magazine.ToString(); // Sets the ammo counter text
		Timer = 3; // reload timer, set to aribitary value above 2.1
		MeleeTimer = 0.7f; // Set to a value above 0.6
	}
	void FixedUpdate () { // Run at the same time every frame
		float Passed = Time.deltaTime; // Set to the time that has been passes since last call
		if (Reloading){
			Timer += Passed; // adds the time past to the timer
		}
		if(MeleeAttacking){
			MeleeTimer += Passed; // adds the time past to the timer
		}
		if (MeleeTimer >= MeleeCooldown){ // if they have meleed for long enough then they are not attacking anymore
			Animator.SetBool("IsMelee",false);
			MeleeAttacking = false;
		}
		if(Timer >= ReloadTime){//if they have reloaded for long enough then they are not reloading anymore
			Animator.SetBool("IsReloading",false);
		}
		if (Input.GetMouseButton(0)){ // If the user presses left mouse button
			Animator.SetBool("IsShooting",false); // set to false as a base
			if (Time.time >= TimeSinceFire){ // If it has been long enough since the last shot then fire again
				TimeSinceFire = Time.time + FireRate; // add the fire rate to the timer
				Fire(); // run the fire function
			}
		}
		if (Input.GetKeyDown(KeyCode.V)){ // if user presses V
			MeleeAttack(); // melee attack
		}
		if (Input.GetKeyDown(KeyCode.R)){ // if user presses R
			Reload(); // reload
		}
	}
	void Fire(){ // Ran when user presses left click
		bool CanShoot = true; // Is set to true until proven false
		if (Timer < ReloadTime){ // If they are still reloading, set it to false
			CanShoot = false;
		}
		else{
			Reloading = false; // if not reloading, reloading = false
		}
		if (MeleeTimer < MeleeCooldown){ // if they are meleeing then we cant shoot
			CanShoot = false;
		}
		if (Weapon.Category == ""){ // if they have a knife, melee attack instead
			CanShoot = false;
			MeleeAttack();
		}
		else if (CurrentAmmo <= 0){ // if they have no ammo, reload
			CanShoot = false;
			Reload();
		}
		if (CanShoot){ // if they can shoot
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Get the mouse position
			Rigidbody2D FiredBullet = Instantiate(Projectile, Barrel.transform.position,Quaternion.LookRotation(transform.position - mousePos, Vector3.forward)) as Rigidbody2D;// instantiate a projectile prefab
			Vector3 BulletDirection = transform.right; // Direction = (1,0,0)
			BulletDirection.x = BulletDirection.x + UnityEngine.Random.Range(-1 * (Inaccuracy/75),(Inaccuracy/75)); // Changes the direction based on inaccuracy values
			BulletDirection.y = BulletDirection.y + UnityEngine.Random.Range(-1 * (Inaccuracy/75),(Inaccuracy/75)); 
			FiredBullet.AddForce(BulletDirection * ProjectileSpeed);// add a force to the projectile in the new direction based on the inaccuracy
			FiredBullet.name = "PlayerProjectile"; // Set the name to PlayerPorjectile for later identification
			CurrentAmmo -= 1; // reduce the ammo by 1
			SoundSource.clip = ShootClip; // play the sound file of the shooting
			SoundSource.Play();
			Animator.SetBool("IsShooting",true); // set animation of shooting to be true
			var NewFlash = Instantiate(MuzzleAnimation, Barrel.transform.position,Barrel.transform.rotation); // Instantiate a muzzle flash object
			Destroy(NewFlash,0.183f); // destroy it in 0.183 seconds (the amount of time for it to complete an animation)
			AmmoCounter.text = "Ammo = " + CurrentAmmo.ToString() + " / " + Weapon.Magazine.ToString(); // Change the text
		}
	}
	void MeleeAttack(){ // Melee attack script
		Animator.SetBool("IsMelee", true); // set the animation boolean to true
		MeleeAttacking = true; // set melee attacking to true
		MeleeTimer = 0; // the timer now = 0
		Vector2 RayDir = new Vector2(0,transform.rotation.z);
		RaycastHit2D Attack = Physics2D.Raycast(Barrel.transform.position, RayDir);// cast a ray forward from the player
		if (Attack.collider != null && Attack.distance <= 0.5f && Attack.collider != PlayerCollider){ // if the ray hits something closer than 0.5 from it, and it isnt hitting itself then:
			SoundSource.clip = MeleeClipHit; // Sets the sound clip to the melee clip
			if (Attack.collider.tag == "Finish"){ // if enemy hits the end object, finish the game
				ExitHandler.Win(); // executes the win function in win
			}
			if (Attack.collider.name == "North"){ // If the user hits the north door
				CordinateHandler.MoveNorth(); // run move north
				transform.position = new Vector3(0.0f, -4.35f, 0.0f); // change player position to appear at the bottom of the screen
				SoundSource.clip = OpenDoor; // play the door opening sound
				SoundSource.Play();
			}
			if (Attack.collider.tag == "Box"){ // if the player hits a box, run the open box functions
				WeaponGenerate Box = Attack.collider.gameObject.GetComponent<WeaponGenerate>();
				Box.OpenBox();
			}
			if (Attack.collider.name == "South"){ // if the player hits the south object move them south
				CordinateHandler.MoveSouth();
				transform.position = new Vector3(0.0f, 4.35f, 0.0f);
				SoundSource.clip = OpenDoor;
				SoundSource.Play();
			}
			if (Attack.collider.name == "East"){ // if the player hits east move the player east
				CordinateHandler.MoveEast();
				transform.position = new Vector3(-10.0f, 0.0f, 0.0f);
				SoundSource.clip = OpenDoor;
				SoundSource.Play();
			}
			if (Attack.collider.name == "West"){ // if the player hits west, move the player west
				CordinateHandler.MoveWest();
				transform.position = new Vector3(10.0f, 0.0f, 0.0f);
				SoundSource.clip = OpenDoor;
				SoundSource.Play();
			}
			if (Attack.collider.tag == "Enemy"){ // if the player hits an enemy make them take damage
				var Turret = Attack.collider.gameObject.GetComponent<TurretHandler>();
				Turret.TakeDamage(25);
			}
			if (Attack.collider.tag == "Monster"){ // if the player hits a monster make them take damage
				var Monster = Attack.collider.gameObject.GetComponent<MonsterHandler>();
				Monster.TakeDamage(25);
			}
		}
		else{
			SoundSource.clip = MeleeClipMiss; // if they dont hit anything play the sound of them missing
		}
		SoundSource.Play(); // play the sound
	}
	void Reload(){ // reload function
		Reloading = true; // Realoading = true
		Timer = 0; // reset the timer
		SoundSource.clip = ReloadClip; // play the sound
		SoundSource.Play();
		Animator.SetBool("IsReloading",true); // set the animation
		CurrentAmmo = Weapon.Magazine; // reset the ammo
		AmmoCounter.text = "Ammo = " + CurrentAmmo.ToString() + " / " + Weapon.Magazine.ToString(); // update the counter
	}
}
