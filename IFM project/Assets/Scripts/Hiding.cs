using UnityEngine;
using System.Collections;

public class Hiding : MonoBehaviour {
	bool hiding;
	bool hidden;

	Color normalColor;
	public Color hiddenColor;

	PlayerMovement mov;
	SpriteRenderer sr;

	void Start () {
		mov = GetComponent<PlayerMovement>();
		sr = GetComponentInChildren<SpriteRenderer>();
		normalColor = sr.color;
	}

	// WIP
	public bool IsHidden () {
		if (hiding /* &&  monster not in same room*/) {
			hidden = true;
		} else {
			hidden = false;
		}

		return hidden;
	}

	public void Hide () {
		hiding = true;
		print("Hiding");

		sr.color = hiddenColor;
	}

	// Called from MouseInput
	public void UnHide () {
		if (!hiding) return;

		hiding = false;
		print("Not Hiding");

		sr.color = normalColor;
	}
}
