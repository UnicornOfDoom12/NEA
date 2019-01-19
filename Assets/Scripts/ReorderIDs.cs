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
		print("REORDER STARTED");
		SqliteConnection WeaponDB = new SqliteConnection("Data Source=Assets\\Plugins\\WeaponsTable.db;Version=3;");
		WeaponDB.Open();
		List<int> NewIds = new List<int>();
		string CMDString1 = "SELECT COUNT(*) FROM tblWeapon";
		SqliteCommand CMD1 = new SqliteCommand(CMDString1,WeaponDB);
		int Data = int.Parse(CMD1.ExecuteScalar().ToString());
		print("Data = " + Data.ToString());
		for (int i = 1; i <= Data; i++){
			NewIds.Add(i);
		}
		print("Amount of items in newid list = " + NewIds.Count().ToString());
		List<int> OldIds = new List<int>();
		
		string CMDString2 = "SELECT id FROM tblWeapon";
		SqliteCommand CMD2 = new SqliteCommand(CMDString2, WeaponDB);
		SqliteDataReader read = CMD2.ExecuteReader();
		while (read.Read()){
			OldIds.Add(Convert.ToInt32(read["id"]));
		}
		print("Amount of items in the oldid list is " + OldIds.Count().ToString());
		
		
		int x = 1;
		for(int i = 1; i <= Data; i++){
			print("i = " + i.ToString());
			print("x = " + x.ToString());
			string CMDString3 = "UPDATE tblWeapon SET id=@newid WHERE id=@i";
			SqliteCommand CMD3 = new SqliteCommand(CMDString3,WeaponDB);
			print("Parameter values for oldid = " + OldIds[i-1].ToString());
			print("Paramter values for new id = " + NewIds[x-1].ToString());
			CMD3.Parameters.AddWithValue("@newid", NewIds[x - 1]);
			CMD3.Parameters.AddWithValue("@i",OldIds[i - 1]);
			CMD3.ExecuteNonQuery();
			x += 1;
		}	
		print("REORDER FINISHED");
		
		WeaponDB.Close();
	}
	

}
