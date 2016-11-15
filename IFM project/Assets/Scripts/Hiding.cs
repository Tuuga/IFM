using UnityEngine;
using System.Collections;

public class Hiding : MonoBehaviour {
	bool hiding;
	public bool hidden;

	Color normalColor;
	public Color hiddenColor;

	PlayerMovement mov;
	SpriteRenderer sr;
	public Enemy enemy;

	void Start () {
		mov = GetComponent<PlayerMovement>();
		sr = GetComponentInChildren<SpriteRenderer>();
		normalColor = sr.color;
	}

	public bool IsHidden () {
		return hidden;
	}

	public void Hide () {
		hiding = true;

		if (mov.GetAtRoom() != enemy.GetAtRoom()) {
			hidden = true;
		} else {
			hidden = false;
		}
		print("Hidden? " + IsHidden());
		print("Player: " + mov.GetAtRoom());
		print("Enemy: " + enemy.GetAtRoom());

		sr.color = hiddenColor;
	}

	// Called from MouseInput
	public void UnHide () {
		if (!hiding) return;

		hiding = false;
		hidden = false;
		print("Not Hiding");

		sr.color = normalColor;
	}
}
