﻿using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Data;
using Mono.Data.Sqlite;
using System.Text;
using UnityEngine.UI;
using UnityEngine; // imports including sqlite
using System.Collections;

public class ExitHandler : MonoBehaviour {
	public Sprite Closedimg; // imports the closed version of the spirte
	public Sprite Openimg; // imports the open version of the sprite , will be used for animations later
	private SpriteRenderer SpriteRender;
	public LoadNewScene LoadNewScene;
	public void determinepresence(){
		GameObject Tracker = GameObject.Find("Cordinate Tracker"); // Finds the gameobject resposible for tracking cordx and cordy
		CordinateHandler CordinateHandler = Tracker.GetComponent<CordinateHandler>(); // adds the script with the values of cordx inside
		int Cordx = CordinateHandler.Cordx; // assigns the values of cordx to a local variable
		int Cordy = CordinateHandler.Cordy; // assigns the values of cordx to a local variable
		SpriteRender = this.gameObject.GetComponent<SpriteRenderer>(); // imports the sprite render function used to change the sprite
		string path = Application.dataPath;
		path = path + "/Plugins/Rooms Table.db";
		var RoomDB = new SqliteConnection("Data Source="+path+";Version=3;"); // define connection to database
		RoomDB.Open(); // opens connection
		string CMDString = "select End from tblRoom where Roomx=@Cordx and Roomy=@Cordy"; // creates a string for the query
		SqliteCommand CMD = new SqliteCommand (CMDString, RoomDB); // constructs the query
		CMD.Parameters.AddWithValue("@Cordx", Cordx); // adds parameters to the query
		CMD.Parameters.AddWithValue("@Cordy",Cordy);// adds parameters to the query
		using (var reader = CMD.ExecuteReader()){ // executes the query
			bool Present = Convert.ToBoolean(reader["End"]); // assings the value in the database to a local variable
			
			SpriteRender.sprite = Closedimg; // changes the sprite so it is the correct version
			if (Present){ // if the value from the database is true move the object onto the screen
				
				transform.position = new Vector3(-8f,-3f,0);
			}
			else{ // else move it out of the players view
				transform.position = new Vector3(-15,1,0);
			}
		}
		RoomDB.Close(); // closes the connection properly
		RoomDB.Dispose();
		GC.Collect();
		GC.WaitForPendingFinalizers();
	}
	public void Win(){
		SpriteRender.sprite = Openimg;
		LoadNewScene.LoadSceneByIndex(4);
	}
}
