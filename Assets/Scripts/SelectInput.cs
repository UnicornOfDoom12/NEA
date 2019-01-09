﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SelectInput : MonoBehaviour {

	// Use this for initialization
	public EventSystem eventSystem;
	public GameObject selectedObject;
	private bool buttonSelected = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetAxisRaw("Vertical") != 0 && !buttonSelected){
			eventSystem.SetSelectedGameObject(selectedObject);
			buttonSelected = true;
		}
	}
	private void Disabled(){
		buttonSelected = false;
	}
}
