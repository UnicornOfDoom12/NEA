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
public class CordinateHandler : MonoBehaviour {

	// Use this for initialization
	public int Cordx; // variable declaration
	public int Cordy;
	public DatabaseHandler DatabaseHandler; // imports the database handler script
	public MapMarkerHandler MapMarkerHandler;
	public EnemySpawner EnemySpawner;
	public LoadNewScene LoadNewScene;
	public PhysicalConnectionHandler PhysicalConnectionHandler;
	void Start () {
			Cordx = 0; // assigns values to zero
			Cordy = 0;
			var RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;"); // define connection to database
			RoomDB.Open(); // open the connection
			string CMDString = "SELECT EnemyNo FROM tblRoom WHERE Roomx=@x AND Roomy=@y";
			SqliteCommand CMD = new SqliteCommand(CMDString,RoomDB);
			CMD.Parameters.AddWithValue("@x",Cordx);
			CMD.Parameters.AddWithValue("@y",Cordy);
			var reader = CMD.ExecuteReader();
			int EnemyAmount = Convert.ToInt32(reader["EnemyNo"]);
			EnemySpawner.SpawnEnemies(EnemyAmount);
			//PhsyicalConnectionHandler.DetermineConnections(Cordx,Cordy);
		}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetKeyDown(KeyCode.L) == true && Cordx < 3){ // detects inputs using ijkl instead of wasd
			Cordx +=1; // changes the variable
			DatabaseHandler.ReSelect();  // runs the function in database handler that updates the objects.
			MapMarkerHandler.UpdatePosition();
			EnemySpawner.DeleteEnemies();
			var RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;"); // define connection to database
			RoomDB.Open(); // open the connection
			string CMDString = "SELECT EnemyNo FROM tblRoom WHERE Roomx=@x AND Roomy=@y";
			SqliteCommand CMD = new SqliteCommand(CMDString,RoomDB);
			CMD.Parameters.AddWithValue("@x",Cordx);
			CMD.Parameters.AddWithValue("@y",Cordy);
			var reader = CMD.ExecuteReader();
			int EnemyAmount = Convert.ToInt32(reader["EnemyNo"]);
			EnemySpawner.SpawnEnemies(EnemyAmount);
			PhysicalConnectionHandler.DetermineConnections(Cordx,Cordy);

		}
		if ( Input.GetKeyDown(KeyCode.J) == true && Cordx > 0){ // detects inputs using ijkl instead of wasd
			Cordx -=1;// changes the variable
			
			DatabaseHandler.ReSelect();  // runs the function in database handler that updates the objects.
			MapMarkerHandler.UpdatePosition();
			EnemySpawner.DeleteEnemies();
			var RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;"); // define connection to database
			RoomDB.Open(); // open the connection
			string CMDString = "SELECT EnemyNo FROM tblRoom WHERE Roomx=@x AND Roomy=@y";
			SqliteCommand CMD = new SqliteCommand(CMDString,RoomDB);
			CMD.Parameters.AddWithValue("@x",Cordx);
			CMD.Parameters.AddWithValue("@y",Cordy);
			var reader = CMD.ExecuteReader();
			int EnemyAmount = Convert.ToInt32(reader["EnemyNo"]);
			EnemySpawner.SpawnEnemies(EnemyAmount);
			PhysicalConnectionHandler.DetermineConnections(Cordx,Cordy);
			
			
		}
		if ( Input.GetKeyDown(KeyCode.I) == true && Cordy > 0){ // detects inputs using ijkl instead of wasd
			Cordy -=1;// changes the variable
			DatabaseHandler.ReSelect();  // runs the function in database handler that updates the objects.
			MapMarkerHandler.UpdatePosition();
			EnemySpawner.DeleteEnemies();
			var RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;"); // define connection to database
			RoomDB.Open(); // open the connection
			string CMDString = "SELECT EnemyNo FROM tblRoom WHERE Roomx=@x AND Roomy=@y";
			SqliteCommand CMD = new SqliteCommand(CMDString,RoomDB);
			CMD.Parameters.AddWithValue("@x",Cordx);
			CMD.Parameters.AddWithValue("@y",Cordy);
			var reader = CMD.ExecuteReader();
			int EnemyAmount = Convert.ToInt32(reader["EnemyNo"]);
			EnemySpawner.SpawnEnemies(EnemyAmount);
			PhysicalConnectionHandler.DetermineConnections(Cordx,Cordy);
			
			
		}		
		if ( Input.GetKeyDown(KeyCode.K) == true && Cordy < 3){ // detects inputs using ijkl instead of wasd
			Cordy +=1;// changes the variable
			DatabaseHandler.ReSelect();  // runs the function in database handler that updates the objects.
			MapMarkerHandler.UpdatePosition();		
			EnemySpawner.DeleteEnemies();	
			var RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;"); // define connection to database
			RoomDB.Open(); // open the connection
			string CMDString = "SELECT EnemyNo FROM tblRoom WHERE Roomx=@x AND Roomy=@y";
			SqliteCommand CMD = new SqliteCommand(CMDString,RoomDB);
			CMD.Parameters.AddWithValue("@x",Cordx);
			CMD.Parameters.AddWithValue("@y",Cordy);
			var reader = CMD.ExecuteReader();
			int EnemyAmount = Convert.ToInt32(reader["EnemyNo"]);
			EnemySpawner.SpawnEnemies(EnemyAmount);
			PhysicalConnectionHandler.DetermineConnections(Cordx,Cordy);
		}
		if (Input.GetKeyDown(KeyCode.Escape) == true){
			LoadNewScene.LoadSceneByIndex(0);
		}

	}
	public void MoveSouth(){
		Cordy +=1;// changes the variable
		DatabaseHandler.ReSelect();  // runs the function in database handler that updates the objects.
		MapMarkerHandler.UpdatePosition();		
		EnemySpawner.DeleteEnemies();	
		var RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;"); // define connection to database
		RoomDB.Open(); // open the connection
		string CMDString = "SELECT EnemyNo FROM tblRoom WHERE Roomx=@x AND Roomy=@y";
		SqliteCommand CMD = new SqliteCommand(CMDString,RoomDB);
		CMD.Parameters.AddWithValue("@x",Cordx);
		CMD.Parameters.AddWithValue("@y",Cordy);
		var reader = CMD.ExecuteReader();
		int EnemyAmount = Convert.ToInt32(reader["EnemyNo"]);
		EnemySpawner.SpawnEnemies(EnemyAmount);
		PhysicalConnectionHandler.DetermineConnections(Cordx,Cordy);
	}
	public void MoveNorth(){
		Cordy -=1;// changes the variable
		DatabaseHandler.ReSelect();  // runs the function in database handler that updates the objects.
		MapMarkerHandler.UpdatePosition();
		EnemySpawner.DeleteEnemies();
		var RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;"); // define connection to database
		RoomDB.Open(); // open the connection
		string CMDString = "SELECT EnemyNo FROM tblRoom WHERE Roomx=@x AND Roomy=@y";
		SqliteCommand CMD = new SqliteCommand(CMDString,RoomDB);
		CMD.Parameters.AddWithValue("@x",Cordx);
		CMD.Parameters.AddWithValue("@y",Cordy);
		var reader = CMD.ExecuteReader();
		int EnemyAmount = Convert.ToInt32(reader["EnemyNo"]);
		EnemySpawner.SpawnEnemies(EnemyAmount);
		PhysicalConnectionHandler.DetermineConnections(Cordx,Cordy);		
	}
	public void MoveEast(){
		Cordx +=1;// changes the variable
		DatabaseHandler.ReSelect();  // runs the function in database handler that updates the objects.
		MapMarkerHandler.UpdatePosition();
		EnemySpawner.DeleteEnemies();
		var RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;"); // define connection to database
		RoomDB.Open(); // open the connection
		string CMDString = "SELECT EnemyNo FROM tblRoom WHERE Roomx=@x AND Roomy=@y";
		SqliteCommand CMD = new SqliteCommand(CMDString,RoomDB);
		CMD.Parameters.AddWithValue("@x",Cordx);
		CMD.Parameters.AddWithValue("@y",Cordy);
		var reader = CMD.ExecuteReader();
		int EnemyAmount = Convert.ToInt32(reader["EnemyNo"]);
		EnemySpawner.SpawnEnemies(EnemyAmount);
		PhysicalConnectionHandler.DetermineConnections(Cordx,Cordy);		
	}
	public void MoveWest(){
		Cordx -=1;// changes the variable
		DatabaseHandler.ReSelect();  // runs the function in database handler that updates the objects.
		MapMarkerHandler.UpdatePosition();
		EnemySpawner.DeleteEnemies();
		var RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;"); // define connection to database
		RoomDB.Open(); // open the connection
		string CMDString = "SELECT EnemyNo FROM tblRoom WHERE Roomx=@x AND Roomy=@y";
		SqliteCommand CMD = new SqliteCommand(CMDString,RoomDB);
		CMD.Parameters.AddWithValue("@x",Cordx);
		CMD.Parameters.AddWithValue("@y",Cordy);
		var reader = CMD.ExecuteReader();
		int EnemyAmount = Convert.ToInt32(reader["EnemyNo"]);
		EnemySpawner.SpawnEnemies(EnemyAmount);
		PhysicalConnectionHandler.DetermineConnections(Cordx,Cordy);		
	}
}
