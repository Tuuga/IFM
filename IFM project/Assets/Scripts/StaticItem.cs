using UnityEngine;
using System.Collections;

public class StaticItem : MonoBehaviour {

	public string customUseText;

	public void Use() {
		var useWithArray = GetComponents<UseWith>();
		foreach (UseWith uw in useWithArray) {
			uw.onUse.Invoke();
		}
	}
}
