using UnityEngine;
using System.Collections;

public class StaticItem : MonoBehaviour {

	public string customUseText;
	public bool active = true;

	Scheduler sch;

	void Start () {
		sch = SchedulerUtility.scheduler;
	}

	public void Use() {
		var useWithArray = GetComponents<UseWith>();
		foreach (UseWith uw in useWithArray) {
			uw.onUse.Invoke();
		}
	}

	void Activate () { active = true; }
	void Deactivate () { active = false; }

	public void ActivateSCH () {
		sch.InvokeLater(this, "Activate", 0f);
	}

	public void DeactivateSCH () {
		sch.InvokeLater(this, "Deactivate", 0f);
	}
}
