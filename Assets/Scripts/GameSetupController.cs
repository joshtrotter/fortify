using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSetupController : MonoBehaviour {

	[SerializeField]
	private Player defaultPlayer;

	[SerializeField]
	private Player defaultOpponent;

	[SerializeField]
	private List<HexBoard> boards;

	void Start() {
		GlobalContext.INSTANCE.setPlayer1 (defaultPlayer);
		GlobalContext.INSTANCE.setPlayer2 (defaultOpponent);
		GlobalContext.INSTANCE.setBoard (randomBoard());
	}

	public void SetOpponent(Player opponent) {
		GlobalContext.INSTANCE.setPlayer2 (opponent);
	}

	public void Play() {		
		GlobalContext.INSTANCE.getBoard ().gameObject.SetActive (true);
		SceneManager.LoadScene ("Game");
	}

	private HexBoard randomBoard() {
		int index = Random.Range (0, boards.Count);
		return boards [index];
	}
}
