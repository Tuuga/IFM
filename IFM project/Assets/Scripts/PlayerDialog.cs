using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerDialog : MonoBehaviour {

	public GameObject dialogBox;
	Text t;
	List<string> dialogs = new List<string>();

	Scheduler scheduler;

	void Start () {
		t = dialogBox.GetComponentInChildren<Text>();
		scheduler = SchedulerUtility.scheduler;
	}

	public void ShowText() {
		dialogBox.SetActive(true);
		t.text = dialogs[0];
		dialogs.RemoveAt(0);
	}

	public  void HideText() {
		t.text = "";
		dialogBox.SetActive(false);
	}

	public void ShowTextSCH (string text) {
		// TODO: remember what instance of ShowText was called
		dialogs.Add(text);
		scheduler.InvokeLater(this, "ShowText", 2f);
	}

	public void HideTextSCH () {
		scheduler.InvokeLater(this, "HideText", 0.5f);
	}
}
