using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour {

	public GameObject deathUI;
	public Enemy enemy;

	EnemySpawner enemySpawner;

	PlayerMovement pm;
	MouseInput mi;

	bool dead;

	void Start () {
		mi = GetComponent<MouseInput>();
		pm = GetComponent<PlayerMovement>();
		enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
	}
	

	// TODO: SCH version
	public void Die () {
		dead = true;
		mi.DisableControls();
		deathUI.SetActive(true);
		enemy.StopAttack();
		enemySpawner.DespawnEnemy();
	}

	public void Respawn () {
		dead = false;
		mi.EnableControls();
		deathUI.SetActive(false);
		transform.position = pm.GetLastRoom().spawnPoint.position;

		var cameraPoint = pm.GetLastRoom().cameraPoint.position;
		var newCameraPos = new Vector3(cameraPoint.x, cameraPoint.y, Camera.main.transform.position.z);
		Camera.main.transform.position = newCameraPos;
		pm.UpdateAtRoom();
	}

	public bool IsDead () {
		return dead;
	}
}
