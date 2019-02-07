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
	void Start(){
		Player = GameObject.Find("Player");
		SoundSource = GameObject.Find("BulletSoundSource").GetComponent<AudioSource>();
		PlayerDeathHandler = GameObject.Find("Player").GetComponent<PlayerDeathHandler>();
	}

	public void OnCollisionEnter2D(Collision2D Hit)
	{

		if (Hit.gameObject.tag == "Player" || Hit.gameObject.tag == "Projectile")
		{
			if (gameObject.name == "PlayerProjectile" || Hit.gameObject.tag == "Projectile"){
				Physics2D.IgnoreCollision(Player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
				print("We hit a player but was fired by a player so no damage");
			}
			else{
				print("Do damage");
				PlayerDeathHandler.TakeDamage(20);
				var NewDecal = Instantiate(ImpactWallDecal, transform.position, transform.rotation);
				SpriteRenderer DecalSprite = NewDecal.GetComponent<SpriteRenderer>();
				DecalSprite.color = Color.red;
				SoundSource.clip = ImpactPlayer;
				SoundSource.Play();
				Destroy(gameObject);
				Destroy(NewDecal,0.183f);
			}
			
		}
		else if(Hit.gameObject.tag == "Enemy" && gameObject.name != "EnemyProjectile"){
			
			print("Damaging an enemy");
			SoundSource.clip = ImpactMetalEnemy;
			SoundSource.Play();
			Destroy(gameObject);
			var NewDecal = Instantiate(ImpactWallDecal, transform.position, transform.rotation);
			Destroy(NewDecal,0.183f);
		}
		else{

			SoundSource.clip = ImpactWall;
			SoundSource.Play();
			var NewDecal = Instantiate(ImpactWallDecal, transform.position, transform.rotation);
			Destroy(NewDecal,0.183f);
			Destroy(gameObject);
		}
	}
}
