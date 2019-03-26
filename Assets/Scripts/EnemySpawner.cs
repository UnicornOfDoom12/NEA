using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public GameObject Turret;
	public GameObject Monster;
	public List<GameObject> Enemies = new List<GameObject>();
	public List<GameObject> EnemyTypes = new List<GameObject>();
	public DifficultyScoreTracker DifficultyScoreTracker;
	void Start () {
		EnemyTypes.Add(Turret); // adds the two types of enemies to a public list
		EnemyTypes.Add(Monster);
	}
	public void SpawnEnemies(int AmountOfEnemies){ // Amount of enemies is passed in, and determined in coordinate handler
		for (int i = 1; i <= AmountOfEnemies; i++){ // for every enemy in the room
			Vector3 pos = transform.position;
			pos.x = UnityEngine.Random.Range(0.0f,8.0f);
			pos.y = UnityEngine.Random.Range(-3.5f,3.5f); // changes this objects position to a random one in the room
			transform.position = pos;
			GameObject EnemyToSpawn = EnemyTypes[Random.Range(0, EnemyTypes.Count )]; // randomly select and enemy from the list of enemies
			GameObject NewEnemy = Instantiate(EnemyToSpawn,transform.position,Quaternion.Euler(new Vector3(0,0,0))); // Spawns the enemy
			Enemies.Add(NewEnemy); // adds the new object to an array
			if(NewEnemy.tag == "Enemy"){
				TurretHandler HealthChanger = NewEnemy.GetComponent<TurretHandler>(); // gets the handler that controls turret AI
				float TempHealth= HealthChanger.health * DifficultyScoreTracker.FinalScore; // changes the health by the difficulty score
				HealthChanger.health = (int)TempHealth; // converts to int, as difficulty score might be a float
			}
			else{
				MonsterHandler HealthChanger = NewEnemy.GetComponent<MonsterHandler>(); // gets the handler that controls monster AI
				float TempHealth = HealthChanger.Health * DifficultyScoreTracker.FinalScore; // changes the health by the difficulty score
				HealthChanger.Health = (int)TempHealth; // converts to int as difficulty score might be a float
			}
			
		}
	}
	public void DeleteEnemies(){
		foreach (GameObject i in Enemies){ // for all the enemies in the list
			Destroy(i); // delete them
		}
	}
}
