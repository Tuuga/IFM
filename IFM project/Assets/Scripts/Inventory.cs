using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public List<Image> slots;

	// Public for debug
	public List<GameObject> items = new List<GameObject>();

	public List<GameObject> GetItemsInInventory () {
		return items;
	}

	public void AddToInventory (GameObject g) {

		if (items.Count < slots.Count) {
			items.Add(g);
			slots[items.Count - 1].sprite = g.GetComponent<SpriteRenderer>().sprite;
			g.gameObject.SetActive(false);
		} else {
			print("INVENTORY FULL");
		}
	}

	public void DEBUG (GameObject g) {
		print(g.name);
	}
}
