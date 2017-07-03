using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIAnimator))]
public class ChallengePanel : MonoBehaviour {

	[SerializeField]
	private TextEffects challengeTitle;

	[SerializeField]
	private string challengeTitleText;

	[SerializeField]
	private TextEffects challengeBody;

	[SerializeField]
	private string challengeBodyText;

	[SerializeField]
	private UIAnimator button;

	private UIAnimator uiAnimator;

	void Awake () {
		uiAnimator = GetComponent<UIAnimator> ();
	}

	public void Initialise(float delay = 0f) {
		VerticalExpand (delay);
	}

	private void VerticalExpand (float delay) {
		uiAnimator.Resize (() => ShowChallengeTitle(), new Vector3 (1f, 0f), new Vector3 (1f, 1f), delay, 0.2f);
	}

	public void ShowChallengeTitle (float delay = 0f) {
		StartCoroutine(this.challengeTitle.TypeText (challengeTitleText, () => ShowChallengeBody(0.4f), 0.4f, delay));	
	}

	public void ShowChallengeBody (float duration, float delay = 0f, bool append = false) {
		StartCoroutine(this.challengeBody.FadeInText (challengeBodyText, () => ShowButton(), duration, delay, append));	
	}
		
	public void ShowButton (float delay = 0f, float duration = 0.25f) {
		button.Resize (() => {}, new Vector3 (0f, 1f), new Vector3 (1f, 1f), delay, duration);
	}

	public void Hide (Action onComplete) {
		uiAnimator.Resize (onComplete, new Vector3 (1f, 1f), new Vector3 (1f, 0f));
	}
}
