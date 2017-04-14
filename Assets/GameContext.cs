using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : MonoBehaviour {

	[SerializeField]
	private GameController gameController;

	[SerializeField]
	private ActionRuleSet actionRuleSet;



	private void setup() {
		Player player1 = GlobalContext.INSTANCE.getPlayer1 ();
		Player player2 = GlobalContext.INSTANCE.getPlayer2 ();

		gameController.Initialise ();
	}

	public GameController getGameController() {
		return gameController;
	}

	public ActionRuleSet getRuleSet() {
		return actionRuleSet;
	}		
		 
}
