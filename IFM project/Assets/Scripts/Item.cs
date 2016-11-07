using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public string lookAtText;
	public GameObject useWith;

	// WIP
	public void LookAtItem () {
		print(lookAtText);
	}

	public GameObject GetUseWith () {
		return useWith;
	}
}
