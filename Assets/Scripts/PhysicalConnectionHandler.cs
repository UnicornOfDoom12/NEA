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
	public GameObject WestObj;
	public GameObject EastObj;
	public GameObject SouthObj;
	public GameObject NorthObj;
	public void DetermineConnections(int x, int y){
		SqliteConnection RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;"); // define connection to database
		RoomDB.Open();
		string CMDString = "SELECT NCon, SCon, ECon, WCon FROM tblRoom WHERE Roomx=@x AND Roomy=@y";
		SqliteCommand CMD = new SqliteCommand(CMDString,RoomDB);
		CMD.Parameters.AddWithValue("@x", x);
		CMD.Parameters.AddWithValue("@y", y);
		var reader = CMD.ExecuteReader();
		bool North = Convert.ToBoolean(reader["NCon"]);
		bool South = Convert.ToBoolean(reader["SCon"]);
		bool East = Convert.ToBoolean(reader["ECon"]);
		bool West = Convert.ToBoolean(reader["WCon"]);
		RoomDB.Close();
		bool FromAbove = false;
		bool FromBelow = false;
		bool FromRight = false;
		bool FromLeft = false;
		if (y > 0){
			FromAbove = FromOutside("SCon",x, y-1);
		}
		if (y < 3){
			FromBelow = FromOutside("NCon",x,y+1);
		}
		if (x > 0){
			FromLeft = FromOutside("ECon",x-1,y);
		}
		if (x < 3){
			FromRight = FromOutside("WCon",x+1,y);
		}

		if (North || FromAbove){
			NorthObj.transform.position = new Vector3 (0.0f, 4.45f, 0.0f);
		}
		else{
			NorthObj.transform.position = new Vector3 (0.0f, 10.0f, 0.0f);
		}
		if (South || FromBelow){
			SouthObj.transform.position = new Vector3(0.0f, -4.45f, 0.0f);
		}
		else{
			SouthObj.transform.position = new Vector3 (0.0f, -10.0f, 0.0f);
		}
		if(East || FromRight){
			EastObj.transform.position = new Vector3(10.23f, 0.0f, 0.0f);
		}
		else{
			EastObj.transform.position = new Vector3(15.23f, 0.0f, 0.0f);
		}
		if (West || FromLeft){
			WestObj.transform.position = new Vector3(-10.23f, 0.0f, 0.0f);
		}
		else{
			WestObj.transform.position = new Vector3(-15.23f, 0.0f, 0.0f);
		}
		
	}

	public bool FromOutside(string Direction, int x, int y){
		SqliteConnection RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;"); // define connection to database
		RoomDB.Open();
		string CMDString = "SELECT " + Direction + " FROM tblRoom WHERE Roomx=@x AND Roomy=@y";
		SqliteCommand CMD = new SqliteCommand(CMDString,RoomDB);
		CMD.Parameters.AddWithValue("@x", x);
		CMD.Parameters.AddWithValue("@y", y);
		var reader = CMD.ExecuteReader();
		print(x);
		print(y);
		print(Direction);
		bool Present = Convert.ToBoolean(reader[Direction]);
		return Present;
	}
}
