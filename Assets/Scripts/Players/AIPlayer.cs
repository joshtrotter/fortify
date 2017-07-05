using UnityEngine;
using System.Collections;

public class AIPlayer : Player, CoinFlipListener, DifficultyChangeListener {

	private const int DEFAULT_DIFFICULTY = 4;

	[SerializeField]
	private float baseWaitTime = 1f;

	[SerializeField]
	private float firstTurnExtraWaitTime = 1.5f;

	[SerializeField]
	private AIDifficultyMappings strategyMappings;

    private AIStrategy strategy;

	private bool addInitialDelay = false;

	void Start()
	{
		if (PlayerPrefs.HasKey ("difficulty")) {
			SetStrategyForDifficulty (PlayerPrefs.GetInt ("difficulty"));
		} else {
			SetStrategyForDifficulty (DEFAULT_DIFFICULTY); 
		}

		EventBus.INSTANCE.RegisterCoinFlipListener (this);
		EventBus.INSTANCE.RegisterDifficultyChangeListener (this);
	}

	public void OnStartingPlayerChosen (Player player)
	{
		if (this == player) {			
			addInitialDelay = true;
		}
	}

	public void OnDifficultyChanged(int difficulty) {
		SetStrategyForDifficulty (difficulty);
	}
	
	private void SetStrategyForDifficulty(int difficulty) 
	{
		strategy = strategyMappings.StrategyForDifficulty (difficulty);
		Debug.Log (PlayerName () + " difficulty = " + strategy);
		strategy.Initialise (this);
	}

    public override void StartTurn()
    {
        strategy.ChooseTile();
    }

    public override void OnTileSelected(HexTile tile)
    {		
        StartCoroutine(ProcessAIMove(tile));     
    }

    private IEnumerator ProcessAIMove(HexTile tile)
    {
		float waitTime = baseWaitTime;
		if (addInitialDelay) {
			waitTime += firstTurnExtraWaitTime;
			addInitialDelay = false;
		}
		yield return new WaitForSeconds (waitTime);
        PlayTile(tile);
    }
		
}
