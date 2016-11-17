using UnityEngine;
using System.Collections;

public class PlayAnimation : MonoBehaviour {
	public Animator anim;
	public AnimationClip clip;
	Scheduler scheduler;

	void Start () {
		scheduler = SchedulerUtility.scheduler;
	}
	
	void PlayClip () {
		anim.Play(clip.name);
	}

	public void PlayClipSCH () {
		scheduler.InvokeLater(this, "PlayClip", clip.length);
	}
}
