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
	public void CheckDataExists () { // Checks if there is data in the table
		
		string path = Application.dataPath;
		path = path + "/Plugins/WeaponsTable.db";
		var WeaponDB = new SqliteConnection("Data Source="+path+";Version=3;"); // define connection to database
		WeaponDB.Open();
		string CMDString = "SELECT COUNT(*) from tblWeapon"; // Counts all values in the database
		SqliteCommand CMD = new SqliteCommand(CMDString, WeaponDB);
		int Data = int.Parse(CMD.ExecuteScalar().ToString());
		if (Data > 0){ // if there is any data
			return; // dont do anything
		}
		else{ // no data
			string InsertQuery = "INSERT INTO tblWeapon (id, Name, Category, Damage, FireRate, Inaccuracy, Magazine)VALUES(@id,@Name,@Category,@Damage,@FireRate,@Inaccuracy,@Magazine)";
			SqliteCommand InsertCommand = new SqliteCommand(InsertQuery, WeaponDB); // Insert a starter weapon into the db
			InsertCommand.Parameters.AddWithValue("@id",1);
			InsertCommand.Parameters.AddWithValue("@Name","Starter"); // basic AR weapon with minimum stats
			InsertCommand.Parameters.AddWithValue("@Category", "AssualtRifle");
			InsertCommand.Parameters.AddWithValue("@Damage", 25);
			InsertCommand.Parameters.AddWithValue("@FireRate", 500);
			InsertCommand.Parameters.AddWithValue("@Inaccuracy", 6.0);
			InsertCommand.Parameters.AddWithValue("@Magazine", 20);
			InsertCommand.ExecuteNonQuery();
			WeaponDB.Close();	
		}
	}
	void Start(){ // run at start
		CheckDataExists(); // checks data exists
	}
}
