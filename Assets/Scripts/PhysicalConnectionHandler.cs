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

public class PhysicalConnectionHandler : MonoBehaviour {
	public GameObject WestObj; // The 4 objects to be used
	public GameObject EastObj;
	public GameObject SouthObj;
	public GameObject NorthObj;
	public void DetermineConnections(int x, int y){
		SqliteConnection RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;"); // define connection to database
		RoomDB.Open();
		string CMDString = "SELECT NCon, SCon, ECon, WCon FROM tblRoom WHERE Roomx=@x AND Roomy=@y"; // Selects if there are connections inside this node
		SqliteCommand CMD = new SqliteCommand(CMDString,RoomDB);
		CMD.Parameters.AddWithValue("@x", x); // adds parameters to the sqlite
		CMD.Parameters.AddWithValue("@y", y); 
		var reader = CMD.ExecuteReader(); // executes the sqlite
		bool North = Convert.ToBoolean(reader["NCon"]); // retreives the sqlite
		bool South = Convert.ToBoolean(reader["SCon"]); // converts the required values for north,south,east and west into booleans
		bool East = Convert.ToBoolean(reader["ECon"]);
		bool West = Convert.ToBoolean(reader["WCon"]);
		RoomDB.Close();
		RoomDB.Dispose();
		bool FromAbove = false;
		bool FromBelow = false;
		bool FromRight = false;
		bool FromLeft = false;
		if (y > 0){
			FromAbove = FromOutside("SCon",x, y-1); // If the node is not in the top row, see if a node above connects downwards
		}
		if (y < 3){
			FromBelow = FromOutside("NCon",x,y+1); // if the node is not the bottom row see if a node below connects upwards
		}
		if (x > 0){
			FromLeft = FromOutside("ECon",x-1,y); // if the node is not the left most node see if a there is a node to the left connecting rght
		}
		if (x < 3){
			FromRight = FromOutside("WCon",x+1,y); // if the node is not the right most collum, see if there is a node to the right connecting left
		}

		if (North || FromAbove){
			NorthObj.transform.position = new Vector3 (0.0f, 4.45f, 0.0f); // if there is a connectiong outgoing up, or one coming down move the north object into position
		}
		else{
			NorthObj.transform.position = new Vector3 (0.0f, 10.0f, 0.0f); // else move out of players view
		}
		if (South || FromBelow){
			SouthObj.transform.position = new Vector3(0.0f, -4.45f, 0.0f); // if there is a connection outgoing down, or one coming up move the south object into position
		}
		else{
			SouthObj.transform.position = new Vector3 (0.0f, -10.0f, 0.0f); // else move it out of players view
		}
		if(East || FromRight){
			EastObj.transform.position = new Vector3(10.23f, 0.0f, 0.0f); // if there is a node connecting to the right, or one coming in move the east object into position
		}
		else{
			EastObj.transform.position = new Vector3(15.23f, 0.0f, 0.0f); // else move it out of players view
		}
		if (West || FromLeft){
			WestObj.transform.position = new Vector3(-10.23f, 0.0f, 0.0f); // if there is a node connecting to the left or one coming in, move the west object into position
		}
		else{
			WestObj.transform.position = new Vector3(-15.23f, 0.0f, 0.0f); // else move it out of players view
		}
	}
	public bool FromOutside(string Direction, int x, int y){ // parameters = direction and coordinates of node to search, returns a boolean
		using(SqliteConnection RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;")){ // define connection to database
			RoomDB.Open();
			string CMDString = "SELECT " + Direction + " FROM tblRoom WHERE Roomx=@x AND Roomy=@y"; // select the correct direction from the specified node
			using(SqliteCommand CMD = new SqliteCommand(CMDString,RoomDB)){
				CMD.Parameters.AddWithValue("@x", x);
				CMD.Parameters.AddWithValue("@y", y);
				using(var reader = CMD.ExecuteReader()){
					bool Present = Convert.ToBoolean(reader[Direction]); // convert value to bool 
					return Present; // return the value
					reader.Close();
					RoomDB.Close();
					RoomDB.Dispose();
				}
			}
		}
	}
}
