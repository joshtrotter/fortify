using UnityEngine;
using System.Collections;

public class AIPlayer : Player, CoinFlipListener {

	[SerializeField]
	private float baseWaitTime = 1f;

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
		EventBus.INSTANCE.NotifyStartAnimation ();
        StartCoroutine(ProcessAIMove(tile));     
    }

    private IEnumerator ProcessAIMove(HexTile tile)
    {
		float waitTime = baseWaitTime;
		if (addInitialDelay) {
			waitTime += 2.5f;
			addInitialDelay = false;
		}
		yield return new WaitForSeconds (waitTime);
        PlayTile(tile);
		EventBus.INSTANCE.NotifyEndAnimation ();
    }
		
}
