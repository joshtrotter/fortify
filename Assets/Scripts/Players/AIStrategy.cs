using UnityEngine;

public class AIStrategy : MonoBehaviour, BoardReadyListener {

	public float valueOfCapturableNeighbour = 1f;

	//Increase the preference towards fortifying, the variable portion scales with the value of the tile
	public float baseValueOfFortifiableNeighbour = 0.33f;
	public float variableValueOfFortifiableNeighbour = 1.2f;

	//Increase the preference towards defortifying, the variable portion scales with the value of the tile
	public float baseValueOfDefortifiableNeighbour = 0.33f;
	public float variableValueOfDefortifiableNeighbour = 1.5f;

	//Scales the fortifiable/defortifiable value of a neighbour tile based on whether that tiles sacrifice value is above or below expectation 
	public float expectedSacrificeValue = 1.5f;

	//Decreasing these values will cause AI to put less consideration into how the player might counterattack which should decrease difficulty
	public float maxPenaltyForAvailableNeighbour = 0.5f; 
	public float valueOfNoClaimableNeighbours = 1f;
	public float valueOfOneClaimableNeighbour = -0.5f;
	public float valueOfTwoClaimableNeighbours = 0.1f;
	public float valueOfThreeClaimableNeighbours = -0.1f;

	public float mistakeChance = 0.25f;
	public float mistakeThreshold = 0.8f;

	public float baseValueOfSacrifice = -0.5f;

	private Player player;
	private HexBoard board;

	void Awake() {
		EventBus.INSTANCE.RegisterBoardReadyListener (this);
	}

	public void Initialise(Player player)
	{
		this.player = player;
	}

	public void OnBoardReady(HexBoard board)
	{
		this.board = board;
	}

	public void ChooseTile() 
	{
		float mistakeScore = -99f;
		float topScore = -99f;
		HexTile mistakeTile = null;
		HexTile topTile = null;

		foreach (HexTile tile in board.Tiles())
		{
			float score = ScoreTile(tile);
			if (score > topScore) {
				mistakeScore = topScore;
				topScore = score;
				mistakeTile = topTile;
				topTile = tile;
			} else if (score > mistakeScore) {
				mistakeScore = score;
				mistakeTile = tile;
			}
		}

//		Debug.Log ("Mistake: " + mistakeScore);
//		Debug.DrawLine (mistakeTile.transform.position, mistakeTile.transform.position + new Vector3 (0f, 1f, 0f), Color.red, 5f);
//
//		Debug.Log ("Top: " + topScore);
//		Debug.DrawLine (topTile.transform.position, topTile.transform.position + new Vector3 (0f, 1f, 0f), Color.green, 5f);

		if (topScore - mistakeScore <= mistakeThreshold && Random.Range (0f, 1f) <= mistakeChance) {
			player.OnTileSelected (mistakeTile);
		} else {
			player.OnTileSelected(topTile);
		}			        
	}

	private float ScoreTile(HexTile tile)
	{
		if (tile.IsActivated ()) {
			if (tile.Available ()) {
				return ValueOfClaiming (tile);
			} else if (tile.Fortified () && tile.CurrentOwner () == player) {
				return ValueOfSacrificing (tile, player) + baseValueOfSacrifice;
			}
		}
		return 0f;
	}

	private float ValueOfClaiming(HexTile tile) 
	{
		float currentScore = tile.TileValue();
		int claimableNeighbours = 0;

		foreach (HexTile neighbour in tile.Neighbours()) 
		{
			if (neighbour.Available()) {
				claimableNeighbours++;
				currentScore -= ValueOfClaimableNeighbour(neighbour, tile);
			} else if (neighbour.CurrentOwner() == player && !neighbour.Fortified()) {
				currentScore += baseValueOfFortifiableNeighbour + (variableValueOfFortifiableNeighbour * (ValueOfSacrificing(neighbour, player) / expectedSacrificeValue));
			} else if (neighbour.CurrentOwner() != player && neighbour.Fortified()) {
				currentScore += baseValueOfDefortifiableNeighbour + (variableValueOfDefortifiableNeighbour * (ValueOfSacrificing(neighbour, player.Opponent()) / expectedSacrificeValue));
			} else if (neighbour.CurrentOwner() != player && !neighbour.Fortified()) {
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
				} else if (!neighbourOfNeighbour.Fortified()) {
					//If they own it but it isn't fortified then they would like to fortify it
					neighbourValue++;
				}
			}
		}

		//Note the max neighbour value would be 5
		return (neighbourValue / 5f) * maxPenaltyForAvailableNeighbour;
	}

	private float ValueOfSacrificing(HexTile tile, Player sacrificer) 
	{
		float score = 0f;
		foreach (HexTile neighbour in tile.Neighbours()) 
		{
			if (!neighbour.Available() && neighbour.CurrentOwner() != sacrificer) {
				if (neighbour.Fortified()) 
				{
					score += variableValueOfDefortifiableNeighbour;
				} else {
					score += valueOfCapturableNeighbour;
				}
			}
		}
        return score;
	}

}