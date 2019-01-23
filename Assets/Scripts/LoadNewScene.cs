using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour {
	public AudioSource SoundSource;
	public AudioClip SoundClip;
	// Use this for initialization
	void Start () {
		SoundSource.clip = SoundClip;
	}
	
	public void LoadSceneByIndex (int scene)
	{
		SoundSource.Play();
		WaitForTime(0.5f);
		SceneManager.LoadScene(scene);
	}
	IEnumerator WaitForTime(float time)
    {
        yield return new WaitForSeconds(time);
    }
}

