using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour, IUsableInventory {

	// WIP
	public void Use () {
		print(gameObject.name + " used with " + GetComponent<Item>().GetUseWith().name);
	}
}
