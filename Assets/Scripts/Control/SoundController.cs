using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundController : MonoBehaviour {

	[SerializeField]
	private AudioClip claimClip;

	[SerializeField]
	private AudioClip captureClip;

	[SerializeField]
	private AudioClip fortifyClip;

	[SerializeField]
	private AudioClip defortifyClip;

	[SerializeField]
	private AudioClip sacrificeClip;

	[SerializeField]
	private AudioClip notificationClip;

	private AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource> ();
	}
	
	public void PlayClaim() {
		audioSource.PlayOneShot (claimClip);
	}

	public void PlayCapture() {
		audioSource.PlayOneShot (captureClip);
	}

	public void PlayFortify() {
		audioSource.PlayOneShot (fortifyClip);
	}

	public void PlayDefortify() {
		audioSource.PlayOneShot (defortifyClip);
	}

	public void PlaySacrifice() {
		audioSource.PlayOneShot (sacrificeClip);
	}

	public void PlayNotification(float delay = 0f) {
		if (delay == 0f) {
			audioSource.PlayOneShot (notificationClip);
		} else {
			StartCoroutine(playAfterDelay(() => PlayNotification(0f), delay));
		}
	}

	private IEnumerator playAfterDelay(Action onComplete, float delay) {
		yield return new WaitForSeconds (delay);
		onComplete ();
	}
}
