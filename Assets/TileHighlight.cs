using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHighlight : MonoBehaviour {

	[SerializeField]
	private float rotateSpeed = 0.2f;

	private float elapsed = 0f;

	void Update () {
		elapsed += Time.deltaTime;
		if (elapsed >= rotateSpeed) {
			transform.Rotate (new Vector3 (0f, 0f, 60f));	
			elapsed = 0f;
		}
	}

}
