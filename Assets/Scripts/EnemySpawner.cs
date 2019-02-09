using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public GameObject Turret;
	// add more enemy types here
	public List<GameObject> Enemies = new List<GameObject>();
	void Start () {
		
	}
	public void SpawnEnemies(int AmountOfEnemies){
		for (int i = 1; i <= AmountOfEnemies; i++){
			// choose enemy type randomly
			print("There are " + i.ToString());
			Vector3 pos = transform.position;
			pos.x = UnityEngine.Random.Range(0.0f,8.0f);
			pos.y = UnityEngine.Random.Range(-3.5f,3.5f);
			transform.position = pos;
			print("The position for the " + i.ToString() +" is " + transform.position.ToString());
			GameObject NewTurret = Instantiate(Turret,transform.position,Quaternion.Euler(new Vector3(0,0,0)));
			Enemies.Add(NewTurret);
		}
	}
	public void DeleteEnemies(){
		foreach (GameObject i in Enemies){
			Destroy(i);
		}
	}
}
