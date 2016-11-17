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
	MouseInput mI;

	bool attacking;

	void Start () {
		playerHiding = player.GetComponent<Hiding>();
		mov = player.GetComponent<PlayerMovement>();
		mI = player.GetComponent<MouseInput>();
		scheduler = SchedulerUtility.scheduler;
		rm = GameObject.Find("RoomManager").GetComponent<RoomManager>();
		atRoom = rm.GetRoomIn(transform);
	}

	void Update() {
		if (!attacking) return;
		
		// WIP
		if (mov.GetAtRoom() == atRoom) {																	// Player and monster in the same room
			if (playerHiding.IsHidden()) {																	// Player is hidden
				print("Player is hidden");
			} else {																						// Player is not hidden
				transform.position += DirTo(player.transform.position) * movementSpeed * Time.deltaTime;
			}
		} else {                                                                                            // Player and monster in different rooms
			var doorPos = mI.GetWalkTarget(mov.GetGoneThrough()[0].gameObject);
			if (Vector3.Distance(transform.position, doorPos) > 0.5f) {										// Monster not at the door
				transform.position += DirTo(doorPos) * movementSpeed * Time.deltaTime;
			} else {																						// Monster at the door
				transform.position = mI.GetWalkTarget(mov.GetGoneThrough()[0].otherDoor.gameObject);
				atRoom = rm.GetRoomIn(transform);
				if (mov.GetAtRoom() == atRoom) {
					mov.ClearGoneThrough();
				}
			}
		}
	}

	Vector3 DirTo(Vector3 vec) {
		var dir = vec - transform.position;
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

	public void StopAttack () {
		attacking = false;
	}

	public void AttackSCH () {
		gameObject.SetActive(true);
		scheduler = SchedulerUtility.scheduler;
		scheduler.InvokeLater(this, "Attack", 0f);
	}

	public void StopAttackSCH () {
		scheduler.InvokeLater(this, "StopAttack", 0f);
	}
}
