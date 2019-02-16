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
	public AudioSource SoundSource;
	public AudioClip OpenClip;
	public static List<int> IDs = new List<int>{};
	public static List<string> Names = new List<string>{};
	public static List<string> WeaponCategories = new List<string>{};
	public static List<int> Damages = new List<int>{};
	public static List<int> FireRates = new List<int>{};
	public static List<int> Magazines = new List<int>{};
	public static List<float> Inaccuracies = new List<float>{};
	public int MaxValue;
	public PlayerDeathHandler PlayerDeathHandler;
	void Start(){
		if(gameObject.name != "WeaponDisplay"){
			BoxHandler = GameObject.Find("BoxHandler").GetComponent<BoxHandler>();
			CordinateHandler = GameObject.Find("Cordinate Tracker").GetComponent<CordinateHandler>();
			SpriteRenderer.sprite = Closed;
			Opened = false;
			PlayerDeathHandler = GameObject.Find("Player").GetComponent<PlayerDeathHandler>();
			if (BoxHandler.OpenBoxes.Contains(new Vector2Int(CordinateHandler.Cordx,CordinateHandler.Cordy))){
				Opened = true;
				SpriteRenderer.sprite = Open;
			}
			SqliteConnection WeaponDB = new SqliteConnection("Data Source=Assets\\Plugins\\WeaponsTable.db;Version=3;");
			WeaponDB.Open();
			string CMDString = "SELECT MAX(id) from tblWeapon";
			SqliteCommand CMD = new SqliteCommand(CMDString, WeaponDB);
			MaxValue = int.Parse(CMD.ExecuteScalar().ToString());
			WeaponDB.Close();
			MaxValue += IDs.Count();
		}
	}
	public void GenerateAndInsert(){
		print("Starting insertion");
		string[] Categories = new string[] { "Assault Rifle", "Hand Cannon", "SMG","Marksman Rifle"};
		string[] Names = new string[] {"MK", "M","MP","AR","AK","AN","QBZ"};
		MaxValue += 1;
		int id = MaxValue; // To be inserted
		string NamePt1 = Names[UnityEngine.Random.Range(0,6)];
		string NamePt2 = UnityEngine.Random.Range(1,99).ToString();
		string Category = Categories[UnityEngine.Random.Range(0,3)]; // To be inserted as category
		string FullName = NamePt1 + NamePt2 + "-" + Category; // To be inserted as name
		if (Category == "Assault Rifle"){
			print("Genning a AR because of " + Category);
			GenerateAR(id, FullName, Category);
			
		}
		else if (Category == "Marksman Rifle"){
			print("Genning a MR because of " + Category);
			GenerateMR(id, FullName, Category);
			
		}
		else if (Category == "SMG"){
			print("Genning a SMG because of " + Category);
			GenerateSMG(id, FullName, Category);
			
		}
		else if (Category == "Hand Cannon"){
			print("Genning a HC because of " + Category);
			GenerateHC(id, FullName, Category);
		}

	}
	public void OpenBox(){
		if (!Opened){
			SpriteRenderer.sprite = Open;
			GenerateAndInsert();
			Opened = true;
			BoxHandler.OpenBoxes.Add(new Vector2Int(CordinateHandler.Cordx, CordinateHandler.Cordy));
			PlayerDeathHandler.RemoveDamage(15);
		}
	}


	public void GenerateAR(int id, string name, string category){
		int Damage = UnityEngine.Random.Range(24,49);
		int FireRate = UnityEngine.Random.Range(500,700);
		float inaccuracy = UnityEngine.Random.Range(3.0f,6.0f);
		int Magazine = UnityEngine.Random.Range(20,40);
		Insert(id, name, category, Damage, FireRate, inaccuracy, Magazine);
	}
	public void GenerateMR(int id, string name, string category){
		int Damage = UnityEngine.Random.Range(40,70);
		int FireRate = UnityEngine.Random.Range(300,500);
		float inaccuracy = UnityEngine.Random.Range(1.0f,3.0f);
		int Magazine = UnityEngine.Random.Range(5,30);
		Insert(id, name, category, Damage, FireRate, inaccuracy, Magazine);
	}
	public void GenerateSMG(int id, string name, string category){
		int Damage = UnityEngine.Random.Range(15,25);
		int FireRate = UnityEngine.Random.Range(700,1000);
		float inaccuracy = UnityEngine.Random.Range(4.0f,8.0f);
		int Magazine = UnityEngine.Random.Range(30,60);
		Insert(id, name, category, Damage, FireRate, inaccuracy, Magazine);
	}
	public void GenerateHC(int id, string name, string category){
		int Damage = UnityEngine.Random.Range(60,99);
		int FireRate = UnityEngine.Random.Range(300,400);
		float inaccuracy = UnityEngine.Random.Range(6.0f,9.0f);
		int Magazine = UnityEngine.Random.Range(3,10);
		Insert(id, name, category, Damage, FireRate, inaccuracy, Magazine);
	}
	public void Insert(int id, string name, string category, int Damage, int FireRate, float inaccuracy, int Magazine){
		print(id);
		IDs.Add(id);
		Names.Add(name);
		WeaponCategories.Add(category);
		Damages.Add(Damage);
		FireRates.Add(FireRate);
		Inaccuracies.Add(inaccuracy);
		Magazines.Add(Magazine);
		Opened = true;
		print("Insertion Finished");
	}
	public string Display(bool Won){
		string itemString;
		if (Won){
			itemString = "The items you gained: ";
		}
		else{
			itemString = "The items you lost: ";
		}
		foreach (string i in Names){
			itemString += i;
			itemString += ", "; 
		}
		if (Won){
			Insert();
		}
		return itemString;
	}
	public void Insert(){
		for (int i = 0; i < IDs.Count(); i++){
			using(SqliteConnection WeaponDB = new SqliteConnection("Data Source=Assets\\Plugins\\WeaponsTable.db;Version=3;")){
				WeaponDB.Open();
				string InsetString = "INSERT INTO tblWeapon (id,name,Category,Damage,Inaccuracy,Magazine,FireRate)VALUES(@id, @name, @category, @damage, @inaccuracy, @magazine, @firerate)";
				using (SqliteCommand CMD = new SqliteCommand(InsetString,WeaponDB)){
					print("Added with the id " + IDs[i].ToString());
					print("Added the " + Names[i]);
					print("=============");
					CMD.Parameters.AddWithValue("@id",IDs[i]);
					CMD.Parameters.AddWithValue("@name",Names[i]);
					CMD.Parameters.AddWithValue("@category",WeaponCategories[i]);
					CMD.Parameters.AddWithValue("@damage",Damages[i]);
					CMD.Parameters.AddWithValue("@inaccuracy",Inaccuracies[i]);
					CMD.Parameters.AddWithValue("@magazine",Magazines[i]);
					CMD.Parameters.AddWithValue("@firerate",FireRates[i]);
					CMD.ExecuteNonQuery();
					WeaponDB.Close();

				}
			}
		}
	}

}
