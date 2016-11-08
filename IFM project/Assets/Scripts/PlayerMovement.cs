using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float movementSpeed;

	bool isMoving;
	IEnumerator move;

	MouseInput mouse;

	void Start () {
		mouse = GetComponent<MouseInput>();
	}

	public void MoveToMousePos () {
		if (GetIsMoving()) {							// If player already moving
			StopCoroutine(move);						// Stop the coroutine
		}
		move = MoveToPos(mouse.GetMouseWorldPos());		// Save the coroutine
		StartCoroutine(move);							// Call new coroutine
	}

	public IEnumerator MoveToPos (Vector3 pos) {
		isMoving = true;

		pos.z = 0;

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

	public bool GetIsMoving () {
		return isMoving;
	}
}
