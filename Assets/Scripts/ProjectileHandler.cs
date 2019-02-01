using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour {

	public GameObject Player;
	
	void Start(){
		Player = GameObject.Find("Player");
	}

	public void OnCollisionEnter2D(Collision2D Hit)
	{
		print("Determining weather to ignore");
		if (Hit.gameObject.tag == "Player")
		{
			print("Ignoring collision");
			Physics.IgnoreCollision(Player.GetComponent<Collider>(), GetComponent<Collider>());
		}
	}
}
