using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Door : MonoBehaviour {

	public Transform cameraPos;
	public Transform otherDoor;
	public int roomNumber;
	Transform player;
	PlayerMovement mov;
	public bool locked;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		mov = player.GetComponent<PlayerMovement>();
	}

	// WIP
	public void GoThrough () {
		if (locked) {
			print("Door is locked");
			return;
		}

		Vector3 newPos = new Vector3(cameraPos.position.x, cameraPos.position.y, Camera.main.transform.position.z);

		Camera.main.transform.position = newPos;
		player.position = otherDoor.position;
		// WIP
		mov.WentThroughDoor(this, otherDoor.GetComponent<Door>().roomNumber);
	}

	public void Unlock () {
		locked = false;
	}
}
