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

	void Awake () {
		if (INSTANCE == null) {
			INSTANCE = this;
			DontDestroyOnLoad (INSTANCE);
			setupOpponents ();
		}
	}

	public void setPlayer1(Player player1) {
		this.player1 = player1;
		setupOpponents ();
	}

	public void setPlayer2(Player player2) {
		this.player2 = player2;
		setupOpponents ();
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

	private void setupOpponents() {
		if (player1 != null) {
			player1.SetOpponent (player2);
		}
		if (player2 != null) {
			player2.SetOpponent (player1);
		}
	}

}
