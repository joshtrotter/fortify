using UnityEngine;
using System.Collections;

public class AIPlayer : Player {

    [SerializeField]
    private AIStrategy strategy;

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
        tile.GetComponent<SpriteRenderer>().color = Color.green;
        yield return new WaitForSeconds(2f);
        PlayTile(tile);
    }

}
