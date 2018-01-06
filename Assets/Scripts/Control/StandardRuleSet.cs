using System;

public class StandardRuleSet : ActionRuleSet
{

	public override void PlayTile (Player player, HexTile tile, Action onComplete)
	{
		if (tile.Available ()) {
			Claim (player, tile, onComplete);
		} else {
			Sacrifice (player, tile, onComplete);
		}			
	}

	private void Claim (Player player, HexTile tile, Action onComplete)
	{
		tile.Claim (player, () => {
			player.AddClaimedTile ();
			foreach (HexTile neighbour in tile.Neighbours()) {
				if (neighbour.CurrentOwner () == player) {
					InfluenceAllyTile (player, neighbour);
				} else if (neighbour.CurrentOwner () == player.Opponent ()) {
					InfluenceOpponentTile (player, neighbour);
				}
			}
			if (onComplete != null) {
				onComplete ();
			}
		});
	}

	private void Sacrifice (Player player, HexTile tile, Action onComplete)
	{
		tile.Sacrifice (() => {
			player.ToggleSacrificing(true);
			foreach (HexTile neighbour in tile.Neighbours()) {
				if (neighbour.CurrentOwner () == player.Opponent ()) {
					InfluenceOpponentTile (player, neighbour);
				}
			}
			player.ToggleSacrificing(false);
			if (onComplete != null) {
				onComplete ();
			}
		});
	}

	private void InfluenceAllyTile (Player player, HexTile ally)
	{
		if (ally.Claimed ()) {
			ally.ApplyFortify ();
		}
	}

	private void InfluenceOpponentTile (Player player, HexTile opponent)
	{
		if (opponent.Claimed ()) {
			opponent.Claim (player);
			player.AddClaimedTile ();
			player.Opponent ().RemoveClaimedTile ();
		} else if (opponent.Fortified ()) {
			opponent.RemoveFortify ();
		}
	}
}
