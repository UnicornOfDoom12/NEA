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
	public SelectedDelete SelectedDelete; // inherits the array of selected items
	public Inventory Inventory; // removes the item from the players local inventory
	public AudioClip SoundClip; // sounds to play on click
	public AudioSource SoundSource; // sound source
	public void DeleteFromArray(){ // deletes the items from the array Selected Delete
		foreach (int i in SelectedDelete.ItemsToDelete){
			Inventory.RemoveItem(i); // Removes them from the Inventory data structure
		}
	}
	public void Start(){
		SoundSource.clip = SoundClip; // Sets the sound clip
	}
	public void DeleteFromTable(){ // Deletes items from the array
		string path = Application.dataPath;
		path = path + "/Plugins/WeaponsTable.db";
		var WeaponDB = new SqliteConnection("Data Source="+path+";Version=3;"); // define connection to database
		WeaponDB.Open();
		foreach(int i in SelectedDelete.ItemsToDelete){
			string CMDString = "DELETE FROM tblWeapon WHERE id=@i";
			SqliteCommand CMD = new SqliteCommand(CMDString, WeaponDB);
			CMD.Parameters.AddWithValue("@i" , i); 
			CMD.ExecuteNonQuery();// Deletes all the rows
		}
		List<int> NewIds = new List<int>(); // Reorders the IDs, new IDs = 1-max value
		string CMDString1 = "SELECT COUNT(*) FROM tblWeapon";
		SqliteCommand CMD1 = new SqliteCommand(CMDString1,WeaponDB);
		int Data = int.Parse(CMD1.ExecuteScalar().ToString());
		for (int i = 1; i <= Data; i++){
			NewIds.Add(i);
		}
		List<int> OldIds = new List<int>(); // old Ids = all the current ids
		for (int i = 1; i<= Data; i++){
			string CMDString2 = "SELECT id FROM tblWeapon WHERE id = @i";
			SqliteCommand CMD2 = new SqliteCommand(CMDString2, WeaponDB);
			CMD2.Parameters.AddWithValue("@i", i);
			OldIds.Add(int.Parse(CMD2.ExecuteScalar().ToString()));
			}
		int x = 1;
		foreach(int i in OldIds){
			string CMDString3 = "UPDATE tblWeapon SET id=@newid WHERE id=@i"; // updates oldids = to new ids
			SqliteCommand CMD3 = new SqliteCommand(CMDString3,WeaponDB);
			CMD3.Parameters.AddWithValue("@newid", NewIds[x]);
			CMD3.Parameters.AddWithValue("@i",OldIds[i]);
			x += 1;
		}	
		
		WeaponDB.Close(); // closes connection
	}

	public void DeleteAll(){
		SoundSource.Play();
		DeleteFromArray(); 
		DeleteFromTable();
		SelectedDelete.ItemsToDelete = new List<int> {}; // clears the list
	}

}
