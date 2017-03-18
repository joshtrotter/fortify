﻿public class StandardRuleSet : ActionRuleSet {

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
            if (neighbour.CurrentOwner() == player)
            {
                InfluenceAllyTile(player, neighbour);
            }
            else if (neighbour.CurrentOwner() == player.Opponent())
            {
                InfluenceOpponentTile(player, neighbour);
            }
        }
    }

    private void Sacrifice(Player player, HexTile tile)
    {
        tile.RemoveFortify();

        foreach (HexTile neighbour in tile.Neighbours())
        {
            if (neighbour.CurrentOwner() == player.Opponent())
            {
                InfluenceOpponentTile(player, neighbour);
            }
        }
    }

    private void InfluenceAllyTile(Player player, HexTile ally)
    {
        if (ally.Claimed())
        {
            ally.Fortify();
        }
    }

    private void InfluenceOpponentTile(Player player, HexTile opponent)
    {
        if (opponent.Claimed())
        {
            opponent.Claim(player);
            player.AddClaimedTile();
            player.Opponent().RemoveClaimedTile();
        }
        else if (opponent.Fortified())
        {
            opponent.RemoveFortify();
        }
    }
}
