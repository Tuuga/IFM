using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject player;
	public float movementSpeed;

	Hiding playerHiding;
	PlayerMovement mov;

	int atRoom;

	void Start () {
		playerHiding = player.GetComponent<Hiding>();
		mov = player.GetComponent<PlayerMovement>();
	}

	void Update () {
		if (mov.GetAtRoom() == atRoom) {
			transform.position += GetDirToPlayer() * movementSpeed * Time.deltaTime;
		}
	}

	Vector3 GetDirToPlayer () {
		var dir = player.transform.position - transform.position;
		dir.z = 0;
		dir.Normalize();
		return dir;
	}

	public void SetAtRoom (int roomInt) {
		atRoom = roomInt;
	}
}
