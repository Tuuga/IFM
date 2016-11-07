using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Door : MonoBehaviour {

	public Transform cameraPos;
	public Transform playerPos;

	public Transform player;

	// WIP
	public void ActivateDoor () {
		Vector3 newPos = new Vector3(cameraPos.position.x, cameraPos.position.y, Camera.main.transform.position.z);

		Camera.main.transform.position = newPos;
		player.position = playerPos.position;
	}
}
