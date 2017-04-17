using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	[SerializeField]
	private Player opponentAI;

	[SerializeField]
	private Player opponentHuman;

	[SerializeField]
	private List<PlayerConfig> configs;

	[SerializeField]
	private List<HexBoard> boards;

	public void SinglePlayer() {
		opponentAI.SetPlayerConfig (RandomConfig ());
		GlobalContext.INSTANCE.setPlayer2 (opponentAI);
		GlobalContext.INSTANCE.setBoard (RandomBoard ());
		SceneManager.LoadScene ("Game");
	}

	public void MultiPlayer() {
		opponentHuman.SetPlayerConfig (RandomConfig ());
		GlobalContext.INSTANCE.setPlayer2 (opponentHuman);
		GlobalContext.INSTANCE.setBoard (RandomBoard ());
		SceneManager.LoadScene ("Game");
	}

	private HexBoard RandomBoard() {
		int index = Random.Range (0, boards.Count);
		return boards [index];
	}

	private PlayerConfig RandomConfig() {
		int index = Random.Range (0, configs.Count);
		return configs [index];
	}


}
