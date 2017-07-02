using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalContext : MonoBehaviour {

	public static GlobalContext INSTANCE;

	private Player player1;

	private Player player2;

	private HexBoard board;

	void Awake () {
		if (INSTANCE == null) {
			INSTANCE = this;
			DontDestroyOnLoad (INSTANCE);
		} else {
			Destroy (gameObject);
		}
	}

	//TODO InitialiseForGame()?
	public void Initialise() {
		
	}
		
	public void Reset() {
		
	}

	public void setPlayer1(Player player1) {
		if (this.player1 != null) {			
			Destroy (this.player1.gameObject);
		}
		this.player1 = Instantiate(player1, this.transform);
	}

	public void setPlayer2(Player player2) {
		if (this.player2 != null) {
			Destroy (this.player2.gameObject);
		}
		this.player2 = Instantiate(player2, this.transform);
	}

	public void setBoard(HexBoard board) {
		if (this.board != null) {
			Destroy (this.board.gameObject);
		}
		this.board = Instantiate(board, this.transform);
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

}
