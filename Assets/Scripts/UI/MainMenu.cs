using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	[SerializeField]
	private Player player;

	[SerializeField]
	private Player opponentAI;

	[SerializeField]
	private Player opponentHuman;

	[SerializeField]
	private List<PlayerConfig> configs;

	[SerializeField]
	private List<HexBoard> boards;

	[SerializeField]
	private ActionRuleSet actionRuleSet;

	[SerializeField]
	private SoundController soundController;

	public void SinglePlayer() {
		AssignPlayerConfigs (opponentAI);
		GlobalContext.INSTANCE.setPlayer1 (player);
		GlobalContext.INSTANCE.setPlayer2 (opponentAI);
		GlobalContext.INSTANCE.setBoard (RandomBoard ());
		GlobalContext.INSTANCE.setActionRuleSet (actionRuleSet);
		GlobalContext.INSTANCE.setSoundController (soundController);
		SceneManager.LoadScene ("Game");
	}

	public void MultiPlayer() {
		AssignPlayerConfigs (opponentHuman);
		GlobalContext.INSTANCE.setPlayer1 (player);
		GlobalContext.INSTANCE.setPlayer2 (opponentHuman);
		GlobalContext.INSTANCE.setBoard (RandomBoard ());
		GlobalContext.INSTANCE.setActionRuleSet (actionRuleSet);
		GlobalContext.INSTANCE.setSoundController (soundController);
		SceneManager.LoadScene ("Game");
	}

	public void Tutorial() {	
		GlobalContext.INSTANCE.setActionRuleSet (actionRuleSet);
		GlobalContext.INSTANCE.setSoundController (soundController);	
		SceneManager.LoadScene ("Tutorial");
	}

	private void AssignPlayerConfigs(Player opponent)
	{
		PlayerConfig playerConfig = RandomConfig ();
		playerConfig.SetPlayerName ("PLAYER 1");
		player.SetPlayerConfig (playerConfig);

		PlayerConfig opponentConfig = RandomConfig ();
		while (opponentConfig == playerConfig) {
			opponentConfig = RandomConfig ();
		}
		opponentConfig.SetPlayerName ("PLAYER 2");
		opponent.SetPlayerConfig (opponentConfig);
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
