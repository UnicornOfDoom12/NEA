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
	void Awake () {
		SqliteConnection WeaponDB = new SqliteConnection("Data Source=Assets\\Plugins\\WeaponsTable.db;Version=3;");
		WeaponDB.Open(); // Opens connection to the DB
		List<int> NewIds = new List<int>(); // Creates the NewIds list
		string CMDString1 = "SELECT COUNT(*) FROM tblWeapon";
		SqliteCommand CMD1 = new SqliteCommand(CMDString1,WeaponDB);
		int Data = int.Parse(CMD1.ExecuteScalar().ToString());
		for (int i = 1; i <= Data; i++){
			NewIds.Add(i); // Creates a list of increasing integers up to the amount of records
		}
		List<int> OldIds = new List<int>(); // creates a list of all the current Ids in ascending order
		string CMDString2 = "SELECT id FROM tblWeapon";
		SqliteCommand CMD2 = new SqliteCommand(CMDString2, WeaponDB);
		SqliteDataReader read = CMD2.ExecuteReader();
		while (read.Read()){
			OldIds.Add(Convert.ToInt32(read["id"])); // Populates the list with the Ids
		}		
		int x = 1;
		for(int i = 1; i <= Data; i++){
			string CMDString3 = "UPDATE tblWeapon SET id=@newid WHERE id=@i";
			SqliteCommand CMD3 = new SqliteCommand(CMDString3,WeaponDB);
			CMD3.Parameters.AddWithValue("@newid", NewIds[x - 1]);
			CMD3.Parameters.AddWithValue("@i",OldIds[i - 1]); // This loop replaces OldIDs with new Ids
			CMD3.ExecuteNonQuery();
			x += 1;
		}	
		WeaponDB.Close(); // Closes connection
	}
}
