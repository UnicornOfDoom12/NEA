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

public class DatabaseHandler : MonoBehaviour {

	// Use this for initialization
	public PositionUpdater PositionUpdater; // the following variables are all scripts, so that we can call their functions later
	public PositionUpdater PositionUpdater1; // assigned through the unity editor
	public PositionUpdater PositionUpdater2;
	public PositionUpdater PositionUpdater3;
	public PositionUpdater PositionUpdater4;
	public PositionUpdater PositionUpdater5;
	public EnemySpawner EnemySpawner;
	public BoxHandler BoxHandler;
	public ExitHandler ExitHandler;
	public MapExitHandler MapExitHandler;
	public MapConnectionHandler MapConnectionHandler;

	bool Rerun;
	void Start () { // run on program start
		var RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;"); // define connection to database
		RoomDB.Open(); // open the connection
		string deletequery = "DROP TABLE tblRoom"; // string for query
		SqliteCommand deletecmd = new SqliteCommand(deletequery, RoomDB); // construct the query
		deletecmd.ExecuteNonQuery(); // executes the command,(this command deletes the table that is old)
		// this query creates a new table
		string query = "CREATE TABLE tblRoom (Roomx FLOAT,Roomy FLOAT,Dpos1 FLOAT,Dpos2 FLOAT,Dpos3 FLOAT, Dpos4 FLOAT,Dpos5 FLOAT,Dpos6 FLOAT,Dpos7 FLOAT,Dpos8 FLOAT,Dpos9 FLOAT,Dpos10 FLOAT,Dpos11 FLOAT,Dpos12 FLOAT,DRot1 INTEGER,DRot2 INTEGER,DRot3 INTEGER,DRot4 INTEGER,DRot5 INTEGER,NCon BOOLEAN,SCon BOOLEAN,ECon BOOLEAN,WCon BOOLEAN,End BOOLEAN,Box BOOLEAN,EnemyNo INTEGER,Cleared BOOLEAN,PRIMARY KEY(Roomx,Roomy))";
		SqliteCommand cmd = new SqliteCommand(query, RoomDB); // constructs the query
		cmd.ExecuteNonQuery(); // executes the theory

		// get the data
		bool EndAlready = false; // used to make sure that there are not two end objects generated.
		for (int x = 0; x<4; x ++){ // iterates through all values of x
			for(int y=0; y<4; y++){ // iterates through all values of y
				float pos1 = GenXCord(); // gets an x pos
				float pos2 = GenYCord(false); // gets a y pos
				float pos3 = GenXCord();
				float pos4 = GenYCord(false);
				float pos5 = GenXCord();
				float pos6 = GenYCord(false);
				float pos7 = GenXCord();
				float pos8 = GenYCord(false);
				float pos9 = GenXCord();
				float pos10 = GenYCord(false);
				float pos11 = GenXCord(); 
				float pos12 = GenYCord(true); 
				int Rot1 = GenRotation();
				int Rot2 = GenRotation();
				int Rot3 = GenRotation();
				int Rot4 = GenRotation();
				int Rot5 = GenRotation();
				bool SCon = GenConnection("S",x,y); // gets a connection parameters used so there are not invalid connections on the edge of the map
				bool NCon = GenConnection("N",x,y);
				bool ECon = GenConnection("E",x,y);
				bool WCon = GenConnection("W",x,y);
				bool End = GenEnd(x,y,EndAlready); // generates and end, paramters used to make sure only one is made
				if (End == true){
					EndAlready = true;
				}
				bool Cleared = false; // cleared is false for all rooms at the start
				int EnemyNo = GenEnemyAmount(); // gets a random enemy amount
				bool box = GenBox(); // chance of a lootbox spawning
				// this query inserts all the values generated into the table
				string InsertQuery = "INSERT INTO tblRoom (Roomx,Roomy,Dpos1,Dpos2,Dpos3,Dpos4,Dpos5,Dpos6,Dpos7,Dpos8,Dpos9,Dpos10,Dpos11,Dpos12,DRot1,DRot2,DRot3,DRot4,DRot5, NCon, SCon, ECon, WCon, End, Box, EnemyNo, Cleared)VALUES(@Roomx,@Roomy,@Dpos1,@Dpos2,@Dpos3,@Dpos4,@Dpos5,@Dpos6,@Dpos7,@Dpos8,@Dpos9,@Dpos10,@Dpos11,@Dpos12,@DRot1,@DRot2,@DRot3,@DRot4,@DRot5, @NCon, @SCon, @ECon, @WCon, @End, @Box, @EnemyNo, @Cleared)";
				SqliteCommand InsertCommand = new SqliteCommand(InsertQuery, RoomDB);
				InsertCommand.Parameters.Add(new SqliteParameter("@Roomx",x)); // the following assigns the parameters to the query
				InsertCommand.Parameters.Add(new SqliteParameter("@Roomy",y));
				InsertCommand.Parameters.Add(new SqliteParameter("@Dpos1",pos1));
				InsertCommand.Parameters.Add(new SqliteParameter("@Dpos2",pos2));
				InsertCommand.Parameters.Add(new SqliteParameter("@Dpos3",pos3));
				InsertCommand.Parameters.Add(new SqliteParameter("@Dpos4",pos4));
				InsertCommand.Parameters.Add(new SqliteParameter("@Dpos5",pos5));
				InsertCommand.Parameters.Add(new SqliteParameter("@Dpos6",pos6));
				InsertCommand.Parameters.Add(new SqliteParameter("@Dpos7",pos7));
				InsertCommand.Parameters.Add(new SqliteParameter("@Dpos8",pos8));
				InsertCommand.Parameters.Add(new SqliteParameter("@Dpos9",pos9));
				InsertCommand.Parameters.Add(new SqliteParameter("@Dpos10",pos10));
				InsertCommand.Parameters.Add(new SqliteParameter("@Dpos11",pos11));
				InsertCommand.Parameters.Add(new SqliteParameter("@Dpos12",pos12));
				InsertCommand.Parameters.Add(new SqliteParameter("@DRot1",Rot1));
				InsertCommand.Parameters.Add(new SqliteParameter("@DRot2",Rot2));
				InsertCommand.Parameters.Add(new SqliteParameter("@DRot3",Rot3));
				InsertCommand.Parameters.Add(new SqliteParameter("@DRot4",Rot4));
				InsertCommand.Parameters.Add(new SqliteParameter("@DRot5",Rot5));
				InsertCommand.Parameters.Add(new SqliteParameter("@NCon",NCon));
				InsertCommand.Parameters.Add(new SqliteParameter("@SCon",SCon));
				InsertCommand.Parameters.Add(new SqliteParameter("@ECon",ECon));
				InsertCommand.Parameters.Add(new SqliteParameter("@WCon",WCon));
				InsertCommand.Parameters.Add(new SqliteParameter("@End",End));
				InsertCommand.Parameters.Add(new SqliteParameter("@Box",box));
				InsertCommand.Parameters.Add(new SqliteParameter("@EnemyNo",EnemyNo));
				InsertCommand.Parameters.Add(new SqliteParameter("@Cleared",Cleared));
				InsertCommand.ExecuteNonQuery(); // executes the query

			}
		}
		RoomDB.Close(); // closes the connection
		RoomDB.Dispose();
		GC.Collect();
		Rerun = CheckGraph();
		Rerun = true;
		if (!Rerun){
			Start();
		}
		MapConnectionHandler.DrawConnections();
		ReSelect(); // runs the function that updates the objects and puts them onto the screen, is run whenever a player moves between rooms.


		
	}

	public void ReSelect () { // function runs all the designated functions.
		PositionUpdater[] Posarray = {PositionUpdater,PositionUpdater1,PositionUpdater2,PositionUpdater3,PositionUpdater4,PositionUpdater5}; // makes an array of all the scripts
		for (int i = 0; i < Posarray.Length; i++){ // iterates through values in the array and executes each values function
			Posarray[i].SelectData();
		}
		BoxHandler.determinepresence(); // run to determine a boxes presence
		ExitHandler.determinepresence();// run to determine a exits presence
		MapExitHandler.DrawObject();
	}
	bool GenEnd(int x,int y,bool happened){ // generates an end value, 25% chance
		if(happened){
			return false;
		}
		x += y;
		int Selection = UnityEngine.Random.Range(0,100);
		if (x > 0){
			Selection = Selection / x;
		}
		if (x == 0){
			return false;
		}
		if (Selection < 25){
			return true;
		}
		else{
			return false;
		}
	}

	float GenXCord (){ // gens a random x cordinate
		float xcord = UnityEngine.Random.Range(-9.0f,9.0f);
		return xcord;
	}
	float GenYCord (bool Big){ // gens a random y, if the object is the big wall the paramters change
		float ycord;
		if (Big){
			ycord = UnityEngine.Random.Range(1,2);
			if (ycord == 1){
				ycord = 1.78f;
			}
			else{
				ycord = -1.7f;
			}
		}
		else{
			ycord = UnityEngine.Random.Range(-4.0f, 4.0f);
		}
		return ycord;
	}
	int GenRotation (){ // gens a random rotation
		int Rotation = UnityEngine.Random.Range(0,360);
		return Rotation;
	}
	int GenEnemyAmount (){ // ran between 1 and 4
		int EnemyNo = UnityEngine.Random.Range(1,4);
		return EnemyNo;
	}
	bool GenConnection (string Directon, int x, int y){ // gens weather or not a connection occurs
		if (Directon == "W" && x == 0)
		{
			return false;
		}
		if (Directon == "N" && y == 0){
			return false;
		}
		if (Directon == "S" && y == 3){
			return false;
		}
		if (Directon == "E" && x == 3){
			return false;
		}
		else
		{
			x += y;
			int chance = 10 * x;
			int Roll = UnityEngine.Random.Range(0,100);
			if (Roll < chance){
				return true;
			}
			else{
				return false;
			}
		}
	}
	bool GenBox(){
		int Selection = UnityEngine.Random.Range(0,100); // 25% chance of a box appearing in a room.
		if (Selection < 25){
			return true;
		}
		else{
			return false;
		}

	}
	bool CheckGraph(){
		int startNodex = 0;
		int startNodey = 0;
		int Nodey;
		int Nodex;
		Vector2Int CurrentNode;
		bool ConNorth = false;
		bool ConSouth = false;
		bool ConEast = false;
		bool ConWest = false;
		bool ConFromAbove = false;
		bool ConFromBelow = false;
		bool ConFromRight = false;
		bool ConFromLeft = false;
		string ThisNodeQuery = "";
		List<Vector2Int> ReachableNodes = new List<Vector2Int>();
		ReachableNodes.Add(new Vector2Int(0,0));
		SqliteConnection CheckingConnection = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;");
		CheckingConnection.Open();
		bool EndValueFound;
		int i = 0;
		int AmountOfNodes = ReachableNodes.Count;
		while (i < AmountOfNodes){
			CurrentNode = ReachableNodes[0];
			print("Searching");
			print(CurrentNode);
			ReachableNodes.Remove(ReachableNodes[0]);
			startNodex = CurrentNode.x;
			startNodey = CurrentNode.y;
			ThisNodeQuery = "select Roomx, Roomy, End, NCon, SCon, ECon, WCon from tblRoom where Roomx=@StartNodex and Roomy=@StartNodey";
			SqliteCommand ThisNodeCMD = new SqliteCommand(ThisNodeQuery,CheckingConnection);
			ThisNodeCMD.Parameters.AddWithValue("@StartNodex", startNodex);
			ThisNodeCMD.Parameters.AddWithValue("@StartNodey", startNodey);
			using(var reader = ThisNodeCMD.ExecuteReader()){
				EndValueFound = Convert.ToBoolean(reader["End"]);
				ConNorth = Convert.ToBoolean(reader["NCon"]);
				ConSouth = Convert.ToBoolean(reader["SCon"]);
				ConEast = Convert.ToBoolean(reader["ECon"]);
				ConWest = Convert.ToBoolean(reader["WCon"]);
			}
			if((startNodey - 1) >= 0){
			ThisNodeQuery = "SELECT SCon FROM tblRoom WHERE Roomx=@StartNodex and Roomy=@StartNodey";
			SqliteCommand AboveNodeCMD = new SqliteCommand(ThisNodeQuery,CheckingConnection);
			AboveNodeCMD.Parameters.Add(new SqliteParameter("@StartNodex", startNodex));
			AboveNodeCMD.Parameters.Add(new SqliteParameter("@StartNodey", (startNodey - 1)));
			using(var reader = AboveNodeCMD.ExecuteReader()){
				ConFromAbove = Convert.ToBoolean(reader["SCon"]);

			}
			}
			if ((startNodey + 1) <= 3){
			ThisNodeQuery = "SELECT NCon FROM tblRoom WHERE Roomx=@StartNodex and Roomy=@StartNodey";
			SqliteCommand BelowNodeCMD = new SqliteCommand(ThisNodeQuery,CheckingConnection);
			BelowNodeCMD.Parameters.Add(new SqliteParameter("@StartNodex", startNodex));
			BelowNodeCMD.Parameters.Add(new SqliteParameter("@StartNodey", (startNodey + 1)));
			using(var reader = BelowNodeCMD.ExecuteReader()){
				ConFromBelow = Convert.ToBoolean(reader["NCon"]);

			}
			}
			if((startNodex + 1) <= 3){
			ThisNodeQuery = "SELECT WCon FROM tblRoom WHERE Roomx=@StartNodex and Roomy=@StartNodey";
			SqliteCommand RightNodeCMD = new SqliteCommand(ThisNodeQuery,CheckingConnection);
			RightNodeCMD.Parameters.Add(new SqliteParameter("@StartNodex", (startNodex + 1)));
			RightNodeCMD.Parameters.Add(new SqliteParameter("@StartNodey", startNodey));
			using(var reader = RightNodeCMD.ExecuteReader()){
				ConFromRight = Convert.ToBoolean(reader["WCon"]);

			}
			}
			if((startNodex - 1 >= 0)){
			ThisNodeQuery = "SELECT ECon FROM tblRoom WHERE Roomx=@StartNodex and Roomy=@StartNodey";
			SqliteCommand LeftNodeCMD = new SqliteCommand(ThisNodeQuery,CheckingConnection);
			LeftNodeCMD.Parameters.Add(new SqliteParameter("@StartNodex", (startNodex - 1)));
			LeftNodeCMD.Parameters.Add(new SqliteParameter("@StartNodey", startNodey));
			using(var reader = LeftNodeCMD.ExecuteReader()){
				ConFromLeft = Convert.ToBoolean(reader["ECon"]);

			}
			}
			if (EndValueFound){
				print("This graph is valid");
				return EndValueFound;
			}
			else{
				if(ConNorth || ConFromAbove){
					if ((startNodey -1) >= 0){
					Nodey = startNodey - 1;
					Nodex = startNodex;
					ReachableNodes.Add(new Vector2Int(Nodex,Nodey));
					}
				}
				if(ConSouth || ConFromBelow){
					if((startNodey + 1) <= 3){
					Nodey = startNodey + 1;
					Nodex = startNodex;
					ReachableNodes.Add(new Vector2Int(Nodex,Nodey));
					}
				}
				if(ConEast || ConFromRight){
					if((startNodex + 1) <=3){
					Nodey = startNodey;
					Nodex = startNodex + 1;
					ReachableNodes.Add(new Vector2Int(Nodex,Nodey));
					}
				}
				if(ConWest || ConFromLeft){
					if((startNodex-1) >=0){
					Nodey = startNodey;
					Nodex = startNodex - 1;
					ReachableNodes.Add(new Vector2Int(Nodex,Nodey));
					}
				}
				AmountOfNodes = ReachableNodes.Count;
			}


		}
		print("This graph aint shit");
		EndValueFound = false;
		return EndValueFound;
		
	}
}
