﻿using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviour {

	public LayerMask mask;

	void Start () {
		
	}

	public Room GetRoomIn (Transform obj) {
		var cols = Physics2D.OverlapPointAll(obj.position, mask);
		Room roomIn = null;
		
		foreach (Collider2D c in cols) {
			if (c.GetComponent<Room>()) {
				roomIn = c.GetComponent<Room>();
			}
		}

		print(obj + " is in " + roomIn);
		return roomIn;	
	}
}
