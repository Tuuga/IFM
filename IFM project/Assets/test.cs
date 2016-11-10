using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class test : MonoBehaviour {
	[SerializeField]
	int listCount;

	void Start () {
		List<Vector3> test = new List<Vector3>(new Vector3[listCount]);

		foreach (Vector3 v in test) {
			print(v);
		}
	}
}
