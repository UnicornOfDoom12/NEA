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
	public Weapon EquippedWeapon;
	public PlayerMovement PlayerMovement;
	public SelectedEquip SelectedEquip;
	void Start(){
		animator = GetComponent<Animator>();
		WeaponId = SelectedEquip.EquippedId;
		print("The weapon I have is " + WeaponId.ToString());
		EquipWeapon();
	}
	public Weapon(int WeaponId, string Category, int Damage, int Magazine, int FireRate, float Inaccuracy){
		this.WeaponId = WeaponId;
		this.Category = Category;
		this.Damage = Damage;
		this.Magazine = Magazine;
		this.FireRate = FireRate;
		this.Inaccuracy = Inaccuracy;
	}
	public Weapon(Weapon weapon){
		this.WeaponId = weapon.WeaponId;
		this.Category = weapon.Category;
		this.Damage = weapon.Damage;
		this.Magazine = weapon.Magazine;
		this.FireRate = weapon.FireRate;
		this.Inaccuracy = weapon.Inaccuracy;
	}
	public void EquipWeapon(){
		SqliteConnection WeaponDB = new SqliteConnection("Data Source=Assets\\Plugins\\WeaponsTable.db;Version=3;");
		WeaponDB.Open();
		string CMDString = "SELECT Category, Damage, Inaccuracy, Magazine, FireRate FROM tblWeapon WHERE id=@ID";
		SqliteCommand CMD = new SqliteCommand(CMDString,WeaponDB);
		if (SelectedEquip.EquippedId < 1){ // Temporary
			SelectedEquip.EquippedId = 1;
		}
		CMD.Parameters.AddWithValue("@id", SelectedEquip.EquippedId);
		var reader = CMD.ExecuteReader();
		Category = Convert.ToString(reader["Category"]);
		Damage = Convert.ToInt32(reader["Damage"]);
		Inaccuracy = Convert.ToInt32(reader["Inaccuracy"]);
		Magazine = Convert.ToInt32(reader["Magazine"]);
		FireRate = Convert.ToInt32(reader["FireRate"]);
		reader.Close();
		WeaponDB.Close();
		Weapon EquippedWeapon = new Weapon(SelectedEquip.EquippedId, Category, Damage, Magazine, FireRate, Inaccuracy);
		print(EquippedWeapon.Category);
		print(EquippedWeapon.Damage);
		print(EquippedWeapon.Magazine);

		PlayerMovement.ChangeImageValue(EquippedWeapon.Category);

	}
}
