using UnityEngine;

public class HumanPlayer : Player {

    [SerializeField]
    private HexTileSelectionObserver hexTileSelectionObserver;

    public override void StartTurn()
    {
        base.StartTurn();
        hexTileSelectionObserver.RegisterPlayer(this);
    }

    public override void OnTileSelected(HexTile tile)
    {
        if (ValidateTileSelection(tile))
        {
            PlayTile(tile);
        }
    }

    public override void EndTurn()
    {
        hexTileSelectionObserver.DeregisterPlayer(this);
        base.EndTurn();
    }

    //Check that the selected tile can either be claimed or sacrificed
    private bool ValidateTileSelection(HexTile tile)
    {
        return tile.Available() || tile.ZoneControlledBy() == this || (tile.ClaimedBy() == this && tile.FortifiedMinor());
    }
}

