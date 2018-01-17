using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class ConquestController : MonoBehaviour, CoinFlipListener, EndTurnListener, BoardReadyListener {

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

	private int totalTileScore = 0;

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
		foreach (HexTile tile in board.Tiles()) {
			totalTileScore += tile.TileScore ();
		}
		challengePanel.Initialise (0.5f);
	}

	public void OnChallengeAccepted()
	{
		FB.LogAppEvent ("StartConquestChallenge");
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
		return (player1.ClaimedTileScore() + player2.ClaimedTileScore() == totalTileScore);
	}

	private void DisplayEndOfGame()
	{
		Dictionary<string, object> eventParams = new Dictionary<string, object> ();
		if (player1.ClaimedTileScore() > player2.ClaimedTileScore())
		{
			eventParams ["Outcome"] = "Success";
			notificationPanel.Reveal ("CHALLENGE COMPLETED!", () => {}, 0f, 0.15f, true);
		} else
		{
			eventParams ["Outcome"] = "Failure";
			notificationPanel.Reveal ("TRY AGAIN!", () => {}, 0f, 0.15f, true);
		}
		FB.LogAppEvent ("CompleteConquestChallenge", parameters: eventParams);
	}

}
