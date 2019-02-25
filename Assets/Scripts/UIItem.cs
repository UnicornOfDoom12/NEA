using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {
    public Item item; // the data values of the item itself
    private Image spriteImage; // the image to be displayed when this sprite is around
    private UIItem selectedItem; // The values of the selected item
    private Tooltip tooltip; // the tool tip for this particular item
    public SelectedEquip SelectedEquip; // Script to determine if this item is selected, also contains a item which is selected
    public SelectedDelete SelectedDelete; // script that determines if this item is to be deleted, contains a list of items to delete
    public AudioClip SoundClip; // sound to be played when selected
    public AudioSource SoundSource; // soundsouce to be played from.
    int SpriteID; // sprite ID used for validating
    string SpriteName; // name to be displayed
    bool PointerOn = false; // will be true when the user is hovering over this item
    void Awake() // awake command
    {
        selectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>(); // retreives the values for everything it needs.
        tooltip = GameObject.Find("Tooltip").GetComponent<Tooltip>();
        spriteImage = GetComponent<Image>();
        SelectedEquip = GameObject.Find("SelectedEquip").GetComponent<SelectedEquip>();
        SelectedDelete = GameObject.Find("SelectedDelete").GetComponent<SelectedDelete>();
        SoundSource = GameObject.Find("SelectSound").GetComponent<AudioSource>();
        SoundSource.clip = SoundClip;
        UpdateItem(null); // updates the item to be empty at the start
    }
    void Update(){
        if (SelectedEquip.EquippedId != SpriteID && this.item != null){
            if (!SelectedDelete.ItemsToDelete.Contains(SpriteID)){
                spriteImage.color = Color.white; // if the item is not selected, and is not null set colour to white (no change)
            }
            
        }
        if (Input.GetKeyDown(KeyCode.E)){ // user presses E
            if(PointerOn){ // if user has mouse over this item
                print("He did the thing on a " + SpriteName);
                if(spriteImage.color != Color.white){
                    spriteImage.color = Color.white; // if the item is already selected and the user presses E, un select it
                }
                else{
                    spriteImage.color = Color.green;
                    SelectedEquip.EquippedId = SpriteID; // else select it
                    SoundSource.Play();
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.D)){ // user presses D
            if(PointerOn){ // mouse hovering over the item
                if(spriteImage.color != Color.white){
                    spriteImage.color = Color.white; // If it is already selected to delete or equip, un select it.
                    if(SelectedDelete.ItemsToDelete.Contains(SpriteID)){
                        SelectedDelete.ItemsToDelete.Remove(SpriteID); // Then if it is in the list of items to delete, remove it from that list
                    }
                }
                else{
                    spriteImage.color = Color.red; // else set the highlight colour to red
                    SelectedDelete.ItemsToDelete.Add(SpriteID); // add it to the list
                }
            }
        }

    }
    public void UpdateItem(Item item) // run at the start.
    {
        this.item = item;
        if (this.item != null) // if the item exists
        {
            spriteImage.color = Color.white; // display the item
            spriteImage.sprite = item.icon; // display the icon
            SpriteID = item.id; // set the values of name and ID
            SpriteName = item.title;
        }
        else
        {
            spriteImage.color = Color.clear; // if there shouldnt be a item here, make the item transparent
        }
    }

    public void OnPointerDown(PointerEventData eventData) // if the item is clicked
    {
        if (this.item != null)
        {
            if (selectedItem.item != null)
            {
                Item clone = new Item(selectedItem.item); // Clone the itme
                selectedItem.UpdateItem(this.item);
                UpdateItem(clone); // display the clone
            }
            else
            {
                selectedItem.UpdateItem(this.item); // dont clone it
                UpdateItem(null);
            }
        }
        else if (selectedItem.item != null)
        {
            UpdateItem(selectedItem.item);
            selectedItem.UpdateItem(null);
        }
    }

    public void OnPointerEnter(PointerEventData eventData) // user is hovering over the item
    {
       if (this.item != null)
       {
            tooltip.GenerateTooltip(this.item); // display the tooltip
            PointerOn = true; // set this to true
       }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false); // remove the tool tip
        PointerOn = false; // set this to false.
    }
}
