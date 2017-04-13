using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : MonoBehaviour {

	[SerializeField]
	private GameController gameController;

	[SerializeField]
	private ActionRuleSet actionRuleSet;

	[SerializeField]
	private PlayerUI player1UI;

	[SerializeField]
	private PlayerUI player2UI;

	void Start () {
		setup ();
		gameController.StartGame ();
	}

	private void setup() {
		Player player1 = GlobalContext.INSTANCE.getPlayer1 ();
		Player player2 = GlobalContext.INSTANCE.getPlayer2 ();

		player1.InitialiseForGame (this, player1UI);
		player2.InitialiseForGame (this, player2UI);

		gameController.Initialise ();
	}

	public GameController getGameController() {
		return gameController;
	}

	public ActionRuleSet getRuleSet() {
		return actionRuleSet;
	}		
		 
}
