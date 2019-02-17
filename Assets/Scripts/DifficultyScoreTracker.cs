using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScoreTracker : MonoBehaviour {

	// Use this for initialization
	public float DifficultyScore;
	public float FinalScore;
	public Weapon Weapon;
	public CordinateHandler CordinateHandler;

	void Start () {
		DifficultyScore = 1;
		if (Weapon.Category == "SMG"){
			DifficultyScore += Weapon.Damage / 25;
		}
		else if (Weapon.Category == "Assault Rifle"){
			DifficultyScore += Weapon.Damage / 49;
		}
		else if (Weapon.Category == "Hand Cannon"){
			DifficultyScore += Weapon.Damage / 99;
		}
		else if (Weapon.Category == "Marksman Rifle"){
			DifficultyScore += Weapon.Damage / 70;
		}
	}
	
	public void ChangeScore(){
		FinalScore = DifficultyScore + (CordinateHandler.Cordx + CordinateHandler.Cordy)/15;
	}
}
