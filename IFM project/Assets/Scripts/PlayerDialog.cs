using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerDialog : MonoBehaviour {

	public GameObject dialogBox;
	Text t;
	string text;

	Scheduler scheduler;

	void Start () {
		t = dialogBox.GetComponentInChildren<Text>();
		scheduler = GameObject.Find("Scheduler").GetComponent<Scheduler>();
	}

	public void ShowText() {
		dialogBox.SetActive(true);
		t.text = text;
	}

	public  void HideText() {
		t.text = "";
		dialogBox.SetActive(false);
	}

	public void ShowTextSCH (string text) {
		// TODO: remember what instance of ShowText was called
		this.text = text;
		scheduler.InvokeLater(this, "ShowText", 2f);
	}

	public void HideTextSCH () {
		scheduler.InvokeLater(this, "HideText", 0f);
	}
}
