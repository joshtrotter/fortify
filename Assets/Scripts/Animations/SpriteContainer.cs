using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteContainer : MonoBehaviour {

	private SpriteRenderer rend;
	private Image img;

	void Start () {
		rend = gameObject.GetComponent<SpriteRenderer>();	
		img = gameObject.GetComponent<Image> ();
	}
	
	public void SetSprite(Sprite sprite) {
		if (rend != null) {
			rend.sprite = sprite;
		}
		if (img != null) {
			img.sprite = sprite;
		}
	}

	public Color GetColor() {
		if (rend != null) {
			return rend.color;
		} else {
			return img.color;
		}
	}

	public void SetColor(Color color) {
		if (rend != null) {
			rend.color = color;
		}
		if (img != null) {
			img.color = color;
		}
	}

	public void SetSortingLayer(string layerName) {
		if (rend != null) {
			rend.sortingLayerName = layerName;
		}
	}
}
