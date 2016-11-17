using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MouseInput : MonoBehaviour {

	public GameObject actionMenu;
	GameObject pickUp, lookAt, use;
	public GameObject dialogBox;
	public Text highlightText;
	public LayerMask mask;

	public float minDisToItemForAction;

	PlayerMovement mov;
	Inventory inv;
	Hiding hiding;
	Scheduler scheduler;

	GameObject currentActionItem;
	bool canControl = true;

	void Start () {
		mov = GetComponent<PlayerMovement>();
		inv = GetComponent<Inventory>();
		hiding = GetComponent<Hiding>();

		pickUp = actionMenu.transform.Find("Pick Up").gameObject;
		lookAt = actionMenu.transform.Find("Look At").gameObject;
		use = actionMenu.transform.Find("Use").gameObject;
		scheduler = GameObject.Find("Scheduler").GetComponent<Scheduler>();
	}

	void Update() {
		var col = GetColliderUnderMouse();

		highlightText.text = "";
		foreach (Collider2D c in col) {
			if (c.tag == "Activateble") {
				highlightText.text = c.name;
			}
		}
		
		if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject() && !inv.GetSelected() && canControl) {
			var lastActionItem = currentActionItem;

			currentActionItem = null;
			foreach (Collider2D c in col) {
				if (c.tag == "Activateble") {
					currentActionItem = c.gameObject;
					hiding.UnHide();
					SetActionMenuButtons();
				}
			}

			if (col.Length == 0) {
				mov.MovePlayer(GetMouseWorldPos());
				hiding.UnHide();
			}


			// Open the action menu if close enough
			// Else move player to items pos and open it after reached
			if (lastActionItem != currentActionItem) {
				if (currentActionItem) {

					CloseActionMenu(false);
					mov.MovePlayer(GetWalkTarget(currentActionItem) , "OpenActionMenu", this);
				}
			}
			
			if (!currentActionItem) {
				CloseActionMenu(true);
			}
		}
	}
	
	public void DisableControls ()	{	canControl = false;	}
	public void EnableControls ()	{	canControl = true;	}

	public void DisableControlsSCH () {
		scheduler.InvokeLater(this, "DisableControls", 0f);
	}

	public void EnableControlsSCH () {
		scheduler.InvokeLater(this, "EnableControls", 0f);
	}

	// WIP
	void OpenActionMenu () {
		actionMenu.SetActive(true);
		actionMenu.transform.position = Camera.main.WorldToScreenPoint(currentActionItem.transform.position) + new Vector3(0, 50, 0);
	}

	public void PickUp () {
		var addItem = currentActionItem.GetComponent<Item>();
		inv.AddToInventory(addItem);
		CloseActionMenu(true);
	}

	public void LookAt () {
		var lookAtItem = currentActionItem.GetComponent<LookAt>();
		lookAtItem.ActivateText(dialogBox);
		CloseActionMenu(true);
	}

	public void Use () {
		var useItem = currentActionItem.GetComponent<StaticItem>();
		useItem.Use();
		CloseActionMenu(true);
	}

	public void CloseActionMenu(bool setCurrentItemNull) {
		actionMenu.SetActive(false);
		if (setCurrentItemNull) {
			currentActionItem = null;
		}
	}

	void SetActionMenuButtons () {
		if (currentActionItem.GetComponent<Item>()) {
			pickUp.SetActive(true);
		} else { pickUp.SetActive(false); }

		if (currentActionItem.GetComponent<StaticItem>()) {
			use.SetActive(true);
		} else { use.SetActive(false); }

		if (currentActionItem.GetComponent<LookAt>()) {
			lookAt.SetActive(true);
		} else { lookAt.SetActive(false); }
	}

	public Collider2D[] GetColliderUnderMouse () {
		return Physics2D.OverlapPointAll(GetMouseWorldPos(), mask);
	}

	public Vector3 GetMouseWorldPos () {
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	public Vector2 GetWalkTarget (GameObject g) {
		// TODO: Check for custom walk target
		var sr = g.GetComponentInChildren<SpriteRenderer>();
		if (!sr)
			return g.transform.position;
		var b = sr.bounds;
		return new Vector2(b.center.x, b.min.y - 0.2f);
	}
}
