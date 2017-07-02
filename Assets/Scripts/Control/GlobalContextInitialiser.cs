using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalContextInitialiser : MonoBehaviour {

	[SerializeField]
	private HexBoard board;

	void Start () {
		

		if (board != null) {
			GlobalContext.INSTANCE.setBoard (board);
			Destroy (board.gameObject);
		}
	}

}
