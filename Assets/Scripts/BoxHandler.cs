using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Data;
using Mono.Data.Sqlite;
using System.Text;
using UnityEngine.UI;
using UnityEngine; // imports including sqlite

public class BoxHandler : MonoBehaviour {


	public GameObject Chest;
	//public SpriteRenderer SpriteRender; // declare sprite render component
	public Sprite sprite0; // the following variables are all different version of the chest sprite
	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	public Sprite sprite4;
	public Sprite sprite5;
	public Sprite sprite6;
	public Sprite sprite7;
	public Sprite sprite8;
	public Sprite sprite9;
	public Sprite sprite10;
	public Sprite sprite11;
	public int SpriteIndex;
	public CordinateHandler CordinateHandler;
	public List<Sprite> SpriteArray;
	public List<GameObject> BoxArray;
	public List<Vector2Int> OpenBoxes;
	public WeaponGenerate WeaponGenerate;
	public void determinepresence(){
		print("Determine Presence");
		int Cordx = CordinateHandler.Cordx;
		int Cordy = CordinateHandler.Cordy;
		//SpriteRender = this.gameObject.GetComponent<SpriteRenderer>(); // imports the game object used for the sprite render variable
		SpriteArray = new List<Sprite>{sprite0,sprite1,sprite2,sprite3,sprite4,sprite5,sprite6,sprite7,sprite8,sprite9,sprite10,sprite11}; // Creates an array of the different sprites to be used
		string path = Application.dataPath;
		path = path + "/Plugins/Rooms Table.db";
		var RoomDB = new SqliteConnection("Data Source="+path+";Version=3;"); // define connection to database
		RoomDB.Open(); // opens the connection
		string CMDString = "select Box from tblRoom where Roomx=@Cordx and Roomy=@Cordy"; // declares the string used in the qeury.
		SqliteCommand CMD = new SqliteCommand (CMDString, RoomDB); // constructs the query
		CMD.Parameters.AddWithValue("@Cordx", Cordx); // adds paramters to the query
		CMD.Parameters.AddWithValue("@Cordy",Cordy); // adds paramters to the query
		using (var reader = CMD.ExecuteReader()){ // executes the command
			bool Present = Convert.ToBoolean(reader["Box"]); // assigns the value in the database to a local variable
			// TODO change this to be based on difficulty
			// changes the sprite version to the one defined by the random number
			Vector2Int TempBoxVector = new Vector2Int(Cordx,Cordy);
			if (Present){ // if there is a box in the room then move the box into view
				GameObject TempChest = Instantiate(Chest,transform.position,transform.rotation);
				SpriteIndex = UnityEngine.Random.Range(0,5);
				WeaponGenerate SpriteImage = TempChest.GetComponent<WeaponGenerate>();
				SpriteImage.Closed = SpriteArray[SpriteIndex];
				SpriteImage.Open = SpriteArray[SpriteIndex + 6];
				print("Box Here");
				BoxArray.Add(TempChest);
			}
		}
		RoomDB.Close(); // closes the connection properly
		RoomDB.Dispose();
		GC.Collect();
		GC.WaitForPendingFinalizers();	
	}
	public void DeleteBoxes(){
		foreach (GameObject i in BoxArray){
			Destroy(i);
		}
	}
}
