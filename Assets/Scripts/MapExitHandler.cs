using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Data;
using Mono.Data.Sqlite;
using System.Text;
using UnityEngine.UI;
using UnityEngine; // imports including sqlite

public class MapExitHandler : MonoBehaviour {

	// Use this for initialization


	public void DrawObject () {
		string path = Application.dataPath;
		path = path + "/Plugins/Rooms Table.db";
		var RoomDB = new SqliteConnection("Data Source="+path+";Version=3;"); // define connection to database
		RoomDB.Open();
		string CMDString;
		SqliteCommand CMD;
		for(int x = 0; x <= 3; x++){
			for(int y = 0; y<=3; y++){
				CMDString = "select End from tblRoom where Roomx=@x and Roomy=@y";
				CMD = new SqliteCommand(CMDString,RoomDB);
				CMD.Parameters.AddWithValue("@x",x);
				CMD.Parameters.AddWithValue("@y",y);
				using (var reader = CMD.ExecuteReader()){ // executes the query
					bool Present = Convert.ToBoolean(reader["End"]);
					if (Present){
						float posx = -50 + -6.5f + (4 * x);
						float posy = 3.0f - (2 * y);
						Vector3 TransformVector = new Vector3(posx, posy, 0);
						gameObject.transform.position = TransformVector;
					}
				}
			}
		}
		RoomDB.Close();
		RoomDB.Dispose();
	}
}
