using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Data;
using Mono.Data.Sqlite;
using System.Text;
using UnityEngine.UI;
using UnityEngine; // imports including sqlite
public class PhysicalConnectionHandler : MonoBehaviour {
	public string Direction;
	public void DrawConnections(){
		GameObject Tracker = GameObject.Find("Cordinate Tracker"); // Finds the gameobject resposible for tracking cordx and cordy
		CordinateHandler CordinateHandler = Tracker.GetComponent<CordinateHandler>(); // adds the script with the values of cordx inside
		var RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;"); // adds a connection to the correct database
		RoomDB.Open(); // opens connection
		int x = CordinateHandler.Cordx;
		int y = CordinateHandler.Cordy;
		//print(Direction);
		string CMDString = "select NCon, SCon, ECon, WCon from tblRoom where Roomx=@x and Roomy=@y";
		//print(CMDString);
		SqliteCommand CMD = new SqliteCommand(CMDString, RoomDB); // constructs the query command
		CMD.Parameters.AddWithValue("@x", x);// adds the parameters for the query
		CMD.Parameters.AddWithValue("@y", y);// adds the parameters for the query
		using(var reader = CMD.ExecuteReader()){ // executes the comman
			bool Present = Convert.ToBoolean(reader[Direction]); // assings local variables to the correct value in the table
			if (Present && Direction == "NCon"){
				gameObject.transform.position = new Vector3(0, 4.25f, 0);
			}
			else{
				gameObject.transform.position = new Vector3(0, 15.25f, 0);
			}
			if (Present && Direction == "SCon"){
				gameObject.transform.position = new Vector3(0, -4.25f, 0);
			}
			else{
				gameObject.transform.position = new Vector3(0, 15.25f, 0);
			}
			if (Present && Direction == "ECon"){
				gameObject.transform.position = new Vector3(10, 0, 0);
			}
			else{
				gameObject.transform.position = new Vector3(0, 15.25f, 0);
			}
			if (Present && Direction == "WCon"){
				gameObject.transform.position = new Vector3(-10, 0, 0);
			}
			else{
				gameObject.transform.position = new Vector3(0, 15.25f, 0);
			}
		}
		RoomDB.Close();
		var RoomDB1 = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;");
		RoomDB1.Open();
		SqliteCommand CMD1 = new SqliteCommand(CMDString,RoomDB1);
		if (Direction == "NCon"){
			y -= 1;
			CMD1.Parameters.AddWithValue("@x", x);// adds the parameters for the query
			CMD1.Parameters.AddWithValue("@y", y);// adds the parameters for the query
		}
		if (Direction == "SCon"){
			y += 1;
			CMD1.Parameters.AddWithValue("@x", x);// adds the parameters for the query
			CMD1.Parameters.AddWithValue("@y", y);// adds the parameters for the query			
		}
		if (Direction == "WCon"){
			x -= 1;
			CMD1.Parameters.AddWithValue("@x", x);// adds the parameters for the query
			CMD1.Parameters.AddWithValue("@y", y);// adds the parameters for the query
		}
		if (Direction == "ECon"){
			x += 1;
			CMD1.Parameters.AddWithValue("@x", x);// adds the parameters for the query
			CMD1.Parameters.AddWithValue("@y", y);// adds the parameters for the query
		}
		else{
			CMD1.Parameters.AddWithValue("@x",x); 
			CMD1.Parameters.AddWithValue("@y",y);
		}
		using(var reader = CMD1.ExecuteReader()){ // executes the comman
			bool Present = Convert.ToBoolean(reader[Direction]); // assings local variables to the correct value in the table
			if (Present && Direction == "NCon"){
				gameObject.transform.position = new Vector3(0, 4.25f, 0);
			}
			else{
				gameObject.transform.position = new Vector3(0, 15.25f, 0);
			}
			if (Present && Direction == "SCon"){
				gameObject.transform.position = new Vector3(0, -4.25f, 0);
			}
			else{
				gameObject.transform.position = new Vector3(0, 15.25f, 0);
			}
			if (Present && Direction == "ECon"){
				gameObject.transform.position = new Vector3(10, 0, 0);
			}
			else{
				gameObject.transform.position = new Vector3(0, 15.25f, 0);
			}
			if (Present && Direction == "WCon"){
				gameObject.transform.position = new Vector3(-10, 0, 0);
			}
			else{
				gameObject.transform.position = new Vector3(0, 15.25f, 0);
			}
		}
	}	
}