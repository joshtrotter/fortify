using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SiegeController : MonoBehaviour, EndTurnListener, BoardReadyListener {

	private Player player1;
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
		Initialise ();
	}

	public void Initialise() {
		GlobalContext.INSTANCE.Initialise ();
		player1 = GlobalContext.INSTANCE.getPlayer1 ();
		player2 = GlobalContext.INSTANCE.getPlayer2 ();      

		player1UI.InitialiseForPlayer (player1);
		player2UI.InitialiseForPlayer (player2);
		EventBus.INSTANCE.DeregisterEndTurnListener (player1UI);
		EventBus.INSTANCE.DeregisterEndTurnListener (player2UI);

		EventBus.INSTANCE.RegisterEndTurnListener (this);
		EventBus.INSTANCE.RegisterBoardReadyListener (this);
	}

	public void OnBoardReady()
	{
		foreach (HexTile tile in GlobalContext.INSTANCE.getBoard().Tiles()) {
			if (tile.CompareTag ("claimed")) {
				tile.SetToState (HexTile.TileState.CLAIMED, player2, player2.PlayerSprite (), true);
				player2.AddClaimedTile ();
			} else if (tile.CompareTag ("fortified")) {
				tile.SetToState (HexTile.TileState.FORTIFIED, player2, player2.FortifySprite (), true);
				player2.AddClaimedTile ();
			}
		}
		player1UI.FadeIn ();
		player2UI.FadeOut ();
		player2UI.UpdateScore ();
		challengePanel.Initialise (0.7f);
	}

	public void OnChallengeAccepted()
	{
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
		if (player2.ClaimedTileCount() == 0)
		{
			notificationPanel.Reveal ("Challenge Completed!", () => {}, 0f, 0.15f, true);
		} else
		{
			notificationPanel.Reveal ("Challenge Failed!", () => {}, 0f, 0.15f, true);
		}
	}

}
