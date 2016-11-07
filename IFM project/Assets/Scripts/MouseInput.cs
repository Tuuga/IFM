using UnityEngine;
using System.Collections;

public class MouseInput : MonoBehaviour {

	public GameObject actionMenu;
	public LayerMask mask;

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

			bool openMenu = false;
			foreach (Collider2D c in col) {
				currentActionItem = null;
				if (c.tag == "Ground") {
					if (mov.GetIsMoving()) {					// If player already moving
						StopCoroutine(move);					// Stop the coroutine
					}
					move = mov.MoveToPos(GetMouseWorldPos());	// Save the coroutine
					StartCoroutine(move);						// Call new coroutine
				} else if (c.tag == "Item") {
					currentActionItem = c.gameObject;
					openMenu = true;
				}
			}
			if (openMenu && !actionMenu.activeSelf) {
				OpenActionMenu(currentActionItem);
			}
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
		print("Picked up " + currentActionItem.name);
		inv.AddToInventory(currentActionItem);
		CloseActionMenu();
	}

	public void LookAt () {
		print("Looked at " + currentActionItem.name);
		currentActionItem.GetComponent<Item>().LookAtItem();
		CloseActionMenu();
	}

	public void Use () {
		print("Used " + currentActionItem.name);
		currentActionItem.GetComponent<IUsableWorld>().Use();
		CloseActionMenu();
	}

	public void CloseActionMenu() {
		actionMenu.SetActive(false);
		// Set active item to null
		currentActionItem = null;
	}

	public Collider2D[] GetColliderUnderMouse () {
		return Physics2D.OverlapPointAll(GetMouseWorldPos(), mask);
	}

	public Vector3 GetMouseWorldPos () {
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
}
