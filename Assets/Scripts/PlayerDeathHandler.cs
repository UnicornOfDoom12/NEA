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

public class PlayerDeathHandler : MonoBehaviour {
	public int Health;
	public AudioSource SoundSource;
	public AudioClip DeathClip;
	public LoadNewScene LoadNewScene;
	public Text HealthCounter;
	// Use this for initialization
	void Start () {
		Health = 100;
		HealthCounter.text = "Health = "+ Health.ToString() + "/100";
	}
	public void TakeDamage(int Damage){
		Health = Health - Damage;
		if (Health <= 0){
			KillPlayer();
		}
		HealthCounter.text = "Health = "+ Health.ToString() + "/100";
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
		HealthCounter.text = "Health = "+ Health.ToString() + "/100";

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
