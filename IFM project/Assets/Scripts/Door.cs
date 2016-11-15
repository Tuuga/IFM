using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Door : MonoBehaviour {

	public Transform cameraPos;
	public Transform otherDoor;
	Transform player;
	PlayerMovement mov;
	public bool locked;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		mov = player.GetComponent<PlayerMovement>();
	}

	public void GoThrough () {
		if (locked) {
			print("Door is locked");
			return;
		}

		Vector3 newPos = new Vector3(cameraPos.position.x, cameraPos.position.y, Camera.main.transform.position.z);

		Camera.main.transform.position = newPos;

		var sr = otherDoor.GetComponentInChildren<SpriteRenderer>();			
		var b = sr.bounds;
		player.position =  new Vector2(b.center.x, b.min.y - 0.2f);

		mov.WentThroughDoor(this);
	}

	public void Unlock () {
		locked = false;
	}
}
