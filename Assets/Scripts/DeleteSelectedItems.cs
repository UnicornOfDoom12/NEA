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



public class DeleteSelectedItems : MonoBehaviour {
	public SelectedDelete SelectedDelete;
	public Inventory Inventory;
	public void DeleteFromArray(){
		foreach (int i in SelectedDelete.ItemsToDelete){
			Inventory.RemoveItem(i);
		}
	}
	public void DeleteFromTable(){
		SqliteConnection WeaponDB = new SqliteConnection("Data Source=Assets\\Plugins\\WeaponsTable.db;Version=3;");
		WeaponDB.Open();
		foreach(int i in SelectedDelete.ItemsToDelete){
			string CMDString = "DELETE FROM tblWeapon WHERE id=@i";
			SqliteCommand CMD = new SqliteCommand(CMDString, WeaponDB);
			CMD.Parameters.AddWithValue("@i" , i);
			CMD.ExecuteNonQuery();
		}
		WeaponDB.Close();
	}

	public void DeleteAll(){
		DeleteFromArray();
		DeleteFromTable();
	}

}
