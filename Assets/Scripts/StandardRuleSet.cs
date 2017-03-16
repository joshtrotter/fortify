using UnityEngine;

public class StandardRuleSet : ActionRuleSet {

    public override void PlayTile(Player player, HexTile tile)
    {
        if (tile.Available())
        {
            Claim(player, tile);
        }
        else
        {
            Sacrifice(player, tile);
        }
    }

    private void Claim(Player player, HexTile tile)
    {
        tile.Claim(player);
        player.AddClaimedTile();
        foreach (HexTile neighbour in tile.Neighbours())
        {
            if (neighbour.ClaimedBy() == player)
            {
                InfluenceAllyTile(player, neighbour);
            }
            else if (neighbour.ClaimedBy() == player.Opponent())
            {
                InfluenceOpponentTile(player, neighbour);
            }
        }
		UpdateZoneOfControl (player, tile);
    }

    private void Sacrifice(Player player, HexTile tile)
    {
        tile.RemoveFortify();

        foreach (HexTile neighbour in tile.Neighbours())
        {
            if (neighbour.ClaimedBy() == player.Opponent())
            {
                InfluenceOpponentTile(player, neighbour);
            }
        }
    }

    private void InfluenceAllyTile(Player player, HexTile ally)
    {
        if (ally.Claimed())
        {
            ally.ApplyMinorFortify();
        }
    }

    private void InfluenceOpponentTile(Player player, HexTile opponent)
    {
        if (opponent.Claimed())
        {
            opponent.Claim(player);
			Debug.Log ("Rezoning Captured Tile Neighbours");
			UpdateZoneOfControl (player, opponent);
            player.AddClaimedTile();
            player.Opponent().RemoveClaimedTile();
        }
        else if (opponent.FortifiedMinor())
        {
            opponent.RemoveFortify();
        }
    }

	private void UpdateZoneOfControl(Player player, HexTile updated)
	{
		foreach (HexTile neighbour in updated.Neighbours())
		{
			if (neighbour.ClaimedBy() == null) {
				ZoneUnclaimedTile (player, neighbour);
			}
		}
	}

	private void ZoneUnclaimedTile(Player player, HexTile unclaimed)
    {
		unclaimed.Zone (player);
		foreach (HexTile neighbour in unclaimed.Neighbours())
		{
			if (neighbour.ClaimedBy () == player.Opponent ()) {
				unclaimed.RemoveZone ();
				break;
			}
		}
    }
}
