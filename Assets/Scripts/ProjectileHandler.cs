using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision Hit)
	{
		if (Hit.gameObject.tag == "Player")
		{
			Physics.IgnoreCollision(Player.collider, collider);
		}
	}
}
