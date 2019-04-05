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
	public DifficultyScoreTracker DifficultyScoreTracker;
	void Start(){ // run at the start
		if(gameObject.name != "WeaponDisplay"){ // If this isnt on the weapon display object
			BoxHandler = GameObject.Find("BoxHandler").GetComponent<BoxHandler>(); // find the boxhander
			CordinateHandler = GameObject.Find("Cordinate Tracker").GetComponent<CordinateHandler>(); // find cordinate tracker
			SpriteRenderer.sprite = Closed; // Set the box to be closed
			Opened = false; // open = false at the start
			PlayerDeathHandler = GameObject.Find("Player").GetComponent<PlayerDeathHandler>(); // get the deathhandler object
			if (BoxHandler.OpenBoxes.Contains(new Vector2Int(CordinateHandler.Cordx,CordinateHandler.Cordy))){ // if this box was already opened then:
				Opened = true; //set the box to be opened
				SpriteRenderer.sprite = Open; // change the sprite to be open
			}
			SqliteConnection WeaponDB = new SqliteConnection("Data Source=Assets\\Plugins\\WeaponsTable.db;Version=3;"); // WeaponDB connection
			WeaponDB.Open();
			string CMDString = "SELECT MAX(id) from tblWeapon"; // select the max id
			SqliteCommand CMD = new SqliteCommand(CMDString, WeaponDB);
			MaxValue = int.Parse(CMD.ExecuteScalar().ToString());
			WeaponDB.Close();
			MaxValue += IDs.Count(); // add one to it, for the ids
			DifficultyScoreTracker = GameObject.Find("DifficultScoreTracker").GetComponent<DifficultyScoreTracker>(); // get the difficulty score
		}
	}
	public void GenerateAndInsert(){
		string[] Categories = new string[] { "Assault Rifle", "Hand Cannon", "SMG","Marksman Rifle"}; // defines list of categories
		string[] Names = new string[] {"MK", "M","MP","AR","AK","AN","QBZ"}; // defines list of possible names
		MaxValue += 1; // adds one to the max value
		int id = MaxValue; // To be inserted
		string NamePt1 = Names[UnityEngine.Random.Range(0,6)]; // generates the first part of the name
		string NamePt2 = UnityEngine.Random.Range(1,99).ToString(); // generates second part
		string Category = Categories[UnityEngine.Random.Range(0,3)]; // To be inserted as category
		string FullName = NamePt1 + NamePt2 + "-" + Category; // To be inserted as name
		if (Category == "Assault Rifle"){
			
			GenerateAR(id, FullName, Category); // if the categroy is AR generate values for an AR
			
		}
		else if (Category == "Marksman Rifle"){
			
			GenerateMR(id, FullName, Category);// if the categroy is MR generate values for an MR
			
		}
		else if (Category == "SMG"){
			
			GenerateSMG(id, FullName, Category);// if the categroy is SMG generate values for an SMG
			
		}
		else if (Category == "Hand Cannon"){
			
			GenerateHC(id, FullName, Category);// if the categroy is HC generate values for an HC
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
		float TempDamage = Damage * DifficultyScoreTracker.FinalScore;
		Damage = (int)TempDamage;
		int FireRate = UnityEngine.Random.Range(500,700);
		float inaccuracy = UnityEngine.Random.Range(3.0f,6.0f);
		int Magazine = UnityEngine.Random.Range(20,40);
		Insert(id, name, category, Damage, FireRate, inaccuracy, Magazine);
	}
	public void GenerateMR(int id, string name, string category){
		int Damage = UnityEngine.Random.Range(40,70);
		float TempDamage = Damage * DifficultyScoreTracker.FinalScore;
		Damage = (int)TempDamage;
		int FireRate = UnityEngine.Random.Range(300,500);
		float inaccuracy = UnityEngine.Random.Range(1.0f,3.0f);
		int Magazine = UnityEngine.Random.Range(5,30);
		Insert(id, name, category, Damage, FireRate, inaccuracy, Magazine);
	}
	public void GenerateSMG(int id, string name, string category){
		int Damage = UnityEngine.Random.Range(15,25);
		float TempDamage = Damage * DifficultyScoreTracker.FinalScore;
		Damage = (int)TempDamage;
		int FireRate = UnityEngine.Random.Range(700,1000);
		float inaccuracy = UnityEngine.Random.Range(4.0f,8.0f);
		int Magazine = UnityEngine.Random.Range(30,60);
		Insert(id, name, category, Damage, FireRate, inaccuracy, Magazine);
	}
	public void GenerateHC(int id, string name, string category){
		int Damage = UnityEngine.Random.Range(60,99);
		float TempDamage = Damage * DifficultyScoreTracker.FinalScore;
		Damage = (int)TempDamage;
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
			InsertToDB();
		}
		return itemString;
	}
	public void InsertToDB(){
		for (int i = 0; i < IDs.Count(); i++){
			using(SqliteConnection WeaponDB = new SqliteConnection("Data Source=Assets\\Plugins\\WeaponsTable.db;Version=3;")){
				WeaponDB.Open();
				string InsetString = "INSERT INTO tblWeapon (id,name,Category,Damage,Inaccuracy,Magazine,FireRate)VALUES(@id, @name, @category, @damage, @inaccuracy, @magazine, @firerate)";
				using (SqliteCommand CMD = new SqliteCommand(InsetString,WeaponDB)){
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
