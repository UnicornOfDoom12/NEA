using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {
	public AudioSource SoundSource;
	public AudioClip SoundClip;
	void Start () {
		SoundSource.clip = SoundClip;
	}
	// Update is called once per frame
	public void Play(){
		SoundSource.clip = SoundClip;
		SoundSource.Play();
	}
}
