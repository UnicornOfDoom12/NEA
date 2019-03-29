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
	public MapConnectionHandler MapConnectionHandler;
	public BoxHandler BoxHandler;
	public DifficultyScoreTracker DifficultyScoreTracker;
	void Start () {
			Cordx = 0; // assigns values to zero
			Cordy = 0;
			DatabaseHandler.ReSelect();  // runs the function in database handler that updates the objects.
			MapMarkerHandler.UpdatePosition(); // Changes the minimap
			using(var RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;")){
				RoomDB.Open(); // open the connection
				string CMDString = "SELECT EnemyNo FROM tblRoom WHERE Roomx=@x AND Roomy=@y"; // Pulls the amount of enemies in the room
				using(SqliteCommand CMD = new SqliteCommand(CMDString,RoomDB)){
					CMD.Parameters.AddWithValue("@y",Cordy);
					CMD.Parameters.AddWithValue("@x",Cordx); 
					using(var reader = CMD.ExecuteReader()){
					int EnemyAmount = Convert.ToInt32(reader["EnemyNo"]);
					RoomDB.Close(); // closes the connection properly
					RoomDB.Dispose();
					GC.Collect();
					GC.WaitForPendingFinalizers();
					reader.Close();
					DifficultyScoreTracker.ChangeScore(); // Changes the difficulty score
					PhysicalConnectionHandler.DetermineConnections(Cordx,Cordy); // Draws the connnections to the room in the correct place
					MapConnectionHandler.DrawConnections(); // Draws connections on minimap
					EnemySpawner.SpawnEnemies(EnemyAmount); // runs the enemy spawner with how many enemies are in the room
					}
				}

			} // define connection to database

		}
	
	// Update is called once per frame
	void Update () {
		/*
		if ( Input.GetKeyDown(KeyCode.L) == true && Cordx < 3){ // detects inputs using ijkl instead of wasd
			MoveEast();
		}
		if ( Input.GetKeyDown(KeyCode.J) == true && Cordx > 0){ // detects inputs using ijkl instead of wasd
			MoveWest();
		}
		if ( Input.GetKeyDown(KeyCode.I) == true && Cordy > 0){ // detects inputs using ijkl instead of wasd
			MoveNorth();
		}		
		if ( Input.GetKeyDown(KeyCode.K) == true && Cordy < 3){ // detects inputs using ijkl instead of wasd
			MoveSouth();
		}
		if (Input.GetKeyDown(KeyCode.Escape) == true){
			LoadNewScene.LoadSceneByIndex(0);
		}
		*/
	}
	public void MoveSouth(){
		Cordy +=1;// changes the variable
		BoxHandler.DeleteBoxes(); // Deletes the boxes in the room so they are not carried over
		DatabaseHandler.ReSelect();  // runs the function in database handler that updates the objects.
		MapMarkerHandler.UpdatePosition(); // Updates map marker position
		EnemySpawner.DeleteEnemies();	// Deletes enemies so they are not carried over
		using(var RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;")){ // define connection to database
			RoomDB.Open(); // open the connection
			string CMDString = "SELECT EnemyNo FROM tblRoom WHERE Roomx=@x AND Roomy=@y";
			using(SqliteCommand CMD = new SqliteCommand(CMDString,RoomDB)){
				CMD.Parameters.AddWithValue("@x",Cordx);
				CMD.Parameters.AddWithValue("@y",Cordy);
				using(var reader = CMD.ExecuteReader()){
					int EnemyAmount = Convert.ToInt32(reader["EnemyNo"]);
					reader.Close();
					RoomDB.Close(); // closes the connection properly
					RoomDB.Dispose();
					GC.Collect();
					GC.WaitForPendingFinalizers();
					DifficultyScoreTracker.ChangeScore(); // Changes the difficulty score
					//PhysicalConnectionHandler.DetermineConnections(Cordx,Cordy); // Will draw doors in the correct position
					EnemySpawner.SpawnEnemies(EnemyAmount); // Spawns enemies
				}
			}
		}
	}
	public void MoveNorth(){
		Cordy -=1;// changes the variable
		BoxHandler.DeleteBoxes(); // Deletes the boxes in the room so they are not carried
		DatabaseHandler.ReSelect();  // runs the function in database handler that updates the objects.
		MapMarkerHandler.UpdatePosition(); // Changes the position of the minimp marker
		EnemySpawner.DeleteEnemies(); // Deletes previous enemies
		using(var RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;")){ // define connection to database
			RoomDB.Open(); // open the connection
			string CMDString = "SELECT EnemyNo FROM tblRoom WHERE Roomx=@x AND Roomy=@y";
			using(SqliteCommand CMD = new SqliteCommand(CMDString,RoomDB)){
				CMD.Parameters.AddWithValue("@x",Cordx);
				CMD.Parameters.AddWithValue("@y",Cordy);
				using(var reader = CMD.ExecuteReader()){
					int EnemyAmount = Convert.ToInt32(reader["EnemyNo"]);
					reader.Close();
					RoomDB.Close(); // closes the connection properly
					RoomDB.Dispose();
					GC.Collect();
					GC.WaitForPendingFinalizers();
					DifficultyScoreTracker.ChangeScore(); // Changes the difficulty score
					//PhysicalConnectionHandler.DetermineConnections(Cordx,Cordy); // Draws physicial connections
					EnemySpawner.SpawnEnemies(EnemyAmount); // Spawns enemies in the room
					
				}
			}
		}		
	}
	public void MoveEast(){
		Cordx +=1;// changes the variable
		BoxHandler.DeleteBoxes(); // Deletes boxes so they are not carried over
		DatabaseHandler.ReSelect();  // runs the function in database handler that updates the objects.
		MapMarkerHandler.UpdatePosition(); // Updates the position of the map marker
		EnemySpawner.DeleteEnemies(); // Deletes enemies so they are not carried over
		using(var RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;")){ // define connection to database
			RoomDB.Open(); // open the connection
			string CMDString = "SELECT EnemyNo FROM tblRoom WHERE Roomx=@x AND Roomy=@y"; // Selects enemies number
			using(SqliteCommand CMD = new SqliteCommand(CMDString,RoomDB)){
				CMD.Parameters.AddWithValue("@x",Cordx);
				CMD.Parameters.AddWithValue("@y",Cordy);
				using(var reader = CMD.ExecuteReader()){
					int EnemyAmount = Convert.ToInt32(reader["EnemyNo"]); // is equal to the ammount of enemies in the room
					reader.Close();
					RoomDB.Close(); // closes the connection properly
					RoomDB.Dispose();
					GC.Collect();
					GC.WaitForPendingFinalizers();
					DifficultyScoreTracker.ChangeScore(); // Changes the difficulty score
					//PhysicalConnectionHandler.DetermineConnections(Cordx,Cordy); // draws physicial connections
					EnemySpawner.SpawnEnemies(EnemyAmount); // spawns the enemies in the room
				}
			}
		}		
	}
	public void MoveWest(){
		Cordx -=1;// changes the variable
		BoxHandler.DeleteBoxes(); // Deletes the previous boxes so they are not carried over
		DatabaseHandler.ReSelect();  // runs the function in database handler that updates the objects.
		MapMarkerHandler.UpdatePosition(); // updates the map marker position
		EnemySpawner.DeleteEnemies(); // Deletes the old enemies so they do not get carried over
		using(var RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;")){ // define connection to database
			RoomDB.Open(); // open the connection
			string CMDString = "SELECT EnemyNo FROM tblRoom WHERE Roomx=@x AND Roomy=@y";
			using(SqliteCommand CMD = new SqliteCommand(CMDString,RoomDB)){
				CMD.Parameters.AddWithValue("@x",Cordx);
				CMD.Parameters.AddWithValue("@y",Cordy);
				using(var reader = CMD.ExecuteReader()){
					int EnemyAmount = Convert.ToInt32(reader["EnemyNo"]);
					reader.Close();
					RoomDB.Close(); // closes the connection properly
					RoomDB.Dispose();
					GC.Collect();
					GC.WaitForPendingFinalizers();
					DifficultyScoreTracker.ChangeScore(); // Changes the diifficulty score of the game
					//PhysicalConnectionHandler.DetermineConnections(Cordx,Cordy); //  Draws the physicial connections
					EnemySpawner.SpawnEnemies(EnemyAmount); // spawns the enemies in the room
				}
			}
		}	
	}
}
