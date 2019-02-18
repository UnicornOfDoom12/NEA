using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class ChangeVolume : MonoBehaviour {
	public Slider Slider;
	public void AdjustVolume(){
		AudioListener.volume = Slider.value;
	}
}
