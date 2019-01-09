using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour {

	
	public void LoadSceneByIndex (int scene)
	{
		SceneManager.LoadScene(scene);
	}
}

