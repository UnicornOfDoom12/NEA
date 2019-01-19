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
		List<int> NewIds = new List<int>();
		string CMDString1 = "SELECT COUNT(*) FROM tblWeapon";
		SqliteCommand CMD1 = new SqliteCommand(CMDString1,WeaponDB);
		int Data = int.Parse(CMD1.ExecuteScalar().ToString());
		for (int i = 1; i <= Data; i++){
			NewIds.Add(i);
		}
		List<int> OldIds = new List<int>();
		for (int i = 1; i<= Data; i++){
			string CMDString2 = "SELECT id FROM tblWeapon WHERE id = @i";
			SqliteCommand CMD2 = new SqliteCommand(CMDString2, WeaponDB);
			CMD2.Parameters.AddWithValue("@i", i);
			OldIds.Add(int.Parse(CMD2.ExecuteScalar().ToString()));
			}
		int x = 1;
		foreach(int i in OldIds){
			string CMDString3 = "UPDATE tblWeapon SET id=@newid WHERE id=@i";
			SqliteCommand CMD3 = new SqliteCommand(CMDString3,WeaponDB);
			CMD3.Parameters.AddWithValue("@newid", NewIds[x]);
			CMD3.Parameters.AddWithValue("@i",OldIds[i]);
			x += 1;
		}	
		
		WeaponDB.Close();
	}

	public void DeleteAll(){
		DeleteFromArray();
		DeleteFromTable();
	}

}
