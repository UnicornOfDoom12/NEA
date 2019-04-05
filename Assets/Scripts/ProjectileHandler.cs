using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour {
	public GameObject Player;
	public AudioSource SoundSource;
	public AudioClip ImpactWall;
	public AudioClip ImpactEnemy;
	public AudioClip ImpactMetalEnemy;
	public GameObject ImpactWallDecal;
	public AudioClip ImpactPlayer;
	public PlayerDeathHandler PlayerDeathHandler;
	public int Damage;
	public Weapon Weapon;
	void Start(){ // run at start
		Player = GameObject.Find("Player"); // finds the player
		SoundSource = GameObject.Find("BulletSoundSource").GetComponent<AudioSource>(); // finds the soundsource
		PlayerDeathHandler = GameObject.Find("Player").GetComponent<PlayerDeathHandler>(); // finds the player death handler
		Weapon = GameObject.Find("Player").GetComponent<Weapon>(); // finds the weapon
		Damage = Weapon.Damage; // determines the damage
	}
	public void OnCollisionEnter2D(Collision2D Hit) // Run when the bullet hits something
	{
		if (Hit.gameObject.tag == "Player" || Hit.gameObject.tag == "Projectile") // If it hit another projectile or a Player
		{
			if (gameObject.name == "PlayerProjectile" || Hit.gameObject.tag == "Projectile"){ // If we were fired from a player and hit a player
				Physics2D.IgnoreCollision(Player.GetComponent<Collider2D>(), GetComponent<Collider2D>()); // ignore the collision
			}
			else{ // else do damage to the player
				Physics2D.IgnoreCollision(Player.GetComponent<Collider2D>(), GetComponent<Collider2D>()); // Stops the player being pushed back
				PlayerDeathHandler.TakeDamage(20); // Do 20 damage to the player
				var NewDecal = Instantiate(ImpactWallDecal, transform.position, transform.rotation); // instantiate the decal object
				SpriteRenderer DecalSprite = NewDecal.GetComponent<SpriteRenderer>(); // get decal sprite
				DecalSprite.color = Color.red; // change to red
				SoundSource.clip = ImpactPlayer; 
				SoundSource.Play(); // play impact sound
				Destroy(gameObject); // Destroy projectile
				Destroy(NewDecal,0.183f); // Destroy the decal after it's animation has played
			}
		}
		else if(Hit.gameObject.tag == "Enemy" && gameObject.name != "EnemyProjectile"){ // If we hit an enemy and were not fired by enemy
			SoundSource.clip = ImpactMetalEnemy;
			SoundSource.Play(); // Play correct impact sound
			Destroy(gameObject); // destroy the projectile
			var NewDecal = Instantiate(ImpactWallDecal, transform.position, transform.rotation); // create a new decal for hitting turret
			SpriteRenderer DecalSprite = NewDecal.GetComponent<SpriteRenderer>();
			DecalSprite.color = Color.yellow; // change colour to yellow
			Destroy(NewDecal,0.183f); // destroy it after animation has played
			TurretHandler Turret = Hit.gameObject.GetComponent<TurretHandler>(); // Get the component
			Turret.TakeDamage(Damage); // Deal damage to the turret
		}
		else if(Hit.gameObject.tag == "Monster" && gameObject.name != "EnemyProjectile"){ // if we hit a monster
			SoundSource.clip = ImpactPlayer; // Play the sound clip for hitting a fleshy enemy
			SoundSource.Play();
			Destroy(gameObject); // destroy the projectile
			var NewDecal = Instantiate(ImpactWallDecal, transform.position, transform.rotation); // instantiate the decal
			SpriteRenderer DecalSprite = NewDecal.GetComponent<SpriteRenderer>();
			DecalSprite.color = Color.red; // make it coloured red
			Destroy(NewDecal,0.183f); // destroy it after animation has been played
			MonsterHandler MonsterHandler = Hit.gameObject.GetComponent<MonsterHandler>();
			MonsterHandler.TakeDamage(Damage); // deal damage to the monster
		}
		else{
			SoundSource.clip = ImpactWall; // Else we hit a wall so play that sound
			SoundSource.Play();
			var NewDecal = Instantiate(ImpactWallDecal, transform.position, transform.rotation); // make a decal
			Destroy(NewDecal,0.183f); // destroy after the animation has played
			Destroy(gameObject); // destroy the projectile
		}
	}
}
