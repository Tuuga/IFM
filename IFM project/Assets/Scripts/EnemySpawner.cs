using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemy;

	bool enemySpawned;

	// WIP
	public void SpawnEnemyAt(Transform spawnPos) {
		print("Enemy Spawned");
		enemySpawned = true;
		enemy.SetActive(true);
		enemy.transform.position = spawnPos.position;
	}

	public void DespawnEnemy () {
		print("Enemy Despawned");
		enemySpawned = false;
		enemy.SetActive(false);
	}

	public bool GetEnemySpawned () {
		return enemySpawned;
	}
}
