using UnityEngine;
using System.Collections;

public class AIPlayer : Player, CoinFlipListener {

	[SerializeField]
	private float baseWaitTime = 1f;

	[SerializeField]
	private float firstTurnExtraWaitTime = 1.5f;

    [SerializeField]
    private AIStrategy strategy;

	private bool addInitialDelay = false;

	public override void Initialise()
	{
		base.Initialise ();
		strategy.Initialise (this);
		EventBus.INSTANCE.RegisterCoinFlipListener (this);
	}

	public override void Reset()
	{
		base.Reset ();
		this.addInitialDelay = false;
		StopAllCoroutines ();
	}

	public void OnStartingPlayerChosen (Player player)
	{
		if (this == player) {			
			addInitialDelay = true;
		}
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
