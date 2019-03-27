using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScoreTracker : MonoBehaviour {
	public float DifficultyScore;
	public float FinalScore;
	public Weapon Weapon;
	public CordinateHandler CordinateHandler;
	void Start () { // run at the start of the program
		DifficultyScore = 1; // initial value
		if (Weapon.Category == "SMG"){ // determines the category for each weapon then divides by the base value to get how much it is over
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
		} // this function will set the DifficultyScore variable to a base value
	}
	public void ChangeScore(){
		FinalScore = DifficultyScore + (CordinateHandler.Cordx + CordinateHandler.Cordy)/15; // Every time the player changes room the final score gets calculated
	}
}
