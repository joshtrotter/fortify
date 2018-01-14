using UnityEngine;

public class HumanPlayer : Player, TileSelectionListener {

	private bool lockMovement = false;

    public override void StartTurn()
    {
		EventBus.INSTANCE.RegisterTileSelectionListener (this);
    }

    public override void OnTileSelected(HexTile tile)
    {
		if (!lockMovement) {
			if (ValidateTileSelection (tile)) {			
				lockMovement = true;
				PlayTile (tile);
			}
		}
    }

    public override void EndTurn()
    {		
		lockMovement = false;
		EventBus.INSTANCE.DeregisterTileSelectionListener (this);
        base.EndTurn();
    }

    //Check that the selected tile can either be claimed or sacrificed
    private bool ValidateTileSelection(HexTile tile)
    {		
		return tile.IsActivated() && (tile.Available() || (tile.CurrentOwner() == this && tile.Fortified()));
    }
}

