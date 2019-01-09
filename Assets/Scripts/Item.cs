using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    public int id;
    public string title;
    public string Category;
    public Sprite icon;
    public Dictionary<string, int> stats = new Dictionary<string, int>();

    public Item(int id, string title, string Category, Dictionary<string, int> stats)
    {
        this.id = id;
        this.title = title;
        this.Category = Category;
        int random = UnityEngine.Random.Range(0,3);
        string randomstr = "_" + random.ToString();
        this.icon = Resources.Load<Sprite>("Sprites/Items/" + Category);
        this.stats = stats;
    }

    public Item(Item item)
    {
        this.id = item.id;
        this.title = item.title;
        this.Category = item.Category;
        int random = UnityEngine.Random.Range(0,3);
        string randomstr = "_" + random.ToString();
        this.icon = Resources.Load<Sprite>("Sprites/Items/" + item.Category);
        this.stats = item.stats;
    }
}
