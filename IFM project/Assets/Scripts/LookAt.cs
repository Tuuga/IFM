using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LookAt : MonoBehaviour {

	public float textTime;
	[TextArea]
	public string lookAtText;

	// TODO: Closeup look at the item
	
	public void ActivateText (GameObject dialogBox) {
		StartCoroutine(DisplayText(dialogBox));
	}

	IEnumerator DisplayText (GameObject dialogBox) {
		var t = dialogBox.GetComponentInChildren<Text>();

		dialogBox.SetActive(true);
		t.text = lookAtText;
		yield return new WaitForSeconds(textTime);
		t.text = "";
		dialogBox.SetActive(false);
	}
}
