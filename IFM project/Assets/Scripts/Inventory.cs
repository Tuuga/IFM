using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public Sprite empty;
	public List<Image> slots;

	Item selected;

	// Public for debug
	public List<Item> items = new List<Item>();

	MouseInput mouseInput;

	void Start () {
		mouseInput = GetComponent<MouseInput>();
	}

	public void AddToInventory (Item itemToAdd) {

		if (items.Count < slots.Count && !items.Contains(itemToAdd)) {
			items.Add(itemToAdd);

			var slotChildImage = slots[items.Count - 1].transform.GetChild(0).GetComponent<Image>();
			print(slotChildImage.name);
			slotChildImage.sprite = itemToAdd.GetComponent<SpriteRenderer>().sprite;
			slotChildImage.color = itemToAdd.GetComponent<SpriteRenderer>().color;

			itemToAdd.gameObject.SetActive(false);
			itemToAdd.GetComponent<BoxCollider2D>().enabled = false;
		} else {
			print("Inventory full or item already in inventory");
		}
	}

	void Update () {
		// WIP
		if (selected) {
			var pos = mouseInput.GetMouseWorldPos();
			pos.z = 0;
			selected.transform.position = pos;

			if (Input.GetKeyDown(KeyCode.Mouse0)) {
				UseSelected();
			}
		}
	}
	
	// Called from inventory slot buttons
	public void Select (int index) {
		if (index < items.Count) {
			selected = items[index];
			selected.gameObject.SetActive(true);
		}
	}

	void Deselect () {
		selected.gameObject.SetActive(false);
		selected = null;
	}

	public void UseSelected () {
		var cols = mouseInput.GetColliderUnderMouse();

		Pointable pointable = null;
		foreach (Collider2D c in cols) {
			if (c.GetComponent<Pointable>()) {
				pointable = c.GetComponent<Pointable>();
			}
		}

		if (!pointable) {
			Deselect();
			return;
		}

		selected.Use(pointable);
		Deselect();
	}

	// Called optionally from the event
	public void DestroyItemOnUse (Item item) {
		items.Remove(item);
		UpdateSlotSprites();
		Destroy(item.gameObject);
	}

	void UpdateSlotSprites () {
		// For every slot that has an item
		// Set slots childs sprite to items sprite
		// For every empty slot
		// Set slots childs sprite to empty
		for (int i = 0; i < slots.Count; i++) {
			var slotSprite = slots[i].transform.GetChild(0).GetComponent<Image>();
			if (i < items.Count) {
				var itemSR = items[i].GetComponent<SpriteRenderer>();

				slotSprite.sprite = itemSR.sprite;
				slotSprite.color = itemSR.color;
			} else {
				slotSprite.sprite = empty;
				slotSprite.color = Color.white;
			}
		}
	}
}
