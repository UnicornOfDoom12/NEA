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
	// Use this for initialization
	void Start () {
		CurrentAmmo = Magazine;
		SightedPlayer = false;
		// Determine it's position in the scene
		reloading = false;
		FireRate = FireRate / 60;
		FireRate = 1/FireRate;
		Player = GameObject.Find("Player");
		PlayerCollider = GameObject.Find("Player").GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float Passed = Time.deltaTime;
		if (reloading){
			Timer += Passed;
		}
		if(!SightedPlayer){
			Vector2 RayDirection = new Vector2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y);
			RaycastHit2D Ray = Physics2D.Raycast(Barrel.transform.position, RayDirection);
			if (Ray.collider == PlayerCollider){
				SightedPlayer = true;
				//print("Sighted the mofo");
				TurretSource.clip = SightedAlert;
				TurretSource.Play();
			}
			else{
				transform.Rotate(new Vector3(0,0,1));

			}
		}
		if (SightedPlayer){
			// Look in their direction
			// Fire  three round burst if time has passed
			Vector3 Rotation = transform.position - Player.transform.position;
			Rotation.Normalize();
         	float rot = Mathf.Atan2(Rotation.y, Rotation.x) * Mathf.Rad2Deg;
         	transform.rotation = Quaternion.Euler(0f, 0f, rot - 270);
			if (Time.time >= TimeSinceFire){
				TimeSinceFire = Time.time + FireRate;
				Fire();
			}
			
			Vector2 RayDirection = new Vector2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y);
			RaycastHit2D Ray = Physics2D.Raycast(Barrel.transform.position, RayDirection);
			if (Ray.collider != PlayerCollider && Ray.transform.tag != "Projectile"){
				SightedPlayer = false;
				print("Lost Vision");
			}

		}
	}
	void Fire(){
		bool CanShoot = true;
		if (Timer <  ReloadTime){
			CanShoot = false;
			print("Still reloading for the enemy");
		}
		else{
			reloading = false;
		}
		if(CurrentAmmo <= 0){
			CanShoot = false;
			Reload();
		}
		if (CanShoot){
			Rigidbody2D FiredBullet = Instantiate(Bullet, Barrel.transform.position, transform.rotation) as Rigidbody2D;
			Vector3 BulletDirection = transform.up;
			BulletDirection.x = BulletDirection.x + UnityEngine.Random.Range(-1 * (Inaccuracy/75),(Inaccuracy/75));
			BulletDirection.y = BulletDirection.y + UnityEngine.Random.Range(-1 * (Inaccuracy/75),(Inaccuracy/75));
			FiredBullet.AddForce(BulletDirection * speed);
			FiredBullet.name = "EnemyProjectile";
			CurrentAmmo -= 1;
			TurretSource.clip = BulletSound;
			TurretSource.Play();
			print("Turret Fired");
			var flash = Instantiate(MuzzleAnimation, Barrel.transform.position, Barrel.transform.rotation);
			Destroy(flash, 0.183f);

		}
	}
	public void TakeDamage(int Damage){
		print("TakingDamage");
		health -= Damage;
		SightedPlayer = true;
		if (health <= 0){
			// Play death animation here and sound
			GameObject newExplosion = Instantiate(Explosion, transform.position, transform.rotation) as GameObject;
			Destroy(newExplosion,0.350f);
			Destroy(gameObject);
		}
	}
	void Reload(){
		reloading = true;
		Timer = 0;
		TurretSource.clip = TurretReload;
		TurretSource.Play();
		CurrentAmmo = Magazine;
	}
}
