using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalContextInitialiser : MonoBehaviour {

	[SerializeField]
	private Player player1;

	[SerializeField]
	private Player player2;

	[SerializeField]
	private HexBoard board;

	[SerializeField]
	private ActionRuleSet ruleSet;

	[SerializeField]
	private SoundController soundController;

	void Start () {
		if (player1 != null) {
			GlobalContext.INSTANCE.setPlayer1 (player1);
			Destroy (player1.gameObject);
		}

		if (player2 != null) {
			GlobalContext.INSTANCE.setPlayer2 (player2);
			Destroy (player2.gameObject);
		}

		if (board != null) {
			GlobalContext.INSTANCE.setBoard (board);
			Destroy (board.gameObject);
		}

		if (ruleSet != null) {
			GlobalContext.INSTANCE.setActionRuleSet (ruleSet);
			Destroy (ruleSet.gameObject);
		}

		if (soundController != null) {
			GlobalContext.INSTANCE.setSoundController (soundController);
			Destroy (soundController.gameObject);
		}			

	}

}
