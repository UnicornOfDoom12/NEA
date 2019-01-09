using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Data;
using Mono.Data.Sqlite;
using System.Text;
using UnityEngine.UI;
using UnityEngine; // imports including sqlite

public class MapConnectionHandler : MonoBehaviour {

	// Use this for initialization
	public GameObject Connection;

	public void DrawConnections () {
		bool ConNorth = false;
		bool ConSouth = false;
		bool ConEast = false;
		bool ConWest = false;
		bool ConFromAbove = false;
		bool ConFromBelow = false;
		bool ConFromRight = false;
		bool ConFromLeft = false;
		SqliteConnection CheckingConnection = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;");
		CheckingConnection.Open();
		for (int x = 0; x<=3; x++){
			for (int y=0; y<=3; y++){
				string ThisNodeQuery = "select Roomx, Roomy, End, NCon, SCon, ECon, WCon from tblRoom where Roomx=@x and Roomy=@y";
				SqliteCommand ThisNodeCMD = new SqliteCommand(ThisNodeQuery,CheckingConnection);
				ThisNodeCMD.Parameters.AddWithValue("@x", x);
				ThisNodeCMD.Parameters.AddWithValue("@y", y);
				ConFromAbove = false;
				ConFromBelow = false;
				ConFromLeft = false;
				ConFromRight = false;
				using(var reader = ThisNodeCMD.ExecuteReader()){

					ConNorth = Convert.ToBoolean(reader["NCon"]);
					ConSouth = Convert.ToBoolean(reader["SCon"]);
					ConEast = Convert.ToBoolean(reader["ECon"]);
					ConWest = Convert.ToBoolean(reader["WCon"]);
				}
				if((y - 1) >= 0){
					ThisNodeQuery = "SELECT SCon FROM tblRoom WHERE Roomx=@StartNodex and Roomy=@StartNodey";
					SqliteCommand AboveNodeCMD = new SqliteCommand(ThisNodeQuery,CheckingConnection);
					AboveNodeCMD.Parameters.Add(new SqliteParameter("@StartNodex", x));
					AboveNodeCMD.Parameters.Add(new SqliteParameter("@StartNodey", (y - 1)));
					using(var reader = AboveNodeCMD.ExecuteReader()){
						ConFromAbove = Convert.ToBoolean(reader["SCon"]);
					}
				}
				if ((y + 1) <= 3){
					ThisNodeQuery = "SELECT NCon FROM tblRoom WHERE Roomx=@StartNodex and Roomy=@StartNodey";
					SqliteCommand BelowNodeCMD = new SqliteCommand(ThisNodeQuery,CheckingConnection);
					BelowNodeCMD.Parameters.Add(new SqliteParameter("@StartNodex", x));
					BelowNodeCMD.Parameters.Add(new SqliteParameter("@StartNodey", (y + 1)));
					using(var reader = BelowNodeCMD.ExecuteReader()){
						ConFromBelow = Convert.ToBoolean(reader["NCon"]);
					}	
				}
				if((x + 1) <= 3){
					ThisNodeQuery = "SELECT WCon FROM tblRoom WHERE Roomx=@StartNodex and Roomy=@StartNodey";
					SqliteCommand RightNodeCMD = new SqliteCommand(ThisNodeQuery,CheckingConnection);
					RightNodeCMD.Parameters.Add(new SqliteParameter("@StartNodex", (x + 1)));
					RightNodeCMD.Parameters.Add(new SqliteParameter("@StartNodey", y));
				using(var reader = RightNodeCMD.ExecuteReader()){
					ConFromRight = Convert.ToBoolean(reader["WCon"]);

					}
				}
				if((x - 1 >= 0)){
					ThisNodeQuery = "SELECT ECon FROM tblRoom WHERE Roomx=@StartNodex and Roomy=@StartNodey";
					SqliteCommand LeftNodeCMD = new SqliteCommand(ThisNodeQuery,CheckingConnection);
					LeftNodeCMD.Parameters.Add(new SqliteParameter("@StartNodex", (x - 1)));
					LeftNodeCMD.Parameters.Add(new SqliteParameter("@StartNodey", y));
					using(var reader = LeftNodeCMD.ExecuteReader()){
						ConFromLeft = Convert.ToBoolean(reader["ECon"]);
					}
				}
				if(ConNorth || ConFromAbove){
					print("Connection to the north of node");
					print(x); print(y);
					Vector3 NorthPos = new Vector3(-56.5f + (4.5f * x), 4 - (y * 2), 0);
					Instantiate(Connection);
					Connection.transform.position = NorthPos;
					Connection.transform.rotation = Quaternion.Euler(0,0,90);
				}
				if (ConSouth || ConFromBelow){
					print("Connection to the south of node");
					print(x); print(y);
					Vector3 SouthPos = new Vector3(-56.5f + (4.5f * x), 2 - (y * 2) ,0);
					Instantiate(Connection); 
					Connection.transform.position = SouthPos;
					Connection.transform.rotation = Quaternion.Euler(0,0,90);
				}
				if (ConEast || ConFromRight){
					print("Connection to the East of node");
					print(x); print(y);
					Vector3 EastPos = new Vector3(-54.25f + (x* 4.25f), 3 - (y * 2), 0);
					Instantiate(Connection); 
					Connection.transform.position = EastPos;
					Connection.transform.rotation = Quaternion.Euler(0,0,0);
				}
				if (ConWest || ConFromLeft){
					print("Connection to the West of node");
					print(x); print(y);
					Vector3 WestPos = new Vector3(-58.5f + (x* 4.25f), 3 - (y * 2), 0);
					Instantiate(Connection); 
					Connection.transform.position = WestPos;
					Connection.transform.rotation = Quaternion.Euler(0,0,0);
				}
			}
		}
	}
}
