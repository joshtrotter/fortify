using UnityEngine;
using System.Collections;

public class AIPlayer : Player {

    [SerializeField]
    private AIStrategy strategy;

	public override void InitialiseForGame (GameContext gameContext, PlayerUI playerUI)
	{
		base.InitialiseForGame (gameContext, playerUI);
		strategy.Initialise (this, GlobalContext.INSTANCE.getBoard ());
	}

    public override void StartTurn()
    {
        base.StartTurn();
        strategy.ChooseTile();
    }

    public override void OnTileSelected(HexTile tile)
    {
        StartCoroutine(ProcessAIMove(tile));     
    }

    private IEnumerator ProcessAIMove(HexTile tile)
    {
		yield return new WaitForSeconds (1f);
        PlayTile(tile);
    }

	public override void EndTurn()
	{
		base.EndTurn ();
	}


}
