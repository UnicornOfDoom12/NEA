using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CroshairMovement : MonoBehaviour {
	private Vector3 mousePos;
    public float Speed = 0.1f;
	public Sprite AR;
	public Sprite MR;
	public Sprite HC;
	public Sprite SMG;
	public Weapon Weapon;
	public SpriteRenderer SpriteRenderer;
	void Start () { // Run at start
		if (Weapon.Category == "Assault Rifle"){ // if they have an Assualt rifle change the sprite to AR
			SpriteRenderer.sprite = AR; 
		}
		if (Weapon.Category == "SMG"){// if they have an SMG change the sprite to SMG
			SpriteRenderer.sprite = SMG;
		}
		if (Weapon.Category == "Hand Cannon"){// if they have an Hand Cannon change the sprite to HC
			SpriteRenderer.sprite = HC;
		}
		if (Weapon.Category == "Marksman Rifle"){// if they have an Marksman Rifle change the sprite to MR
			SpriteRenderer.sprite = MR;
		}
		Cursor.visible = false; // Makes the mouse invisible
	}
	void Update () {
		mousePos = Input.mousePosition; // Gets the Mouse position
        mousePos = Camera.main.ScreenToWorldPoint(mousePos); // Convert it to a world space
        transform.position = Vector2.Lerp(transform.position, mousePos, Speed); // Moves the crosshair to the correct position, with purposefull lag
	}
}
