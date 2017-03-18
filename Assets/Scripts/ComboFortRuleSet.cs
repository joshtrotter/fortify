using System.Collections.Generic;

public class ComboFortRuleSet : ActionRuleSet {

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
        
		List<HexTile> neighbours = new List<HexTile>(tile.Neighbours ());

		//Combine with ally neighbour tiles to form either minor forts or a combo fort
		List<HexTile> influencableAllyNeighbours = neighbours.FindAll (t => t.CurrentOwner () == player && !t.ComboFortified());
		if (CanFormComboFort (influencableAllyNeighbours)) {
			HashSet<HexTile> comboTiles = new HashSet<HexTile> (influencableAllyNeighbours);
			comboTiles.Add (tile);
			FormComboFort (comboTiles);
		} else if (influencableAllyNeighbours.Count > 0) {
			FormMinorForts (tile, influencableAllyNeighbours);
		}

		//Influence neighbour tiles claimed by the opponent player
		neighbours
			.FindAll (t => t.CurrentOwner () == player.Opponent())
			.ForEach(t => InfluenceOpponentTile(player, t));					
    }

	private bool CanFormComboFort(List<HexTile> allyNeighbours)
	{
		//Can't form a combo fort with less than 2 neighbours
		if (allyNeighbours.Count < 2) {
			return false;
		//Can always form a combo fort with more than 2 neighbours
		} else if (allyNeighbours.Count > 2) {
			return true;
		//Can only form a combo fort with exactly 2 neighbours if those neighbours aren't neighbours of eachother
		} else {
			HexTile firstNeighbour = allyNeighbours [0];
			HexTile secondNeighbour = allyNeighbours [1];
			return !firstNeighbour.Neighbours ().Contains (secondNeighbour);
		}
	}

	private void FormComboFort(HashSet<HexTile> comboTiles)
	{		
		foreach (HexTile comboTile in comboTiles) {
			comboTile.ComboFortify (comboTiles);
		}
	}

	private void FormMinorForts(HexTile tile, List<HexTile> neighbours)
	{
		tile.Fortify ();
		neighbours.ForEach (n => n.Fortify ());
	}

    private void Sacrifice(Player player, HexTile tile)
    {        
		foreach (HexTile comboTile in tile.TilesInFortifiedCombo()) 
		{
			foreach (HexTile neighbour in comboTile.Neighbours())
			{
				if (neighbour.CurrentOwner() == player.Opponent())
				{
					InfluenceOpponentTile(player, neighbour);
				}
			}
			comboTile.RemoveFortify ();
		}
		tile.TilesInFortifiedCombo ().Clear ();
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
