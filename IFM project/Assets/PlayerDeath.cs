using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour {

	public GameObject deathUI;
	public Enemy enemy;

	EnemySpawner enemySpawner;

	MouseInput mi;

	bool dead;

	void Start () {
		mi = GetComponent<MouseInput>();

	}
	
	public void Die () {
		dead = true;
		mi.DisableControls();
		deathUI.SetActive(true);
		enemy.StopAttack();

	}

	public void Respawn () {
		dead = false;
		mi.EnableControls();
		deathUI.SetActive(false);

	}

	public bool IsDead () {
		return dead;
	}
}
