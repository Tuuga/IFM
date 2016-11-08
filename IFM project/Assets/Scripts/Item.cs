using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public void Use(Pointable withThis) {
		var useWithArray = GetComponents<UseWith>();
		var pointable = System.Array.Find(useWithArray, p => p.objectOnLevel == withThis);
		if (pointable) {
			pointable.onUse.Invoke();
		}
	}
}
