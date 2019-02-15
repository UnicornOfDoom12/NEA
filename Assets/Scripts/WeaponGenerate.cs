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

public class WeaponGenerate : MonoBehaviour {

	// Use this for initialization
	public BoxHandler BoxHandler;
	public CordinateHandler CordinateHandler;
	public SpriteRenderer SpriteRenderer;
	public Sprite Closed;
	public Sprite Open;
	public bool Opened;

	void Start(){
		BoxHandler = GameObject.Find("BoxHandler").GetComponent<BoxHandler>();
		CordinateHandler = GameObject.Find("Cordinate Tracker").GetComponent<CordinateHandler>();
		SpriteRenderer.sprite = Closed;
		Opened = false;
	}
	public void GenerateAndInsert(){
		print("Starting insertion");
		string[] Categories = new string[] { "Assault Rifle", "Marksman Rifle", "SMG","Hand Cannon"};
		string[] Names = new string[] {"MK", "M","MP","AR","AK","AN","QBZ"};
		SqliteConnection WeaponDB = new SqliteConnection("Data Source=Assets\\Plugins\\WeaponsTable.db;Version=3;");
		WeaponDB.Open();
		string CMDString = "SELECT MAX(id) from tblWeapon";
		SqliteCommand CMD = new SqliteCommand(CMDString, WeaponDB);
		int Data = int.Parse(CMD.ExecuteScalar().ToString());
		WeaponDB.Close();
		int id = Data + 1; // To be inserted
		string NamePt1 = Names[UnityEngine.Random.Range(0,6)];
		string NamePt2 = UnityEngine.Random.Range(1,99).ToString();
		string Category = Categories[UnityEngine.Random.Range(0,3)]; // To be inserted as category
		string FullName = NamePt1 + NamePt2 + "-" + Category; // To be inserted as name
		if (Category == "Assault Rifle"){
			GenerateAR(id, FullName, Category);
			
		}
		if (Category == "Marksman Rifle"){
			GenerateMR(id, FullName, Category);
			
		}
		if (Category == "SMG"){
			GenerateSMG(id, FullName, Category);
			
		}
		if (Category == "Hand Cannon"){
			GenerateHC(id, FullName, Category);
		}

	}
	public void OpenBox(){
		if (!Opened){
			SpriteRenderer.sprite = Open;
			using (SqliteConnection RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;")){
				RoomDB.Open();
				string RemovalString = "UPDATE tblRoom SET Box=@f WHERE Roomx=@x AND Roomy=@y";
				using(SqliteCommand CMD = new SqliteCommand(RemovalString,RoomDB)){
					CMD.Parameters.AddWithValue("@f", false);
					CMD.Parameters.AddWithValue("@x",CordinateHandler.Cordx);
					CMD.Parameters.AddWithValue("@y",CordinateHandler.Cordy);
					CMD.ExecuteNonQuery();
					RoomDB.Close();
					GenerateAndInsert();
				}

			}

		}
	}


	public void GenerateAR(int id, string name, string category){
		int Damage = UnityEngine.Random.Range(24,49);
		int FireRate = UnityEngine.Random.Range(500,700);
		float inaccuracy = UnityEngine.Random.Range(3.0f,6.0f);
		int Magazine = UnityEngine.Random.Range(20,40);
		//Insert(id, name, category, Damage, FireRate, inaccuracy, Magazine);
	}
	public void GenerateMR(int id, string name, string category){
		int Damage = UnityEngine.Random.Range(40,70);
		int FireRate = UnityEngine.Random.Range(300,500);
		float inaccuracy = UnityEngine.Random.Range(1.0f,3.0f);
		int Magazine = UnityEngine.Random.Range(5,30);
		//Insert(id, name, category, Damage, FireRate, inaccuracy, Magazine);
	}
	public void GenerateSMG(int id, string name, string category){
		int Damage = UnityEngine.Random.Range(15,25);
		int FireRate = UnityEngine.Random.Range(700,1000);
		float inaccuracy = UnityEngine.Random.Range(4.0f,8.0f);
		int Magazine = UnityEngine.Random.Range(30,60);
		//Insert(id, name, category, Damage, FireRate, inaccuracy, Magazine);
	}
	public void GenerateHC(int id, string name, string category){
		int Damage = UnityEngine.Random.Range(60,99);
		int FireRate = UnityEngine.Random.Range(300,400);
		float inaccuracy = UnityEngine.Random.Range(6.0f,9.0f);
		int Magazine = UnityEngine.Random.Range(3,10);
		//Insert(id, name, category, Damage, FireRate, inaccuracy, Magazine);
	}
	public void Insert(int id, string name, string category, int Damage, int FireRate, float inaccuracy, int Magazine){
		print(id);
		SqliteConnection WeaponDB = new SqliteConnection("Data Source=Assets\\Plugins\\WeaponsTable.db;Version=3;");
		WeaponDB.Open();
		string InsertQuery = "INSERT INTO tblWeapon (id, Name, Category, Damage, FireRate, Inaccuracy, Magazine)VALUES(@id,@Name,@Category,@Damage,@FireRate,@Inaccuracy,@Magazine)";
		SqliteCommand InsertCommand = new SqliteCommand(InsertQuery, WeaponDB);
		InsertCommand.Parameters.AddWithValue("@id",id);
		InsertCommand.Parameters.AddWithValue("@Name",name);
		InsertCommand.Parameters.AddWithValue("@Category", category);
		InsertCommand.Parameters.AddWithValue("@Damage", Damage);
		InsertCommand.Parameters.AddWithValue("@FireRate", FireRate);
		InsertCommand.Parameters.AddWithValue("@Inaccuracy", inaccuracy);
		InsertCommand.Parameters.AddWithValue("@Magazine", Magazine);
		InsertCommand.ExecuteNonQuery();
		WeaponDB.Close();
		Opened = true;
		
		print("Insertion Finished");

	}

}
