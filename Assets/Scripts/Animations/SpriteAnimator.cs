using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteContainer))]
public class SpriteAnimator : MonoBehaviour {

	private SpriteContainer spriteContainer;

	void Awake () {
		spriteContainer = gameObject.GetComponent<SpriteContainer>();	
	}

	public void SetSprite(Sprite sprite) {
		spriteContainer.SetSprite(sprite);
	}

	public IEnumerator SacrificeAnimation(Sprite sprite, Action onComplete, float initialScale = 0.33f, float duration = 0.20f, float expansionSize = 1.66f) {
		EventBus.INSTANCE.NotifyStartAnimation ();
		SpriteAnimator placeholder = Instantiate (this, transform.parent);
		spriteContainer.SetSortingLayer("Overlay");
		SetSprite(sprite);

		Vector3 initialExpansionSize = Vector3.one * initialScale;
		transform.localScale = initialExpansionSize;

		yield return placeholder.Expand (Vector3.one, Vector3.one * expansionSize, duration, () => {});
		placeholder.AnimateExpand (Vector3.one * expansionSize, Vector3.one, duration, () => {});
		yield return Expand (initialExpansionSize, Vector3.one, duration, () => {});

		Destroy (placeholder.gameObject);
		spriteContainer.SetSortingLayer("Default");

		if (onComplete != null) {			
			onComplete ();
		}

		EventBus.INSTANCE.NotifyEndAnimation ();
	}

	public void AnimateExpandToSprite(Sprite sprite, Action onComplete, float initialScale = 0.33f, float duration = 0.20f) {
		StartCoroutine (ExpandToSprite(sprite, onComplete, initialScale, duration));
	}
	
	public IEnumerator ExpandToSprite(Sprite sprite, Action onComplete, float initialScale = 0.33f, float duration = 0.20f) {
		EventBus.INSTANCE.NotifyStartAnimation ();
		SpriteAnimator placeholder = Instantiate (this, transform.parent);
		spriteContainer.SetSortingLayer("Overlay");
		SetSprite(sprite);

		Vector3 initialExpansionSize = Vector3.one * initialScale;
		transform.localScale = initialExpansionSize;

		yield return Expand (initialExpansionSize, Vector3.one, duration, () => {});
			
		Destroy (placeholder.gameObject);
		spriteContainer.SetSortingLayer("Default");

		if (onComplete != null) {			
			onComplete ();
		}


		EventBus.INSTANCE.NotifyEndAnimation ();
	}

	public IEnumerator FlipToSprite(Sprite sprite, Action onComplete, float endSize = 1f, float duration = 0.20f) {	
		EventBus.INSTANCE.NotifyStartAnimation ();	
		yield return Flip (transform.localScale.x, 0f, duration / 2f);
		SetSprite(sprite);
		yield return Flip (0f, endSize, duration / 2f);

		if (onComplete != null) {			
			onComplete ();
		}
		EventBus.INSTANCE.NotifyEndAnimation ();
	}

	public void AnimateExpand(Vector3 startSize, Vector3 endSize, float duration, Action onComplete) {
		StartCoroutine(Expand(startSize, endSize, duration, onComplete));
	}

	public IEnumerator Expand(Vector3 startSize, Vector3 endSize, float duration, Action onComplete) {
		float elapsed = 0f;
		do {			
			yield return new WaitForEndOfFrame ();
			elapsed += Time.deltaTime;
			transform.localScale = Vector3.Lerp (startSize, endSize, elapsed / duration);
		} while (elapsed < duration);
		if (onComplete != null) {
			onComplete ();
		}
	}

	public IEnumerator Flip(float startSize, float endSize, float duration) {
		float elapsed = 0f;
		while (elapsed < duration) {			
			elapsed += Time.deltaTime;
			transform.localScale = new Vector3(Mathf.Lerp (startSize, endSize, elapsed / duration), transform.localScale.y);
			yield return new WaitForEndOfFrame ();
		} 
	}

	public IEnumerator ExpandingFlip(float startSize, float endSize, float startRotation, float endRotation, float duration, Action onComplete) {
		float elapsed = 0f;
		while (elapsed < duration) {			
			elapsed += Time.deltaTime;
			float newScale = Mathf.Lerp (startSize, endSize, elapsed / duration);
			transform.localScale = new Vector3(newScale, newScale);
			transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.x, Mathf.Lerp (startRotation, endRotation, elapsed / duration), transform.localRotation.z));
			yield return new WaitForEndOfFrame ();
		}

		if (onComplete != null) {
			onComplete ();
		}
	}

	public IEnumerator TweenColor(Color to, float duration, Action onComplete) {
		Color orig = spriteContainer.GetColor ();
		float elapsed = 0f;
		while (elapsed < duration) {			
			elapsed += Time.deltaTime;
			spriteContainer.SetColor(Color.Lerp (orig, to, elapsed / duration));
			yield return new WaitForEndOfFrame ();
		}

		if (onComplete != null) {
			onComplete ();
		}
	}
}
