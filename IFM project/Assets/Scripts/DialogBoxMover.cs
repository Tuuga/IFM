using UnityEngine;
using System.Collections;

public class DialogBoxMover : MonoBehaviour {

	public Transform dialogBox;

	void Update () {
		dialogBox.position = Camera.main.WorldToScreenPoint(transform.position);
	}
}
