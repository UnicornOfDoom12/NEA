using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquipper : MonoBehaviour {

	// Use this for initialization
	public int WeaponId;
	public SelectedEquip SelectedEquip;
	void Start(){
		WeaponId = SelectedEquip.EquippedId;
		print("The weapon I have is " + WeaponId.ToString());
	}
}
