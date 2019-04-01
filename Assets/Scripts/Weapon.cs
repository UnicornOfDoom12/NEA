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

public class Weapon : MonoBehaviour {
	public int WeaponId; 
	public string Category;
	public int Damage;
	public int Magazine;
	public int FireRate;
    public float Inaccuracy;
	public PlayerMovement PlayerMovement;
	public SelectedEquip SelectedEquip;
	void Start(){ // run at the start
		WeaponId = SelectedEquip.EquippedId; // Determines the ID of the selected weapon
		EquipWeapon(); // runs the EquipWeapon function
	}
	public void EquipWeapon(){
		SqliteConnection WeaponDB = new SqliteConnection("Data Source=Assets\\Plugins\\WeaponsTable.db;Version=3;"); // Connects to DB
		WeaponDB.Open(); // Opens the DB
		string CMDString = "SELECT Category, Damage, Inaccuracy, Magazine, FireRate FROM tblWeapon WHERE id=@ID"; // Selects all the attributes of the weapons
		SqliteCommand CMD = new SqliteCommand(CMDString,WeaponDB);
		CMD.Parameters.AddWithValue("@id", SelectedEquip.EquippedId);
		var reader = CMD.ExecuteReader(); //executes the query 
		Category = Convert.ToString(reader["Category"]); // Converts the arributes to their correct type
		Damage = Convert.ToInt32(reader["Damage"]);
		Inaccuracy = Convert.ToInt32(reader["Inaccuracy"]);
		Magazine = Convert.ToInt32(reader["Magazine"]);
		FireRate = Convert.ToInt32(reader["FireRate"]);
		reader.Close();
		WeaponDB.Close();
		PlayerMovement.ChangeImageValue(Category); // Changes the sprite value
	}
}
