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
		EnemyTypes.Add(Turret);
		EnemyTypes.Add(Monster);
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
			GameObject EnemyToSpawn = EnemyTypes[Random.Range(0, EnemyTypes.Count )];
			GameObject NewTurret = Instantiate(EnemyToSpawn,transform.position,Quaternion.Euler(new Vector3(0,0,0)));
			if(EnemyToSpawn == EnemyTypes[0]){
				TurretHandler HealthChanger = NewTurret.GetComponent<TurretHandler>();
				float TempHealth= HealthChanger.health * DifficultyScoreTracker.FinalScore;
				HealthChanger.health = (int)TempHealth;
			}
			else{
				MonsterHandler HealthChanger = NewTurret.GetComponent<MonsterHandler>();
				float TempHealth = HealthChanger.Health * DifficultyScoreTracker.FinalScore;
				HealthChanger.Health = (int)TempHealth;
			}
			Enemies.Add(NewTurret);
		}
	}
	public void DeleteEnemies(){
		foreach (GameObject i in Enemies){
			Destroy(i);
		}
	}
}
