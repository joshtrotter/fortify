using UnityEngine;

public class HumanPlayer : Player, TileSelectionListener {

    public override void StartTurn()
    {
		EventBus.INSTANCE.RegisterTileSelectionListener (this);
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
		EventBus.INSTANCE.DeregisterTileSelectionListener (this);
        base.EndTurn();
    }

    //Check that the selected tile can either be claimed or sacrificed
    private bool ValidateTileSelection(HexTile tile)
    {
        return tile.Available() || (tile.CurrentOwner() == this && tile.FortifiedMinor());
    }
}

