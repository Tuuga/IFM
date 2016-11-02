using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	
	// Public for debug
	public List<Item> items = new List<Item>();

	public List<Item> GetItemsInInventory () {
		return items;
	}

	public void AddToInventory (Item i) {
		items.Add(i);
		i.gameObject.SetActive(false);
	}
}
