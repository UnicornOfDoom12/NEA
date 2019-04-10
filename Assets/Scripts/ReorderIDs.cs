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

public class ReorderIDs : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		string path = Application.dataPath;
		path = path + "/Plugins/WeaponsTable.db";
		var WeaponDB = new SqliteConnection("Data Source="+path+";Version=3;"); // define connection to database
		WeaponDB.Open();
		List<int> NewIds = new List<int>();
		string CMDString1 = "SELECT COUNT(*) FROM tblWeapon";
		SqliteCommand CMD1 = new SqliteCommand(CMDString1,WeaponDB);
		int Data = int.Parse(CMD1.ExecuteScalar().ToString());
		for (int i = 1; i <= Data; i++){
			NewIds.Add(i);
		}
		List<int> OldIds = new List<int>();
		string CMDString2 = "SELECT id FROM tblWeapon";
		SqliteCommand CMD2 = new SqliteCommand(CMDString2, WeaponDB);
		SqliteDataReader read = CMD2.ExecuteReader();
		while (read.Read()){
			OldIds.Add(Convert.ToInt32(read["id"]));
		}		
		int x = 1;
		for(int i = 1; i <= Data; i++){
			string CMDString3 = "UPDATE tblWeapon SET id=@newid WHERE id=@i";
			SqliteCommand CMD3 = new SqliteCommand(CMDString3,WeaponDB);
			CMD3.Parameters.AddWithValue("@newid", NewIds[x - 1]);
			CMD3.Parameters.AddWithValue("@i",OldIds[i - 1]);
			CMD3.ExecuteNonQuery();
			x += 1;
		}	
		WeaponDB.Close();
	}
}
