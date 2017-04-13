using UnityEngine;

public class AIStrategy : MonoBehaviour {

	public float valueOfFortifiableNeighbour = 1.2f;
	public float valueOfCapturableNeighbour = 1f;
	public float valueOfDefortifiableNeighbour = 1.5f;
	public float maxPenaltyForAvailableNeighbour = 0.5f;
	public float valueOfNoClaimableNeighbours = 1f;
	public float valueOfOneClaimableNeighbour = -0.5f;
	public float valueOfTwoClaimableNeighbours = 0.1f;
	public float valueOfThreeClaimableNeighbours = -0.1f;

	public float baseValueOfSacrifice = -0.5f;

	private Player player;
	private HexBoard board;

	public void Initialise(Player player, HexBoard board)
	{
		this.player = player;
		this.board = board;
	}

	public void ChooseTile() 
	{
		//TODO deal with case where all tiles are scored zero or negative
		float topScore = 0f;
		HexTile topTile = null;
		foreach (HexTile tile in board.Tiles())
		{
			float score = ScoreTile(tile);
			if (score > topScore) {
				topScore = score;
				topTile = tile;
			}
		}

        player.OnTileSelected(topTile);
	}

	private float ScoreTile(HexTile tile)
	{
		if (tile.Available ()) {
			return ScoreClaimableTile (tile);
		} else if (tile.FortifiedMinor() && tile.CurrentOwner() == player) {
			return ScoreFortifiedTile(tile);
		}
		return 0f;
	}

	private float ScoreClaimableTile(HexTile tile) 
	{
		float currentScore = 0f;
		int claimableNeighbours = 0;

		foreach (HexTile neighbour in tile.Neighbours()) 
		{
			if (neighbour.Available()) {
				claimableNeighbours++;
				currentScore -= ValueOfClaimableNeighbour(neighbour, tile);
			} else if (neighbour.CurrentOwner() == player && !neighbour.FortifiedMinor()) {
				//TODO the value placed on fortifying should depend on the possible value of a sacrifice
				currentScore += valueOfFortifiableNeighbour;
			} else if (neighbour.CurrentOwner() != player && neighbour.FortifiedMinor()) {
				//TODO the value placed on defortifying should depend on the possible value of a sacrifice
				currentScore += valueOfDefortifiableNeighbour;
			} else if (neighbour.CurrentOwner() != player && !neighbour.FortifiedMinor()) {
				currentScore += valueOfCapturableNeighbour;
			}
		}

		if (claimableNeighbours == 0) 
		{
			currentScore += valueOfNoClaimableNeighbours;
		} else if (claimableNeighbours == 1) 
		{
			currentScore += valueOfOneClaimableNeighbour;
		} else if (claimableNeighbours == 2) 
		{
			currentScore += valueOfTwoClaimableNeighbours;
		} else if (claimableNeighbours == 3) 
		{
			currentScore += valueOfThreeClaimableNeighbours;
		}

		return currentScore;
	}

	//Claimable neighbours can be a liability as the opponent might capture the tile we are placing with them
	private float ValueOfClaimableNeighbour(HexTile neighbour, HexTile tile) 
	{
		float neighbourValue = 0f;
		//Calculate the current value of the neighbour tile to the opponent
		foreach (HexTile neighbourOfNeighbour in neighbour.Neighbours()) {
			if (neighbourOfNeighbour != tile && !neighbourOfNeighbour.Available()) {
				if (neighbourOfNeighbour.CurrentOwner() == player) {
					//If we own it then the other player would like to capture / defortify it
					neighbourValue++;
				} else if (!neighbourOfNeighbour.FortifiedMinor()) {
					//If they own it but it isn't fortified then they would like to fortify it
					neighbourValue++;
				}
			}
		}

		//Note the max neighbour value would be 5
		return (neighbourValue / 5f) * maxPenaltyForAvailableNeighbour;
	}

	private float ScoreFortifiedTile(HexTile tile) 
	{
		float score = baseValueOfSacrifice;
		foreach (HexTile neighbour in tile.Neighbours()) 
		{
			if (!neighbour.Available() && neighbour.CurrentOwner() != player) {
				if (neighbour.FortifiedMinor()) 
				{
					score += valueOfDefortifiableNeighbour;
				} else {
					score += valueOfCapturableNeighbour;
				}
			}
		}
        return score;
	}

}