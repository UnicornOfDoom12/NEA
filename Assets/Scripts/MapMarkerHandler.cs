﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMarkerHandler : MonoBehaviour {
	public void UpdatePosition(){
		GameObject Tracker = GameObject.Find("Cordinate Tracker"); // Finds the gameobject resposible for tracking cordx and cordy
		CordinateHandler CordinateHandler = Tracker.GetComponent<CordinateHandler>(); // adds the script with the values of cordx inside
		int Cordx = CordinateHandler.Cordx; // assigns the values of cordx to a local variable
		int Cordy = CordinateHandler.Cordy; // assigns the values of cordx to a local variable
		float posx = -50 + -6.5f + (4 * Cordx); // adjust the position of the marker in the x direction
		float posy = 3.0f - (2 * Cordy); // adjust the position of the marker in the y direction
		Vector3 NewPosition = new Vector3(posx,posy,0); // Converts the position into a vector
		transform.position = NewPosition; // assigns the vector
	}
}
