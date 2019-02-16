using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponLost : MonoBehaviour {
	// Use this for initialization
	public WeaponGenerate WeaponGenerate;
	public Text ItemsLost;
	void Start () {
		string Lost = WeaponGenerate.Display(false);
		ItemsLost.text = Lost;
	}
}
