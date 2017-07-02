using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
public class HexButton : MonoBehaviour {

	[SerializeField]
	private Sprite upSprite;

	[SerializeField]
	private Sprite downSprite;

	[SerializeField]
	private float heightShrinkAmount = 5f;

	private Image image;
	private RectTransform rectTransform;

	private Vector2 upSize;
	private Vector3 upPos;
	private Vector2 downSize;
	private Vector3 downPos;

	private bool upState = true;

	void Awake() {
		image = GetComponent<Image> ();
		rectTransform = GetComponent<RectTransform> ();

		upSize = rectTransform.sizeDelta;
		upPos = rectTransform.localPosition;
		downSize = new Vector2 (upSize.x, upSize.y - heightShrinkAmount);
		downPos = upPos - new Vector3 (0, heightShrinkAmount / 2f, 0);
	}

	public void onDown() {
		rectTransform.sizeDelta = downSize;
		rectTransform.localPosition = downPos;
		image.sprite = downSprite;
		upState = false;
	}

	public void onUp() {
		rectTransform.sizeDelta = upSize;
		rectTransform.localPosition = upPos;
		image.sprite = upSprite;
		upState = true;
	}

	public void ToggleState() {
		if (upState) {
			onDown ();
		} else {
			onUp ();
		}
	}

}
