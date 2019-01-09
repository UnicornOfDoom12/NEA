using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Data;
using Mono.Data.Sqlite;
using System.Text;
using UnityEngine.UI;
using UnityEngine; // imports including sqlite

public class BoxHandler : MonoBehaviour {


	public List<Vector3Int> OpenBoxes = new List<Vector3Int>();
	public WeaponGenerate WeaponGenerate;
	public SpriteRenderer SpriteRender; // declare sprite render component
	public bool BoxColourDecided = false;
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

	public void determinepresence(){
		for (int i = 0; i < OpenBoxes.Count; i++){
			if(OpenBoxes[i].x == CordinateHandler.Cordx && OpenBoxes[i].y == CordinateHandler.Cordy){
				SpriteIndex = OpenBoxes[i].z;
			}
			else{
				SpriteIndex = UnityEngine.Random.Range(0,5);
			}
		}
		int Cordx = CordinateHandler.Cordx;
		int Cordy = CordinateHandler.Cordy;
		SpriteRender = this.gameObject.GetComponent<SpriteRenderer>(); // imports the game object used for the sprite render variable
		Sprite[] SpriteArray = {sprite0,sprite1,sprite2,sprite3,sprite4,sprite5,sprite6,sprite7,sprite8,sprite9,sprite10,sprite11}; // Creates an array of the different sprites to be used
		var RoomDB = new SqliteConnection("Data Source=Assets\\Plugins\\Rooms Table.db;Version=3;"); // defines the connection to database
		RoomDB.Open(); // opens the connection
		string CMDString = "select Box from tblRoom where Roomx=@Cordx and Roomy=@Cordy"; // declares the string used in the qeury.
		SqliteCommand CMD = new SqliteCommand (CMDString, RoomDB); // constructs the query
		CMD.Parameters.AddWithValue("@Cordx", Cordx); // adds paramters to the query
		CMD.Parameters.AddWithValue("@Cordy",Cordy); // adds paramters to the query
		using (var reader = CMD.ExecuteReader()){ // executes the command
			bool Present = Convert.ToBoolean(reader["Box"]); // assigns the value in the database to a local variable
			// TODO change this to be based on difficulty
			SpriteRender.sprite = SpriteArray[SpriteIndex]; // changes the sprite version to the one defined by the random number
			if (Present){ // if there is a box in the room then move the box into view
				
				transform.position = new Vector3(-8f,3f,0);
			}
			else{ // else move it out of the players view
				transform.position = new Vector3(-15,0,0);
			}
		}
		RoomDB.Close(); // close the connection
		GC.Collect();
		GC.WaitForPendingFinalizers();
	}
	public void ChangeImage(){
		Sprite[] SpriteArray = {sprite0,sprite1,sprite2,sprite3,sprite4,sprite5,sprite6,sprite7,sprite8,sprite9,sprite10,sprite11};
		SpriteRender.sprite = SpriteArray[SpriteIndex];
		OpenBoxes.Add(new Vector3Int(CordinateHandler.Cordx,CordinateHandler.Cordy, SpriteIndex));
	}
	


}
