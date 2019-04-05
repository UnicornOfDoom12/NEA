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
	void Start () { // Run at the start
		Health = 100; // sets health = 100
		HealthCounter.text = "Health = "+ Health.ToString() + "/100"; // updates the counter string
	}
	public void TakeDamage(int Damage){ // Take damage used when the player is hit
		Health = Health - Damage; // Removes this ammount from the player
		if (Health <= 0){ // if the player has no health left
			KillPlayer(); // kill the player
		}
		HealthCounter.text = "Health = "+ Health.ToString() + "/100"; // update the counter string
	}
	void KillPlayer(){ // run when player dies
		SoundSource.clip = DeathClip; // load an play the sound of a player dieing
		SoundSource.Play();
		LoadNewScene.LoadSceneByIndex(3); // load the loss scene
	}
	public void RemoveDamage(int Healing){ // When the player heals
		Health = Health + Healing; // Adds health to the player
		if (Health > 100){ // makes sure they cannot exceede max health
			Health = 100;
		}
		HealthCounter.text = "Health = "+ Health.ToString() + "/100"; // updates the counter string
	}
}
