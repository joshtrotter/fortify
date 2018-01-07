using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureController : MonoBehaviour, CoinFlipListener, EndTurnListener, BoardReadyListener {

	[SerializeField]
	private Player player1;

	[SerializeField]
	private Player player2;

	[SerializeField]
	private PlayerUI player1UI;

	[SerializeField]
	private PlayerUI player2UI;

	[SerializeField]
	private ChallengePanel challengePanel;

	[SerializeField]
	private NotificationPanel notificationPanel;

	[SerializeField]
	private Coin coin;

	private int capturePoints = 4;
	private HexBoard board;

	void Start () {
		EventBus.INSTANCE.RegisterEndTurnListener (this);
		EventBus.INSTANCE.RegisterCoinFlipListener (this);
		EventBus.INSTANCE.RegisterBoardReadyListener (this);

		Initialise ();
	}

	public void Initialise() {						
		player1UI.InitialiseForPlayer (player1);
		player2UI.InitialiseForPlayer (player2);
	}

	public void OnBoardReady(HexBoard board)
	{
		this.board = board;
		foreach (GameObject tile in GameObject.FindGameObjectsWithTag ("claimed")) {
			tile.GetComponent<HexTile>().SetToState (HexTile.TileState.CLAIMED, player2, player2.PlayerSprite(), true);
			player2.AddClaimedTile (tile.GetComponent<HexTile>());
		}

		challengePanel.Initialise (0.5f);
	}

	public void OnChallengeAccepted()
	{
		challengePanel.Hide (() => {coin.Toss (player1, player2);});
	}

	public void OnStartingPlayerChosen (Player player)
	{
		player.StartTurn();
	}

	public void OnEndTurn(Player player)
	{
		if (CheckForEndOfGame())
		{
			DisplayEndOfGame();
		} 
		else
		{
			notificationPanel.Hide (() => {});
			player.Opponent().StartTurn();
		}
	}	

	private bool CheckForEndOfGame()
	{
		return player1.ClaimedTileScore () == capturePoints || board.Tiles().TrueForAll((tile) => !tile.Available());
	}

	private void DisplayEndOfGame()
	{
		if (player1.ClaimedTileScore () == capturePoints)
		{
			notificationPanel.Reveal ("CHALLENGE COMPLETED!", () => {}, 0f, 0.15f, true);
		} else
		{
			notificationPanel.Reveal ("TRY AGAIN!", () => {}, 0f, 0.15f, true);
		}
	}

}
