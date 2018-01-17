using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class SacrificeController : MonoBehaviour, CoinFlipListener, EndTurnListener, BoardReadyListener {

	[SerializeField]
	private Player player1;

	[SerializeField]
	private Player player2;

	[SerializeField]
	private SacrificePlayerUI player1UI;

	[SerializeField]
	private SacrificePlayerUI player2UI;

	[SerializeField]
	private ChallengePanel challengePanel;

	[SerializeField]
	private NotificationPanel notificationPanel;

	[SerializeField]
	private Coin coin;

	private int tileCount;

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
		tileCount = board.Tiles ().Count;
		challengePanel.Initialise (0.5f);
	}

	public void OnChallengeAccepted()
	{
		FB.LogAppEvent ("StartMartyrChallenge");
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
		return (player1.ClaimedTileScore() + player2.ClaimedTileScore() == tileCount);
	}

	private void DisplayEndOfGame()
	{
		Dictionary<string, object> eventParams = new Dictionary<string, object> ();
		if (player1.SacrificeScore() > player2.SacrificeScore())
		{
			eventParams ["Outcome"] = "Success";
			notificationPanel.Reveal ("CHALLENGE COMPLETED!", () => {}, 0f, 0.15f, true);
		} else
		{
			eventParams ["Outcome"] = "Failure";
			notificationPanel.Reveal ("TRY AGAIN!", () => {}, 0f, 0.15f, true);
		}
		FB.LogAppEvent ("CompleteMartyrChallenge", parameters: eventParams);
	}

}
