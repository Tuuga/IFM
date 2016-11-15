using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	public float movementSpeed;

	// Public for debug
	Door goneThrough;

	Room atRoom;

	public Transform[] grounds;

	bool isMoving;
	IEnumerator move;

	EnemySpawner spawner;
	RoomManager rm;

	void Start () {
		spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
		rm = GameObject.Find("RoomManager").GetComponent<RoomManager>();
		atRoom = rm.GetRoomIn(transform);
	}

	public Room GetAtRoom () {
		return atRoom;
	}

	public Door GetGoneThrough () {
		return goneThrough;
	}

	public void WentThroughDoor (Door d) {
		if (spawner.GetEnemySpawned()) {
			goneThrough = d;
		}
		atRoom = rm.GetRoomIn(transform);
	}

	float MaxYInRoom (Room room) {
		var pos = room.ground.position;
		var scale = room.ground.lossyScale;
		var y = pos.y + scale.y / 2;

		return y;
	}

	public void MovePlayer (Vector3 pos) {
		if (GetIsMoving()) {							// If player already moving
			StopCoroutine(move);						// Stop the coroutine
		}
		move = MoveToPos(pos);							// Save the coroutine
		StartCoroutine(move);							// Call new coroutine
	}

	public void MovePlayer(Vector3 pos, string method, MonoBehaviour calledFrom) {
		if (GetIsMoving()) {							// If player already moving
			StopCoroutine(move);						// Stop the coroutine
		}
		move = MoveToPos(pos, method, calledFrom);		// Save the coroutine
		StartCoroutine(move);							// Call new coroutine
	}

	public IEnumerator MoveToPos (Vector3 pos) {
		isMoving = true;

		pos.z = 0;

		pos.y = Mathf.Clamp(pos.y, Mathf.NegativeInfinity, MaxYInRoom(atRoom));

		var playerToPos = pos - transform.position;
		var dist = playerToPos.magnitude;
		var dir = playerToPos.normalized;
		
		while (dist > 0) {
			transform.position += dir * movementSpeed * Time.deltaTime;
			dist -= movementSpeed * Time.deltaTime;

			yield return new WaitForEndOfFrame();
		}
		isMoving = false;
	}

	public IEnumerator MoveToPos(Vector3 pos, string method, MonoBehaviour calledFrom) {
		isMoving = true;

		pos.z = 0;

		pos.y = Mathf.Clamp(pos.y, Mathf.NegativeInfinity, MaxYInRoom(atRoom));

		var playerToPos = pos - transform.position;
		
		var dist = playerToPos.magnitude;
		var dir = playerToPos.normalized;

		while (dist > 0) {
			transform.position += dir * movementSpeed * Time.deltaTime;
			dist -= movementSpeed * Time.deltaTime;

			yield return new WaitForEndOfFrame();
		}
		calledFrom.Invoke(method, 0f);
		isMoving = false;
	}

	public bool GetIsMoving () {
		return isMoving;
	}
}
