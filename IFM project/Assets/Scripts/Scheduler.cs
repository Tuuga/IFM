using UnityEngine;
using System.Collections;
using System.Collections.Generic;

struct ScheduleItem {
	public float time;
	public enum Action { Delay, TuukanTest };
	public Action action;
	public string strParam;
	public MonoBehaviour objParam;
	public ScheduleItem(float time, Action action, string strParam, MonoBehaviour objParam) {
		this.time = time;
		this.action = action;
		this.strParam = strParam;
		this.objParam = objParam;
	}
}

public class Scheduler : MonoBehaviour {
	List<ScheduleItem> actions = new List<ScheduleItem>();
	public float defaultDelay = 2f;
	float timer;
	ScheduleItem previousAction;
	
	void Update () {
		timer -= Time.deltaTime;
		if (timer > 0 || actions.Count == 0) {
			return;
		}

		var a = actions[0];

		if (a.action == ScheduleItem.Action.Delay) {
			timer = a.time;
			actions.RemoveAt(0);
		}

		if (a.action == ScheduleItem.Action.TuukanTest) {
			timer = a.time;
			print(a.strParam);
			actions.RemoveAt(0);
		}
	}
	
	public void TuukanTest (string s) {
		ScheduleItem sch = new ScheduleItem(defaultDelay, ScheduleItem.Action.TuukanTest, s, null);
		actions.Add(sch);
	}

	public void Delay(float f) {
		ScheduleItem sch = new ScheduleItem(f, ScheduleItem.Action.Delay, "", null);
		actions.Add(sch);
	}
}
