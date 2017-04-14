using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSetupController : MonoBehaviour {

	[SerializeField]
	private Player player;

	[SerializeField]
	private Player opponent;

	[SerializeField]
	private PlayerConfig playerConfig;

	[SerializeField]
	private PlayerConfig opponentConfig;

	[SerializeField]
	private List<HexBoard> boards;

	void Start() {
		//Randomise Defaults?
	}

	public void SetPlayerConfig(PlayerConfig config) {
		this.playerConfig = config;
	}

	public void SetOpponent(Player opponent) {
		this.opponent = opponent;
	}

	public void SetOpponentConfig(PlayerConfig config) {
		this.opponentConfig = config;
	}

	public void Play() {		
		InitialiseGlobalContext ();
		GlobalContext.INSTANCE.getBoard ().gameObject.SetActive (true);
		SceneManager.LoadScene ("Game");
	}
		
	private void InitialiseGlobalContext() {
		player.SetPlayerConfig (playerConfig);
		opponent.SetPlayerConfig (opponentConfig);
		GlobalContext.INSTANCE.setPlayer1 (player);
		GlobalContext.INSTANCE.setPlayer2 (opponent);
		GlobalContext.INSTANCE.setBoard (RandomBoard());
	}

	private HexBoard RandomBoard() {
		int index = Random.Range (0, boards.Count);
		return boards [index];
	}
}
