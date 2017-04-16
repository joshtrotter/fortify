using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPanel : MonoBehaviour {

	private Text text;

	void Start () {
		text = GetComponentInChildren<Text> ();
	}

	public void OnClick()
	{
		Hide (() => {});
	}
	
	public void Reveal(string message, Action onReveal, float delay = 0f, float duration = 0.15f)
	{
		StartCoroutine(RevealOverTime(message, onReveal, delay, duration));
	}

	public void Hide(Action onHide, float delay = 0f, float duration = 0.15f)
	{
		StartCoroutine (HideOverTime (onHide, delay, duration));
	}

	private IEnumerator RevealOverTime(string message, Action onReveal, float delay, float duration)
	{
		yield return new WaitForSeconds (delay);
		text.text = message;

		Vector3 start = new Vector3 (0f, 1f, 1f);
		float elapsed = 0f;
		while (elapsed < duration) {
			elapsed += Time.deltaTime;
			transform.localScale = Vector3.Lerp (start, Vector3.one, elapsed / duration);
			yield return new WaitForEndOfFrame ();
		}

		if (onReveal != null) {
			onReveal ();
		}
	}

	private IEnumerator HideOverTime(Action onHide, float delay, float duration)
	{		
		yield return new WaitForSeconds (delay);

		float elapsed = 0f;
		Vector3 start = transform.localScale;
		Vector3 end = new Vector3 (0f, 1f, 1f);
		while (elapsed < duration) {
			elapsed += Time.deltaTime;
			transform.localScale = Vector3.Lerp (start, end, elapsed / duration);
			yield return new WaitForEndOfFrame ();
		}

		if (onHide != null) {
			onHide ();
		}
	}
}
