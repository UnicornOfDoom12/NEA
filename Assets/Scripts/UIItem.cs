using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {
    public Item item;
    private Image spriteImage;
    private UIItem selectedItem;
    private Tooltip tooltip;
    public SelectedEquip SelectedEquip;
    public SelectedDelete SelectedDelete;
    public AudioClip SoundClip;
    public AudioSource SoundSource;
    int SpriteID;
    string SpriteName;
    bool PointerOn = false;
    void Awake()
    {
        selectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();
        tooltip = GameObject.Find("Tooltip").GetComponent<Tooltip>();
        spriteImage = GetComponent<Image>();
        SelectedEquip = GameObject.Find("SelectedEquip").GetComponent<SelectedEquip>();
        SelectedDelete = GameObject.Find("SelectedDelete").GetComponent<SelectedDelete>();
        SoundSource = GameObject.Find("SelectSound").GetComponent<AudioSource>();
        SoundSource.clip = SoundClip;
        UpdateItem(null);
    }
    void Update(){
        if (SelectedEquip.EquippedId != SpriteID && this.item != null){
            if (!SelectedDelete.ItemsToDelete.Contains(SpriteID)){
                spriteImage.color = Color.white;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.E)){
            if(PointerOn){
                print("He did the thing on a " + SpriteName);
                if(spriteImage.color != Color.white){
                    spriteImage.color = Color.white;
                }
                else{
                    spriteImage.color = Color.green;
                    SelectedEquip.EquippedId = SpriteID;
                    SoundSource.Play();
                }
                
                
                print("The item selected is" + SelectedEquip.EquippedId);
            }
        }
        if(Input.GetKeyDown(KeyCode.D)){
            if(PointerOn){
                print("Selected item to delete" + SpriteName);
                if(spriteImage.color != Color.white){
                    spriteImage.color = Color.white;
                    if(SelectedDelete.ItemsToDelete.Contains(SpriteID)){
                        SelectedDelete.ItemsToDelete.Remove(SpriteID);
                        print(SelectedDelete.ItemsToDelete.Count);
                    }
                }
                else{
                    spriteImage.color = Color.red;
                    SelectedDelete.ItemsToDelete.Add(SpriteID);
                    print(SelectedDelete.ItemsToDelete.Count);
                }
            }
        }

    }
    public void UpdateItem(Item item)
    {
        this.item = item;
        if (this.item != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = item.icon;
            SpriteID = item.id;
            SpriteName = item.title;
        }
        else
        {
            spriteImage.color = Color.clear;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (this.item != null)
        {
            print("Item clicked");
            if (selectedItem.item != null)
            {
                Item clone = new Item(selectedItem.item);
                selectedItem.UpdateItem(this.item);
                UpdateItem(clone);
                print("Item selected " + SpriteName);
            }
            else
            {
                selectedItem.UpdateItem(this.item);
                UpdateItem(null);
            }
        }
        else if (selectedItem.item != null)
        {
            UpdateItem(selectedItem.item);
            selectedItem.UpdateItem(null);
            
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       if (this.item != null)
       {
            tooltip.GenerateTooltip(this.item);
            print("Hovering over " + SpriteName);
            PointerOn = true;
       }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false);
        PointerOn = false;
    }
}
