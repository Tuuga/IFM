using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemy;

	bool enemySpawned;

	Scheduler scheduler;
	Transform spawnPos;

	void Start () {
		scheduler = GameObject.Find("Scheduler").GetComponent<Scheduler>();
	}

	public void SpawnEnemyAt () {
		enemySpawned = true;
		enemy.SetActive(true);
		enemy.transform.position = spawnPos.position;
	}

	public void DespawnEnemy () {
		enemySpawned = false;
		enemy.SetActive(false);
	}

	public void SpawnAtSCH (Transform spawnPos) {
		this.spawnPos = spawnPos;
		scheduler.InvokeLater(this, "SpawnEnemyAt", 1f);
	}
	public void DespawnSCH () {
		scheduler.InvokeLater(this, "DespawnEnemy", 1f);
	}

	public bool GetEnemySpawned () {
		return enemySpawned;
	}
}
