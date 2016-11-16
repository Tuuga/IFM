using UnityEngine;
using System.Collections;
using System.Collections.Generic;

struct ScheduleItem {
	public float time;
	public enum Action { Delay, Invoke };
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
	float timer;
		
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

		if (a.action == ScheduleItem.Action.Invoke) {
			timer = a.time;
			a.objParam.Invoke(a.strParam, 0);
			actions.RemoveAt(0);
		}
	}

	public void Delay(float f) {
		ScheduleItem sch = new ScheduleItem(f, ScheduleItem.Action.Delay, "", null);
		actions.Add(sch);
	}

	public void InvokeLater (MonoBehaviour obj, string method, float duration) {
		ScheduleItem sch = new ScheduleItem(duration, ScheduleItem.Action.Invoke, method, obj);
		actions.Add(sch);
	}
}
