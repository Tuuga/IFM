using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public Sprite empty;
	public List<Image> slots;

	GameObject selected;

	// Public for debug
	public List<GameObject> items = new List<GameObject>();

	MouseInput mouseInput;

	void Start () {
		mouseInput = GetComponent<MouseInput>();
	}

	public void AddToInventory (GameObject g) {

		if (items.Count < slots.Count && !items.Contains(g)) {
			items.Add(g);
			slots[items.Count - 1].sprite = g.GetComponent<SpriteRenderer>().sprite;
			g.SetActive(false);
			g.GetComponent<BoxCollider2D>().enabled = false;
		} else {
			print("Inventory full or item already in inventory");
		}
	}

	void Update () {
		// WIP
		if (selected != null) {
			var pos = mouseInput.GetMouseWorldPos();
			pos.z = 0;
			selected.transform.position = pos;

			if (Input.GetKeyDown(KeyCode.Mouse0)) {
				UseSelected();
			}
		}
	}
	
	public void Select (int index) {
		if (index < items.Count) {
			selected = items[index];
			selected.SetActive(true);
		}
	}

	void Deselect () {
		selected.SetActive(false);
		selected = null;
	}

	public void UseSelected () {
		var cols = mouseInput.GetColliderUnderMouse();
		Collider2D col = null;

		foreach (Collider2D c in cols) {
			col = c;
		}
		if (col == null || col.gameObject != selected.GetComponent<Item>().GetUseWith()) {
			if (col != null)
			print(col.gameObject.name);
			Deselect();
			return;
		}

		selected.GetComponent<IUsableInventory>().Use();
		slots[items.IndexOf(selected)].sprite = empty;
		items.Remove(selected);
		UpdateSlotSprites();
		Destroy(selected);
	}

	void UpdateSlotSprites () {
		for (int i = 0; i < slots.Count; i++) {
			if (i < items.Count) {
				slots[i].sprite = items[i].GetComponent<SpriteRenderer>().sprite;
			} else {
				slots[i].sprite = empty;
			}
		}
	}
}
