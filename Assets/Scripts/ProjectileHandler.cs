using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour {

	public GameObject Player;
	public AudioSource SoundSource;
	public AudioClip ImpactWall;
	public AudioClip ImpactEnemy;
	public GameObject ImpactWallDecal;
	void Start(){
		Player = GameObject.Find("Player");
		SoundSource = GameObject.Find("BulletSoundSource").GetComponent<AudioSource>();
	}

	public void OnCollisionEnter2D(Collision2D Hit)
	{

		if (Hit.gameObject.tag == "Player")
		{
			
			Physics2D.IgnoreCollision(Player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		}
		else if(Hit.gameObject.tag == "Enemy"){
			// play blood
			print("Damaging an enemy");
			SoundSource.clip = ImpactEnemy;
			SoundSource.Play();
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
