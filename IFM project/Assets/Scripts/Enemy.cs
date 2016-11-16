using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	public GameObject player;
	public float movementSpeed;

	Hiding playerHiding;
	PlayerMovement mov;
	RoomManager rm;
	Scheduler scheduler;
	Room atRoom;

	bool attacking;

	void Start () {
		playerHiding = player.GetComponent<Hiding>();
		mov = player.GetComponent<PlayerMovement>();
		rm = GameObject.Find("RoomManager").GetComponent<RoomManager>();
		atRoom = rm.GetRoomIn(transform);
		//scheduler = GameObject.Find("Scheduler").GetComponent<Scheduler>();
	}

	void Update() {
		if (!attacking) return;
		
		// WIP
		if (mov.GetAtRoom() == atRoom) {																	// Player and monster in the same room
			if (playerHiding.IsHidden()) {																	// Player is hidden
				print("Player is hidden");
			} else {																						// Player is not hidden
				transform.position += DirTo(player.transform) * movementSpeed * Time.deltaTime;
			}
		} else {																							// Player and monster in different rooms
			if (Vector3.Distance(transform.position, mov.GetGoneThrough()[0].transform.position) > 0.5f) {	// Monster not at the door
				transform.position += DirTo(mov.GetGoneThrough()[0].transform) * movementSpeed * Time.deltaTime;
			} else {																						// Monster at the door
				transform.position = mov.GetGoneThrough()[0].otherDoor.position;
				atRoom = rm.GetRoomIn(transform);
				if (mov.GetAtRoom() == atRoom) {
					mov.ClearGoneThrough();
				}
			}
		}
	}

	Vector3 DirTo(Transform t) {
		var dir = t.position - transform.position;
		dir.z = 0;
		dir.Normalize();
		return dir;
	}

	public Room GetAtRoom() {
		return atRoom;
	}

	public void Attack () {
		attacking = true;
	}

	public void AttackSCH () {
		gameObject.SetActive(true);
		scheduler = GameObject.Find("Scheduler").GetComponent<Scheduler>();
		scheduler.InvokeLater(this, "Attack", 0f);
	}
}
