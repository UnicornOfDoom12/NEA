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

public class WeaponDBHandler : MonoBehaviour {
	public bool CanContinue = false;
	// Use this for initialization
	public void CheckDataExists () {
		
		SqliteConnection WeaponDB = new SqliteConnection("Data Source=Assets\\Plugins\\WeaponsTable.db;Version=3;");
		WeaponDB.Open();
		string CMDString = "SELECT COUNT(*) from tblWeapon";
		SqliteCommand CMD = new SqliteCommand(CMDString, WeaponDB);
		int Data = int.Parse(CMD.ExecuteScalar().ToString());
		if (Data > 0){
			print("Table Exists");
				
		}

		else{

			print("Table does not exist");
			string InsertQuery = "INSERT INTO tblWeapon (id, Name, Category, Damage, FireRate, Inaccuracy, Magazine)VALUES(@id,@Name,@Category,@Damage,@FireRate,@Inaccuracy,@Magazine)";
			SqliteCommand InsertCommand = new SqliteCommand(InsertQuery, WeaponDB);
			InsertCommand.Parameters.AddWithValue("@id",1);
			InsertCommand.Parameters.AddWithValue("@Name","Starter");
			InsertCommand.Parameters.AddWithValue("@Category", "AssualtRifle");
			InsertCommand.Parameters.AddWithValue("@Damage", 25);
			InsertCommand.Parameters.AddWithValue("@FireRate", 500);
			InsertCommand.Parameters.AddWithValue("@Inaccuracy", 6.0);
			InsertCommand.Parameters.AddWithValue("@Magazine", 20);
			InsertCommand.ExecuteNonQuery();
			WeaponDB.Close();
			print("Table now has data");
				
		}

	}

	
	void Start(){
		CheckDataExists();
	}
	
}
