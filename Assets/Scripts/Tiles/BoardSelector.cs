using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSelector : MonoBehaviour {

	[SerializeField]
	private List<HexBoard> boards;

	void Awake () {
		boards [Random.Range (0, boards.Count)].Initialise ();	
	}

}
