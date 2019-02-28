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
public class ItemDatabase : MonoBehaviour {
    public List<Item> items = new List<Item>(); // Creates a new list of items, this is all items in the game

    void Awake()
    {
        SqliteConnection WeaponDB = new SqliteConnection("Data Source=Assets\\Plugins\\WeaponsTable.db;Version=3;"); // Connects to database
        WeaponDB.Open();
		string CMDString = "SELECT MAX(id) from tblWeapon"; // Max value in the database
		SqliteCommand CMD = new SqliteCommand(CMDString, WeaponDB);
		int Data = int.Parse(CMD.ExecuteScalar().ToString());
        for (int x = 1; x<=Data; x++){  
            BuildData(x); // Iterates through for every record in the database
        }
    }
    public Item GetItem(int id) // Returns the item with a particular ID
    {
        return items.Find(item=> item.id == id);
    }
    public Item GetItem(string itemName) // returns the item with a particular Name
    {
        return items.Find(item => item.title == itemName);
    }
    void BuildData(int Id){
        SqliteConnection WeaponDB = new SqliteConnection("Data Source=Assets\\Plugins\\WeaponsTable.db;Version=3;"); // Connects to DB
        WeaponDB.Open();
        string CMDString = "select Name, Category, Damage, Magazine, FireRate, Inaccuracy from tblWeapon where id=@Id";
        SqliteCommand CMD = new SqliteCommand(CMDString,WeaponDB);
        CMD.Parameters.AddWithValue("@Id", Id);
        using(var reader = CMD.ExecuteReader()){ // executes the command
            string Name = Convert.ToString(reader["Name"]);
            string Category = Convert.ToString(reader["Category"]);
			int Damage = Convert.ToInt32(reader["Damage"]); // assings local variables to the correct value in the table
			int Magazine = Convert.ToInt32(reader["Magazine"]);
            int FireRate = Convert.ToInt32(reader["FireRate"]);
            float Inaccuracy = Convert.ToSingle(reader["Inaccuracy"]); // float = single?
            items.Add(new Item(Id, Name, Category, new Dictionary<string, int> {{"Damage",Damage},{"Fire Rate",FireRate},{"Magazine",Magazine}}, Inaccuracy)); // adds a new item to the list
        }
        
    }
}
