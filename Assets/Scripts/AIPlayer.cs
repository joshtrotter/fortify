using UnityEngine;
using System.Collections;

public class AIPlayer : Player {

    [SerializeField]
    private AIStrategy strategy;

	public void Awake() {
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
		yield return new WaitForSeconds (1f);
        PlayTile(tile);
    }
		
}
