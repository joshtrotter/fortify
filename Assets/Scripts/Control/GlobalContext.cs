using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalContext : MonoBehaviour {

	public static GlobalContext INSTANCE;

	[SerializeField]
	private Player player1;

	[SerializeField]
	private Player player2;

	[SerializeField]
	private HexBoard board;

	[SerializeField]
	private ActionRuleSet actionRuleSet;

	void Awake () {
		if (INSTANCE == null) {
			INSTANCE = this;
			DontDestroyOnLoad (INSTANCE);
		}
	}

	public void setPlayer1(Player player1) {
		this.player1 = player1;
	}

	public void setPlayer2(Player player2) {
		this.player2 = player2;
	}

	public void setBoard(HexBoard board) {
		this.board = board;
	}

	public Player getPlayer1() {
		return player1;
	}

	public Player getPlayer2() {
		return player2;
	}

	public HexBoard getBoard() {
		return board;
	}

	public ActionRuleSet getRuleSet() {
		return actionRuleSet;
	}

	public Player getOpponentOf(Player player) {
		return player == player1 ? player2 : player1;
	}

}
