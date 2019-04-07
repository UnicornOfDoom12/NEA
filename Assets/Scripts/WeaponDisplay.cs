using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour {
	public WeaponGenerate WeaponGenerate;
	public Text ItemsGained;
	void Start () { // run at start
		string gained = WeaponGenerate.Display(true); // Runs display with true parameter, which returns a string
		ItemsGained.text = gained; // changes the display text to show the correct thing
		WeaponGenerate.IDs.Clear(); // clear the list of Items
		WeaponGenerate.Names.Clear();
		WeaponGenerate.WeaponCategories.Clear();
		WeaponGenerate.Damages.Clear();
		WeaponGenerate.FireRates.Clear();
		WeaponGenerate.Inaccuracies.Clear();
		WeaponGenerate.Magazines.Clear();
	}
}
