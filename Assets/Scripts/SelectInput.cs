using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SelectInput : MonoBehaviour {

	public EventSystem eventSystem; // Used to detect user inputs
	public GameObject selectedObject; // A gameobject that is selected by the user
	private bool buttonSelected = false;
	void Start () {
		Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetAxisRaw("Vertical") != 0 && !buttonSelected){ // If the user has clicked on a button in the menu
			eventSystem.SetSelectedGameObject(selectedObject);
			buttonSelected = true; // Selects the button 
		}
	}
	private void Disabled(){
		buttonSelected = false; // If the menu is disabled, disabled the button
	}
}
