using UnityEngine;
using System.Collections;

public class StaticItem : MonoBehaviour {

	public void Use() {
		var useWithArray = GetComponents<UseWith>();
		foreach (UseWith uw in useWithArray) {
			uw.onUse.Invoke();
		}
	}
}
