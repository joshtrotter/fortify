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
	private TextEffects rewardTitle;

	[SerializeField]
	private string rewardTitleText;

	[SerializeField]
	private TextEffects rewardBody;

	[SerializeField]
	private string rewardBodyText;

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
		uiAnimator.Resize (() => HorizontalExpand(), new Vector3 (0.5f, 0f), new Vector3 (0.5f, 1f), delay, 0.2f);
	}

	private void HorizontalExpand () {		
		uiAnimator.Resize (() => ShowChallengeTitle(), new Vector3 (0.5f, 1f), new Vector3 (1f, 1f), 0.25f, 0.2f);
	}

	public void ShowChallengeTitle (float delay = 0f) {
		StartCoroutine(this.challengeTitle.TypeText (challengeTitleText, () => ShowChallengeBody(0.4f), 0.4f, delay));	
	}

	public void ShowChallengeBody (float duration, float delay = 0f, bool append = false) {
		StartCoroutine(this.challengeBody.FadeInText (challengeBodyText, () => ShowRewardTitle(), duration, delay, append));	
	}

	public void ShowRewardTitle (float delay = 0f) {
		StartCoroutine(this.rewardTitle.TypeText (rewardTitleText, () => ShowRewardBody(0.4f), 0.4f, delay));	
	}

	public void ShowRewardBody (float duration, float delay = 0f, bool append = false) {
		StartCoroutine(this.rewardBody.FadeInText (rewardBodyText, () => ShowButton(), duration, delay, append));	
	}

	public void ShowButton (float delay = 0f, float duration = 0.25f) {
		button.Resize (() => {}, new Vector3 (0f, 1f), new Vector3 (1f, 1f), delay, duration);
	}

	public void Hide (Action onComplete) {
		uiAnimator.Resize (onComplete, new Vector3 (1f, 1f), new Vector3 (1f, 0f));
	}
}
