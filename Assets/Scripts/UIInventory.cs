using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour {
    public List<UIItem> uiItems = new List<UIItem>(); // List of classes of type uiItems empty when first run
    public GameObject slotPrefab; // Takes in a slot gameobject
    public Transform slotPanel; // Parent empty slotPanel

    void Awake() // When first start
    {
        Cursor.visible = true; // Ensures we can see the mouse
        for(int i = 0; i < 24; i++) // iterates for 24 slots
        {
            GameObject instance = Instantiate(slotPrefab); // instantiates a slot
            instance.transform.SetParent(slotPanel); // makes the slot a child of slot panel
            uiItems.Add(instance.GetComponentInChildren<UIItem>()); // Adds to the List of UIItems this instance pf the slot.
        }
        
    }

    public void UpdateSlot(int slot, Item item)
    {
        uiItems[slot].UpdateItem(item); // Runs a procedure of the class UiItems
    }

    public void AddNewItem(Item item)
    {
        UpdateSlot(uiItems.FindIndex(i=> i.item == null), item); // Adds a new UI Item
    }

    public void RemoveItem(Item item)
    {
        UpdateSlot(uiItems.FindIndex(i=> i.item == item), null); // Deletes a Ui item
    }
}
