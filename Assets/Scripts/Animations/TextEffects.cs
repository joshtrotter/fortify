using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextEffects : MonoBehaviour {

	private Text text;

	void Awake () {
		text = GetComponent<Text> ();
	}
	
	public void SetText(string text) 
	{
		this.text.text = text;
	}

	public void SetVisible(bool visible)
	{
		gameObject.SetActive (visible);
	}

	public IEnumerator TypeText(string message, Action onComplete, float duration, float delay = 0f, bool append = false)
	{
		yield return new WaitForSeconds (delay);
		if (append) {
			message = this.text.text + message;
		}
		float elapsed = 0f;
		while (elapsed < duration) {
			elapsed += Time.deltaTime;
			float len = Mathf.Lerp (0, message.Length, elapsed / duration);
			SetText(message.Substring(0, (int)len));
			yield return new WaitForEndOfFrame ();
		}

		if (onComplete != null) {
			onComplete ();
		}
	}

	public IEnumerator FadeInText(string message, Action onComplete, float duration, float delay = 0f, bool append = false)
	{
		yield return new WaitForSeconds (delay);
		string existing = "";
		if (append) {
			existing = this.text.text;
		}
		float elapsed = 0f;
		while (elapsed < duration) {
			elapsed += Time.deltaTime;
			int alphaAmount = (int)Mathf.Lerp (0, 15, elapsed / duration);
			string alphaCode = alphaAmount.ToString("X") + alphaAmount.ToString("X");
			SetText(existing + "<color=#ffffff" + alphaCode.ToLower() + ">" + message + "</color>");
			yield return new WaitForEndOfFrame ();
		}
		if (onComplete != null) {
			onComplete ();
		}
	}
}
