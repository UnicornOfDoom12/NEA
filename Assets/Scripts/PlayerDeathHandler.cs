using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour {
	public int Health;
	public AudioSource SoundSource;
	public AudioClip DeathClip;
	public LoadNewScene LoadNewScene;
	// Use this for initialization
	void Start () {
		Health = 100;
	}
	public void TakeDamage(int Damage){
		Health = Health - Damage;
		if (Health <= 0){
			KillPlayer();
		}
	}
	void KillPlayer(){
		SoundSource.clip = DeathClip;
		SoundSource.Play();
		LoadNewScene.LoadSceneByIndex(3);
	}
	public void RemoveDamage(int Healing){
		Health = Health + Healing;
		if (Health > 100){
			Health = 100;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
