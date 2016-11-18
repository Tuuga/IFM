using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

	public float movementSpeed;
	
	List<Door> goneThrough = new List<Door>();

	Room atRoom;
	Room lastRoom;

	bool isMoving;
	IEnumerator move;

	EnemySpawner spawner;
	public Enemy enemy;
	RoomManager rm;

	Animator anim;

	void Start () {
		spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
		rm = GameObject.Find("RoomManager").GetComponent<RoomManager>();
		anim = GetComponentInChildren<Animator>();
		atRoom = rm.GetRoomIn(transform);
	}

	public Room GetAtRoom () {
		return atRoom;
	}

	public Room GetLastRoom() {
		return lastRoom;
	}

	public List<Door> GetGoneThrough () {
		return goneThrough;
	}

	public void WentThroughDoor (Door d) {
		if (spawner.GetEnemySpawned()) {
			goneThrough.Add(d);
		}
		UpdateAtRoom();
		if (enemy.GetAtRoom() == atRoom) {
			ClearGoneThrough();
		}
	}

	public void UpdateAtRoom () {
		lastRoom = atRoom;
		atRoom = rm.GetRoomIn(transform);
	}

	float MaxYInRoom (Room room) {
		var pos = room.ground.position;
		var scale = room.ground.lossyScale;
		var y = pos.y + scale.y / 2;

		return y;
	}

	public void MovePlayer (Vector3 pos) {
		MovePlayer(pos, null, "");
	}
	public void MovePlayer(Vector3 pos, MonoBehaviour calledFrom, string method) {
		if (GetIsMoving()) {							// If player already moving
			StopCoroutine(move);						// Stop the coroutine
		}
		move = MoveToPos(pos, calledFrom, method);		// Save the coroutine
		StartCoroutine(move);							// Call new coroutine
	}

	public IEnumerator MoveToPos(Vector3 pos, MonoBehaviour calledFrom = null, string method = "") {
		isMoving = true;
		pos.z = 0;
		pos.y = Mathf.Clamp(pos.y, Mathf.NegativeInfinity, MaxYInRoom(atRoom));

		var playerToPos = pos - transform.position;
		
		var dist = playerToPos.magnitude;
		var dir = playerToPos.normalized;


		ResetAnimBools();
		// Checks what direction the player is moving towards
		if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y)) {
			if (dir.x > 0) {
				anim.SetBool("Right", true);
				print("Going right");
			} else {
				anim.SetBool("Left", true);
				print("Going left");
			}
		} else {
			if (dir.y > 0) {
				anim.SetBool("Up", true);
				print("Going up");
			} else {
				anim.SetBool("Down", true);
				print("Going down");
			}
		}

		while (dist > 0) {
			transform.position += dir * movementSpeed * Time.deltaTime;
			dist -= movementSpeed * Time.deltaTime;

			yield return new WaitForEndOfFrame();
		}
		if (calledFrom) {
			calledFrom.Invoke(method, 0f);
		}
		ResetAnimBools();
		isMoving = false;
	}

	void ResetAnimBools () {
		anim.SetBool("Up", false);
		anim.SetBool("Down", false);
		anim.SetBool("Left", false);
		anim.SetBool("Right", false);
	}

	public bool GetIsMoving () {
		return isMoving;
	}

	public void ClearGoneThrough() {
		goneThrough = new List<Door>();
	}
}
