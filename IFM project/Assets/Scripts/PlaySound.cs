using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {

	public AudioSource audioSource;
	Scheduler scheduler;

	void Start() {
		scheduler = SchedulerUtility.scheduler;
	}

	void PlayClip() {
		audioSource.Play();
	}

	public void PlaySoundClipSCH() {
		scheduler.InvokeLater(this, "PlayClip", audioSource.clip.length);
	}
}
