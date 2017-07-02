using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundController : MonoBehaviour, SfxToggleListener {

	public static SoundController INSTANCE;

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

	private bool sfxTurnedOn = true;

	void Awake() {
		if (INSTANCE == null) {
			INSTANCE = this;
			audioSource = GetComponent<AudioSource> ();
			if (PlayerPrefs.HasKey ("sfx")) {
				sfxTurnedOn = PlayerPrefs.GetInt ("sfx") == 1;
			}
		}
	}

	void Start() {
		EventBus.INSTANCE.RegisterSfxToggleListener (this);
	}

	public void OnSfxToggle (bool sfxOn)
	{
		sfxTurnedOn = sfxOn;
	}
		
	public void PlayClaim() {
		if (sfxTurnedOn) {
			audioSource.PlayOneShot (claimClip);
		}
	}

	public void PlayCapture() {
		if (sfxTurnedOn) {
			audioSource.PlayOneShot (captureClip);
		}
	}

	public void PlayFortify() {
		if (sfxTurnedOn) {
			audioSource.PlayOneShot (fortifyClip);
		}
	}

	public void PlayDefortify() {
		if (sfxTurnedOn) {
			audioSource.PlayOneShot (defortifyClip);
		}
	}

	public void PlaySacrifice() {
		if (sfxTurnedOn) {
			audioSource.PlayOneShot (sacrificeClip);
		}
	}

	public void PlayNotification(float delay = 0f) {		
		if (delay == 0f) {
			if (sfxTurnedOn) {
				audioSource.PlayOneShot (notificationClip);
			}
		} else {
			StartCoroutine(playAfterDelay(() => PlayNotification(0f), delay));
		}
	}

	private IEnumerator playAfterDelay(Action onComplete, float delay) {
		yield return new WaitForSeconds (delay);
		onComplete ();
	}
}
