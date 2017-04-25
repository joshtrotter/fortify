using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimator : MonoBehaviour {

	public void Resize(Action onComplete, Vector3 start, Vector3 end, float delay = 0f, float duration = 0.15f)
	{
		StartCoroutine(ResizeOverTime(onComplete, start, end, delay, duration));
	}

	private IEnumerator ResizeOverTime(Action onComplete, Vector3 start, Vector3 end, float delay, float duration)
	{
		yield return new WaitForSeconds (delay);
		float elapsed = 0f;
		while (elapsed < duration) {
			elapsed += Time.deltaTime;
			transform.localScale = Vector3.Lerp (start, end, elapsed / duration);
			yield return new WaitForEndOfFrame ();
		}

		if (onComplete != null) {
			onComplete ();
		}
	}

}
