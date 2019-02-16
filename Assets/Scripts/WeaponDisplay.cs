using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour {

	// Use this for initialization
	public WeaponGenerate WeaponGenerate;
	public Text ItemsGained;
	void Start () {
		string gained = WeaponGenerate.Display(true);
		ItemsGained.text = gained;
	}
}
