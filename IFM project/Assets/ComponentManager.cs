using UnityEngine;
using System.Collections;

public class ComponentManager : MonoBehaviour {
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			var comps = GetComponents<TestComponent>();
			foreach(TestComponent t in comps) {
				print(t.s);
			}
		}
	}
}
