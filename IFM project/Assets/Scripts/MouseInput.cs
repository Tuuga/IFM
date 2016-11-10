using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MouseInput : MonoBehaviour {

	public GameObject actionMenu;
	public GameObject dialogBox;
	public LayerMask mask;

	public float minDisToItemForAction;

	PlayerMovement mov;
	Inventory inv;

	GameObject currentActionItem;

	void Start () {
		mov = GetComponent<PlayerMovement>();
		inv = GetComponent<Inventory>();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject() && !inv.GetSelected()) {
			var col = GetColliderUnderMouse();
			var lastActionItem = currentActionItem;

			currentActionItem = null;
			foreach (Collider2D c in col) {
				if (c.tag == "Activateble") {
					currentActionItem = c.gameObject;
				} else if (c.tag == "Ground") {
					mov.MovePlayer(GetMouseWorldPos());
				}
			}


			// OLD
			//if (lastActionItem != currentActionItem) {
			//	if (currentActionItem) {
			//		OpenActionMenu();
			//	}
			//}


			// Open the action menu if close enough
			// Else move player to items pos and open it after reached
			if (lastActionItem != currentActionItem) {
				if (currentActionItem) {
					var actPos = currentActionItem.transform.position;
					var playerToAct = actPos - transform.position;
					playerToAct.z = 0;	// Just in case
					var dist = playerToAct.magnitude;

					if (dist > minDisToItemForAction) {
						CloseActionMenu(false);
						mov.MovePlayer(currentActionItem.transform.position, "OpenActionMenu", this);
					} else {
						OpenActionMenu();
					}
				}
			}
			
			if (!currentActionItem) {
				CloseActionMenu(true);
			}
		}
	}
	
	// WIP
	void OpenActionMenu () {
		actionMenu.SetActive(true);
		actionMenu.transform.position = Input.mousePosition + new Vector3(0, 50, 0);
	}

	public void PickUp () {
		var addItem = currentActionItem.GetComponent<Item>();
		if (addItem) {
			inv.AddToInventory(addItem);
		} else {
			// WIP
			print("<color=red>No Item component</color>");
		}
		CloseActionMenu(true);
	}

	public void LookAt () {
		var lookAtItem = currentActionItem.GetComponent<LookAt>();
		if (lookAtItem) {
			lookAtItem.ActivateText(dialogBox);
		} else {
			// WIP
			print("<color=red>No LookAt component</color>");
		}
		CloseActionMenu(true);
	}

	public void Use () {
		var useItem = currentActionItem.GetComponent<StaticItem>();
		if (useItem) {
			useItem.Use();
		} else {
			// WIP
			print("<color=red>No StaticItem component</color>");
		}
		CloseActionMenu(true);
	}

	public void CloseActionMenu(bool setCurrentItemNull) {
		actionMenu.SetActive(false);
		if (setCurrentItemNull) {
			currentActionItem = null;
		}
	}

	public Collider2D[] GetColliderUnderMouse () {
		return Physics2D.OverlapPointAll(GetMouseWorldPos(), mask);
	}

	public Vector3 GetMouseWorldPos () {
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
}
