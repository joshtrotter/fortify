using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class SiegeController : MonoBehaviour, EndTurnListener, BoardReadyListener {

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
	private int turnsRemaining;

	[SerializeField]
	private Text turnsRemainingLabel;

	void Start () {
		EventBus.INSTANCE.DeregisterEndTurnListener (player1UI);
		EventBus.INSTANCE.DeregisterEndTurnListener (player2UI);

		EventBus.INSTANCE.RegisterEndTurnListener (this);
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
			if (tile.CompareTag ("claimed")) {
				tile.SetToState (HexTile.TileState.CLAIMED, player2, player2.PlayerSprite (), true);
				player2.AddClaimedTile(tile);
			} else if (tile.CompareTag ("fortified")) {
				tile.SetToState (HexTile.TileState.FORTIFIED, player2, player2.FortifySprite (), true);
				player2.AddClaimedTile(tile);
			}
		}
		player1UI.FadeIn ();
		player2UI.FadeOut ();
		player2UI.UpdateScore ();
		challengePanel.Initialise (0.7f);
	}

	public void OnChallengeAccepted()
	{
		FB.LogAppEvent ("StartSiegeChallenge");
		challengePanel.Hide (() => player1.StartTurn ());
	}

	public void OnEndTurn(Player player)
	{
		turnsRemainingLabel.text = "" + --turnsRemaining;
		player1UI.UpdateScore ();
		player2UI.UpdateScore ();
		if (CheckForEndOfGame())
		{
			DisplayEndOfGame();
		} 
		else
		{
			player.StartTurn ();
		}
	}	

	private bool CheckForEndOfGame()
	{
		return turnsRemaining == 0;
	}

	private void DisplayEndOfGame()
	{
		Dictionary<string, object> eventParams = new Dictionary<string, object> ();
		if (player2.ClaimedTileScore() == 0)
		{
			eventParams ["Outcome"] = "Success";
			notificationPanel.Reveal ("CHALLENGE COMPLETED!", () => {}, 0f, 0.15f, true);
		} else
		{
			eventParams ["Outcome"] = "Failure";
			notificationPanel.Reveal ("TRY AGAIN!", () => {}, 0f, 0.15f, true);
		}
		FB.LogAppEvent ("CompleteSiegeChallenge", parameters: eventParams);
	}

}
