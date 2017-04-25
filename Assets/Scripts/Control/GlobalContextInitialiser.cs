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

	void Start () {
		GlobalContext.INSTANCE.setPlayer1 (player1);
		GlobalContext.INSTANCE.setPlayer2 (player2);
		GlobalContext.INSTANCE.setBoard (board);
		GlobalContext.INSTANCE.setActionRuleSet (ruleSet);

		Destroy (player1.gameObject);
		Destroy (player2.gameObject);
		Destroy (board.gameObject);
		Destroy (ruleSet.gameObject);
	}

}
