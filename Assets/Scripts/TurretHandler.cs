using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHandler : MonoBehaviour {
	public int Damage;
	public float Inaccuracy;
	public int Magazine;
	public float FireRate;
	public int CurrentAmmo;
	public bool SightedPlayer;
	public GameObject Barrel;
	public GameObject Player;
	public Collider2D PlayerCollider;
	public float Timer = 2;
	public float ReloadTime = 2;
	public float TimeSinceFire;
	public bool reloading;
	public Rigidbody2D Bullet;
	public AudioClip BulletSound;
	public AudioClip SightedAlert;
	public AudioClip TurretReload;
	public AudioSource TurretSource;
	public float speed;
	public int health;
	public GameObject MuzzleAnimation;
	public GameObject Explosion;
	void Start () { // run at the start
		CurrentAmmo = Magazine; // sets the starting variables
		SightedPlayer = false; // two bools that start off false
		reloading = false;
		FireRate = FireRate / 60;
		FireRate = 1/FireRate; // determines the fire rate in time between shots
		Player = GameObject.Find("Player"); // finds the player
		PlayerCollider = GameObject.Find("Player").GetComponent<Collider2D>(); // finds the Player Collider
	}
	void FixedUpdate () { // called once a frame, used for physics
		float Passed = Time.deltaTime; // get the time passed since last call
		if (reloading){ // if the turret is reloading then add the time that has passed to the Timer
			Timer += Passed;
		}
		if(!SightedPlayer){ // if we have not yet seen the player, search for the player
			Vector2 RayDirection = new Vector2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y); // cast a ray to the player
			RaycastHit2D Ray = Physics2D.Raycast(Barrel.transform.position, RayDirection); // Get the first thing that the ray hits
			if (Ray.collider == PlayerCollider){ // if the ray hit the player then we can see them
				SightedPlayer = true; // set this variable to true
				TurretSource.clip = SightedAlert;
				TurretSource.Play(); // play a sound
			}
			else{
				transform.Rotate(new Vector3(0,0,1)); // if we cannot see the player rotate by 1 degree
			}
		}
		if (SightedPlayer){ // if we can see the player
			Vector3 Rotation = transform.position - Player.transform.position; // get vector between the player and the turret
			Rotation.Normalize(); // gives the vector a magnitude of 1, used to keep rotation ammount constant
         	float rot = Mathf.Atan2(Rotation.y, Rotation.x) * Mathf.Rad2Deg;  // turn it into a rotation value
         	transform.rotation = Quaternion.Euler(0f, 0f, rot - 270);// Look in their direction
			if (Time.time >= TimeSinceFire){ // if the turret can fire, determine by fire rate
				TimeSinceFire = Time.time + FireRate; // reset the timer
				Fire(); // fire the weapon
			}
			Vector2 RayDirection = new Vector2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y);// send out ray to confirm we can still see player
			RaycastHit2D Ray = Physics2D.Raycast(Barrel.transform.position, RayDirection); 
			if (Ray.collider != PlayerCollider && Ray.transform.tag != "Projectile"){ // if the ray hits anything but the player or projectile then we can no longer see them
				SightedPlayer = false;
			}
		}
	}
	void Fire(){ // used for shooting at the player
		bool CanShoot = true; // Can shoot starts true and gets disproved
		if (Timer <  ReloadTime){ // if still reloading then we cant shoot
			CanShoot = false;
		}
		else{ // if were not then reloading = false
			reloading = false;
		}
		if(CurrentAmmo <= 0){ // if we got no ammo we cant shoot
			CanShoot = false;
			Reload(); // runs reload function
		}
		if (CanShoot){ // if we can shoot
			Rigidbody2D FiredBullet = Instantiate(Bullet, Barrel.transform.position, transform.rotation) as Rigidbody2D; // instantiate a projectile
			Vector3 BulletDirection = transform.up;
			BulletDirection.x = BulletDirection.x + UnityEngine.Random.Range(-1 * (Inaccuracy/75),(Inaccuracy/75));
			BulletDirection.y = BulletDirection.y + UnityEngine.Random.Range(-1 * (Inaccuracy/75),(Inaccuracy/75));
			FiredBullet.AddForce(BulletDirection * speed); // find a direction based on inaccuracy and add a force to the projectile based on speed
			FiredBullet.name = "EnemyProjectile"; // change name to enemy projectile for collision detection
			CurrentAmmo -= 1; // reduce the ammo in the turret
			TurretSource.clip = BulletSound;
			TurretSource.Play(); // play a gunfire sound
			var flash = Instantiate(MuzzleAnimation, Barrel.transform.position, Barrel.transform.rotation); // instantiate a muzzle flash
			Destroy(flash, 0.183f); // destroy muzzle flash after animation

		}
	}
	public void TakeDamage(int Damage){ // used for when the turret gets hit
		health -= Damage; // Take away from turret health
		SightedPlayer = true; // if the turret gets hit it will turn to face the player
		if (health <= 0){ // if the turret dies
			GameObject newExplosion = Instantiate(Explosion, transform.position, transform.rotation) as GameObject; // instantiate explosion
			Destroy(newExplosion,0.350f); // destroy explosion after animation
			Destroy(gameObject); // destroy the turret
		}
	}
	void Reload(){ // reload function
		reloading = true; // sets reloading to true
		Timer = 0; // resets timer
		TurretSource.clip = TurretReload;
		TurretSource.Play(); // plays turret reloading sound
		CurrentAmmo = Magazine; // resets the ammo counter
	}
}
