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
    public List<Item> items = new List<Item>();

    void Awake()
    {
        SqliteConnection WeaponDB = new SqliteConnection("Data Source=Assets\\Plugins\\WeaponsTable.db;Version=3;");
        WeaponDB.Open();
		string CMDString = "SELECT MAX(id) from tblWeapon";
		SqliteCommand CMD = new SqliteCommand(CMDString, WeaponDB);
		int Data = int.Parse(CMD.ExecuteScalar().ToString());

        for (int x = 1; x<=Data; x++){
            
            BuildData2(x);
        }

        
    }

    public Item GetItem(int id)
    {
        return items.Find(item=> item.id == id);
    }

    public Item GetItem(string itemName)
    {
        return items.Find(item => item.title == itemName);
    }

    void BuildDatabase()
    {
        items = new List<Item>() {
            new Item(0, "Starter", "Assualt Rifle",
            new Dictionary<string, int> {
                { "Damage", 25 },
                { "Fire Rate", 500 }
            }, 6f),
            new Item(1, "Big gun", "SMG",
            new Dictionary<string, int> {
                { "Value", 300 }
            },6f),
            new Item(2, "Small gun", "Marksman Rifle",
            new Dictionary<string, int> {
                { "Power", 5 },
                { "Mining", 20}
            },6f),
            new Item(3, "Smaller gun", "Hand Cannon",
            new Dictionary<string, int> {
                { "Power", 5 },
                { "Mining", 20}
            },6f)
        };
    }
    void BuildData2(int Id){
        SqliteConnection WeaponDB = new SqliteConnection("Data Source=Assets\\Plugins\\WeaponsTable.db;Version=3;");
        WeaponDB.Open();
        string CMDString = "select Name, Category, Damage, Magazine, FireRate, Inaccuracy from tblWeapon where id=@Id";
        SqliteCommand CMD = new SqliteCommand(CMDString,WeaponDB);
        CMD.Parameters.AddWithValue("@Id", Id);
        using(var reader = CMD.ExecuteReader()){ // executes the comman
           // print(Id);
            string Name = Convert.ToString(reader["Name"]);
           // print("Name = " + Name);
            string Category = Convert.ToString(reader["Category"]);
			int Damage = Convert.ToInt32(reader["Damage"]); // assings local variables to the correct value in the table
            //print(Damage.GetType().Name);
			int Magazine = Convert.ToInt32(reader["Magazine"]);
            int FireRate = Convert.ToInt32(reader["FireRate"]);
            float Inaccuracy = Convert.ToSingle(reader["Inaccuracy"]); // float = single?
            items.Add(new Item(Id, Name, Category, new Dictionary<string, int> {{"Damage",Damage},{"Fire Rate",FireRate},{"Magazine",Magazine}}, Inaccuracy));
        }
        
    }
}
