using UnityEngine;
using System.Collections;

public class MouseInput : MonoBehaviour {

	public GameObject actionMenu;

	PlayerMovement mov;
	Inventory inv;

	GameObject currentActionItem;

	IEnumerator move;

	void Start () {
		mov = GetComponent<PlayerMovement>();
		inv = GetComponent<Inventory>();
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {

			var col = GetColliderUnderMouse();

			foreach (Collider2D c in col) {
				currentActionItem = null;
				if (c.tag == "Ground") {
					if (mov.GetIsMoving()) {                // If player already moving
						StopCoroutine(move);                // Stop the coroutine
					}
					var mouseWrld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					move = mov.MoveToPos(mouseWrld);		// Save the coroutine
					StartCoroutine(move);					// Call new coroutine
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.Mouse1)) {
			OpenActionMenu(currentActionItem);
		}
	}

	void OpenActionMenu (GameObject g) {
		if (g == null) { return; }
		actionMenu.SetActive(true);
		// Change position to cursor							
		actionMenu.transform.position = Input.mousePosition + new Vector3(0, 50, 0);
		// Set active item to g
	}

	public void PickUp () {

	}

	public void LookAt () {
		
	}

	public void Use () {

	}

	public void CloseActionMenu() {
		actionMenu.SetActive(false);
		// Set active item to null
	}

	Collider2D[] GetColliderUnderMouse () {
		return Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));
	}
}
