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

public class Inventory : MonoBehaviour {
    public List<Item> characterItems = new List<Item>(); // List of items in the game
    public ItemDatabase itemDatabase; // The item database object
    public UIInventory inventoryUI; // the inventory Panel

    void Start()
    {
        string path = Application.dataPath;
		path = path + "/Plugins/WeaponsTable.db";
		var WeaponDB = new SqliteConnection("Data Source="+path+";Version=3;"); // define connection to database
        WeaponDB.Open();
		string CMDString = "SELECT MAX(id) from tblWeapon";
		SqliteCommand CMD = new SqliteCommand(CMDString, WeaponDB);
		int Data = int.Parse(CMD.ExecuteScalar().ToString()); // Gets the max value from the db
        int y = 30;
        if (Data <= y){
            y = Data; // If there are less than 30 items in the db give them all if it
        }
        else{
            y = 29;// Gives the player the first 30 items in the database
        }
        for(int i = 1; i <= y; i++){
            GiveItem(i);  // gives them the itemw

        }
    }
    public void GiveItem(int id)
    {
        Item itemToAdd = itemDatabase.GetItem(id);
        characterItems.Add(itemToAdd); // adds to the player inventory
        inventoryUI.AddNewItem(itemToAdd); // Adds and item from the characterItems db to the UI, gives them for an int
    }

    public void GiveItem(string itemName)
    {
        Item itemToAdd = itemDatabase.GetItem(itemName);
        characterItems.Add(itemToAdd); // adds to the players inventory
        inventoryUI.AddNewItem(itemToAdd); // Adds the string version of the item to the UI
    }

    public Item CheckForItem(int id)
    {
        return characterItems.Find(item => item.id == id); // Returns the the item if the player has it.
    }
    public void RemoveItem(int id) // removes and item from the UI
    {  
        print("In remove item");
        Item itemToRemove = CheckForItem(id); // Checks for the item first
        if (itemToRemove != null)
        {
            print("item found");
            characterItems.Remove(itemToRemove); // if the items there remove it from players inventory
            inventoryUI.RemoveItem(itemToRemove); // and from the UI
        }
    }
}
