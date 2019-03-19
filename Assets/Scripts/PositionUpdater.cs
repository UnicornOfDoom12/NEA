using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Data;
using Mono.Data.Sqlite;
using System.Text;
using UnityEngine.UI;
using UnityEngine; // Importing required modules
public class PositionUpdater : MonoBehaviour{

	// Use this for initialization

	public string yArg; // Assigned in unity editor these values are used to determine which row of the database to use
	public string xArg;
	public string rotArg;

	public void SelectData(){ // Function used to read data from the database
		GameObject Tracker = GameObject.Find("Cordinate Tracker"); // Finds the gameobject resposible for tracking cordx and cordy
		CordinateHandler CordinateHandler = Tracker.GetComponent<CordinateHandler>(); // adds the script with the values of cordx inside
		int Cordx = CordinateHandler.Cordx; // assigns the values of cordx to a local variable
		int Cordy = CordinateHandler.Cordy; // assigns the values of cordx to a local variable
		var RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;"); // adds a connection to the correct database
		RoomDB.Open(); // opens connection
		string CMDString = "select " + xArg + ", " + yArg + ", " + rotArg + " from tblRoom where Roomx=@Cordx and Roomy=@Cordy"; // constructs sql query with rotation
		if (rotArg == "None"){
			CMDString = "select " + xArg + ", " + yArg + " from tblRoom where Roomx=@Cordx and Roomy=@Cordy"; // constructs query differently if there are is no rotation
		}
		
		
		SqliteCommand CMD = new SqliteCommand(CMDString, RoomDB); // constructs the query command
		CMD.Parameters.AddWithValue("@Cordx", Cordx);// adds the parameters for the query
		CMD.Parameters.AddWithValue("@Cordy", Cordy);// adds the parameters for the query
		using(var reader = CMD.ExecuteReader()){ // executes the comman
			float Positionx = Convert.ToSingle(reader[xArg]); // assings local variables to the correct value in the table
			float Positiony = Convert.ToSingle(reader[yArg]); // assings local variables to the correct value in the table
			Vector3 Vec = new Vector3 (Positionx, Positiony,0f); // Creates a new vector made up of the values in the database
			transform.position = Vec; // Assings the new vector to the position component of the object changing it's onscreen position
			// MAYBE TODO: potential to add collision detection here
			if (rotArg != "None"){ // if there is a rotation
				int Rotation = Convert.ToInt32(reader[rotArg]); // finds the rotation value needed
				transform.rotation = Quaternion.Euler(0,0,Rotation); // changes the rotation of the object.
			}
			
		}
		RoomDB.Close();	// closes connection
		GC.Collect();// closes connection
		GC.WaitForPendingFinalizers();// closes connection

	}

}
