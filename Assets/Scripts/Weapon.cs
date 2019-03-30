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
	Animator animator;
	public int WeaponId;
	public string Category;
	public int Damage;
	public int Magazine;
	public int FireRate;
    public float Inaccuracy;
	public PlayerMovement PlayerMovement;
	public SelectedEquip SelectedEquip;
	void Start(){
		animator = GetComponent<Animator>();
		WeaponId = SelectedEquip.EquippedId;
		EquipWeapon();
	}
	public void EquipWeapon(){
		SqliteConnection WeaponDB = new SqliteConnection("Data Source=Assets\\Plugins\\WeaponsTable.db;Version=3;");
		WeaponDB.Open();
		string CMDString = "SELECT Category, Damage, Inaccuracy, Magazine, FireRate FROM tblWeapon WHERE id=@ID";
		SqliteCommand CMD = new SqliteCommand(CMDString,WeaponDB);
		CMD.Parameters.AddWithValue("@id", SelectedEquip.EquippedId);
		var reader = CMD.ExecuteReader();
		Category = Convert.ToString(reader["Category"]);
		Damage = Convert.ToInt32(reader["Damage"]);
		Inaccuracy = Convert.ToInt32(reader["Inaccuracy"]);
		Magazine = Convert.ToInt32(reader["Magazine"]);
		FireRate = Convert.ToInt32(reader["FireRate"]);
		reader.Close();
		WeaponDB.Close();
		PlayerMovement.ChangeImageValue(Category);
	}
}
