using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class CrossingController : MonoBehaviour, CoinFlipListener, EndTurnListener, BoardReadyListener {

	[SerializeField]
	private Player player1;

	[SerializeField]
	private Player player2;

	[SerializeField]
	private CrossingPlayerUI player1UI;

	[SerializeField]
	private CrossingPlayerUI player2UI;

	[SerializeField]
	private ChallengePanel challengePanel;

	[SerializeField]
	private NotificationPanel notificationPanel;

	[SerializeField]
	private Coin coin;

	[SerializeField]
	private int islands = 2;

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
		challengePanel.Initialise (0.5f);
	}

	public void OnChallengeAccepted()
	{
		FB.LogAppEvent ("StartCrossingChallenge");
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
		return (player1.ClaimedTileScore() + player2.ClaimedTileScore() == board.Tiles().Count);
	}

	private void DisplayEndOfGame()
	{
		int crossingScore = 0;
		for (int i = 1; i <= islands; i++) {
			int playerIslandScore = 0;
			int opponentIslandScore = 0;
			foreach (HexTile tile in board.Tiles()) {
				if (tile.tag == "island" + i) {
					if (tile.CurrentOwner () == player1) {
						++playerIslandScore;
					} else if (tile.CurrentOwner () == player2) {
						++opponentIslandScore;
					}
				}
			}
			if (playerIslandScore > opponentIslandScore) {
				++crossingScore;
			}
		}

		Dictionary<string, object> eventParams = new Dictionary<string, object> ();
		if (crossingScore == islands)
		{
			eventParams ["Outcome"] = "Success";
			notificationPanel.Reveal ("CHALLENGE COMPLETED!", () => {}, 0f, 0.15f, true);
		} else
		{
			eventParams ["Outcome"] = "Failure";
			notificationPanel.Reveal ("TRY AGAIN!", () => {}, 0f, 0.15f, true);
		}
		FB.LogAppEvent ("CompleteCrossingChallenge", parameters: eventParams);
	}

}
