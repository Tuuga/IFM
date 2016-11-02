using UnityEngine;
using System.Collections;

public class MouseInput : MonoBehaviour {

	PlayerMovement mov;
	Inventory inv;

	IEnumerator move;

	void Start () {
		mov = GetComponent<PlayerMovement>();
		inv = GetComponent<Inventory>();
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {

			Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D[] col = Physics2D.OverlapPointAll(mouseWorldPos);

			foreach (Collider2D c in col) {
				if (c.tag == "Ground") {
					if (mov.GetIsMoving()) {                // If player already moving
						StopCoroutine(move);                // Stop the coroutine
					}
					move = mov.MoveToPos(mouseWorldPos);	// Save the coroutine
					StartCoroutine(move);					// Call new coroutine
				} else if (c.tag == "Door") {
					// Do door things
					StopCoroutine(move);
					c.GetComponent<Door>().ActivateDoor();
				} else if (c.tag == "Item") {
					inv.AddToInventory(c.GetComponent<Item>());
				}
			}
		}
	}
}
