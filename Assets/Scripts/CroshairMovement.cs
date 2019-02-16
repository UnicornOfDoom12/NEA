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
	// Use this for initialization
	void Start () {
		if (Weapon.Category == "Assault Rifle"){
			SpriteRenderer.sprite = AR;
		}
		if (Weapon.Category == "SMG"){
			SpriteRenderer.sprite = SMG;
		}
		if (Weapon.Category == "Hand Cannon"){
			SpriteRenderer.sprite = HC;
		}
		if (Weapon.Category == "Marksman Rifle"){
			SpriteRenderer.sprite = MR;
		}
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Weapon.Category == "Assault Rifle"){
			SpriteRenderer.sprite = AR;
		}
		else if (Weapon.Category == "SMG"){
			SpriteRenderer.sprite = SMG;
		}
		else if (Weapon.Category == "Hand Cannon"){
			SpriteRenderer.sprite = HC;
		}
		else if (Weapon.Category == "Marksman Rifle"){
			SpriteRenderer.sprite = MR;
		}
		mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = Vector2.Lerp(transform.position, mousePos, Speed);
	}
}
