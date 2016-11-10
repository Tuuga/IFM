﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float movementSpeed;

	// public for debug
	public int atRoom;

	public Transform[] grounds;

	bool isMoving;
	IEnumerator move;

	// DEBUG
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			print(MaxYInRoom(atRoom));
		}
	}

	public void SetAtRoom (int room) {
		atRoom = room;
	}

	float MaxYInRoom (int roomIndex) {
		var pos = grounds[roomIndex].position;
		var scale = grounds[roomIndex].lossyScale;
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
		print("Invoking " + method);
		calledFrom.Invoke(method, 0f);
		isMoving = false;
	}

	public bool GetIsMoving () {
		return isMoving;
	}
}
