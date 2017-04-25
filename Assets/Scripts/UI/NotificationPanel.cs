using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UIAnimator))]
public class NotificationPanel : MonoBehaviour {

	private Text text;
	private UIAnimator uiAnimator;

	void Awake () {
		text = GetComponentInChildren<Text> ();
		uiAnimator = GetComponent<UIAnimator> ();
	}

	public void OnClick()
	{
		Hide (() => {});
	}
	
	public void Reveal(string message, Action onReveal, float delay = 0f, float duration = 0.15f)
	{
		text.text = message;
		uiAnimator.Resize (onReveal, new Vector3 (0f, 1f, 1f), Vector3.one, delay, duration);
	}

	public void Hide(Action onHide, float delay = 0f, float duration = 0.15f)
	{
		uiAnimator.Resize (onHide, transform.localScale, new Vector3 (0f, 1f, 1f), delay, duration);
	}
		
}
