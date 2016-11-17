using UnityEngine;
using System.Collections;

public class SchedulerUtility : MonoBehaviour {

	public static Scheduler scheduler;

	void Awake () {
		scheduler = GameObject.Find("Scheduler").GetComponent<Scheduler>();
	}
}
