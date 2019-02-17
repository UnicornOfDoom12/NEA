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
			MapMarkerHandler.UpdatePosition();
			using(var RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;")){
				RoomDB.Open(); // open the connection
				string CMDString = "SELECT EnemyNo FROM tblRoom WHERE Roomx=@x AND Roomy=@y";
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
					DifficultyScoreTracker.ChangeScore();
					EnemySpawner.SpawnEnemies(EnemyAmount);
					}
				}

			} // define connection to database
			PhysicalConnectionHandler.DetermineConnections(Cordx,Cordy);
			MapConnectionHandler.DrawConnections();
		}
	
	// Update is called once per frame
	void Update () {
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

	}
	public void MoveSouth(){
		Cordy +=1;// changes the variable
		BoxHandler.DeleteBoxes();
		DatabaseHandler.ReSelect();  // runs the function in database handler that updates the objects.
		MapMarkerHandler.UpdatePosition();		
		EnemySpawner.DeleteEnemies();	
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
					DifficultyScoreTracker.ChangeScore();
					EnemySpawner.SpawnEnemies(EnemyAmount);
					PhysicalConnectionHandler.DetermineConnections(Cordx,Cordy);
				}
			}
		}
	}
	public void MoveNorth(){
		Cordy -=1;// changes the variable
		BoxHandler.DeleteBoxes();
		DatabaseHandler.ReSelect();  // runs the function in database handler that updates the objects.
		MapMarkerHandler.UpdatePosition();
		EnemySpawner.DeleteEnemies();
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
					DifficultyScoreTracker.ChangeScore();
					EnemySpawner.SpawnEnemies(EnemyAmount);
					PhysicalConnectionHandler.DetermineConnections(Cordx,Cordy);
				}
			}
		}		
	}
	public void MoveEast(){
		Cordx +=1;// changes the variable
		BoxHandler.DeleteBoxes();
		DatabaseHandler.ReSelect();  // runs the function in database handler that updates the objects.
		MapMarkerHandler.UpdatePosition();
		EnemySpawner.DeleteEnemies();
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
					DifficultyScoreTracker.ChangeScore();
					EnemySpawner.SpawnEnemies(EnemyAmount);
					PhysicalConnectionHandler.DetermineConnections(Cordx,Cordy);
				}
			}
		}		
	}
	public void MoveWest(){
		Cordx -=1;// changes the variable
		BoxHandler.DeleteBoxes();
		DatabaseHandler.ReSelect();  // runs the function in database handler that updates the objects.
		MapMarkerHandler.UpdatePosition();
		EnemySpawner.DeleteEnemies();
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
					DifficultyScoreTracker.ChangeScore();
					EnemySpawner.SpawnEnemies(EnemyAmount);
					PhysicalConnectionHandler.DetermineConnections(Cordx,Cordy);
				}
			}
		}	
	}
}
