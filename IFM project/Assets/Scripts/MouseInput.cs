﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MouseInput : MonoBehaviour {

	public GameObject actionMenu;
	public GameObject dialogBox;
	public LayerMask mask;

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
				}
			}

			if (lastActionItem != currentActionItem) {
				if (currentActionItem) {
					OpenActionMenu();
				}
			}

			if (!currentActionItem) {
				CloseActionMenu();
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
			print("<color=red>No Item component</color>");
		}
		CloseActionMenu();
	}

	public void LookAt () {
		var lookAtItem = currentActionItem.GetComponent<LookAt>();
		if (lookAtItem) {
			lookAtItem.ActivateText(dialogBox);
		} else {
			print("<color=red>No LookAt component</color>");
		}
		CloseActionMenu();
	}

	public void Use () {
		var useItem = currentActionItem.GetComponent<StaticItem>();
		if (useItem) {
			useItem.Use();
		} else {
			print("<color=red>No StaticItem component</color>");
		}
		CloseActionMenu();
	}

	public void CloseActionMenu() {
		actionMenu.SetActive(false);
		currentActionItem = null;
	}

	public Collider2D[] GetColliderUnderMouse () {
		return Physics2D.OverlapPointAll(GetMouseWorldPos(), mask);
	}

	public Vector3 GetMouseWorldPos () {
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
}
