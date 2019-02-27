using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFollowMouse : MonoBehaviour {
	void LateUpdate() { // runs after update
        transform.position = Input.mousePosition; // changes the selected items transform to the mouse
	}
}
