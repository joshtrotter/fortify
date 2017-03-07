using UnityEngine;

// Notifies the observingPlayer, if one has been registered, when a HexTile has been clicked on.
// The registered player will generally be the player whose turn it is, unless that is an AI player, 
// in which case there would be no observers. Decouples the HexTiles from knowing about the players.
public class HexTileSelectionObserver : MonoBehaviour {

    private Player observingPlayer;

    public void RegisterPlayer(Player player)
    {
        observingPlayer = player;
    }

    public void DeregisterPlayer(Player player)
    {
        if (observingPlayer == player)
        {
            observingPlayer = null;
        }
    }

    public void NotifyPlayerOfTileSelection(HexTile tile)
    {
        if (observingPlayer != null)
        {
            observingPlayer.OnTileSelected(tile);
        }
    }

}

