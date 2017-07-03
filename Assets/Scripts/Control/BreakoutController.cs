﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakoutController : MonoBehaviour, EndTurnListener, BoardReadyListener {

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

	private int tileCount;

	private HexTile source;
	private HexTile dest1;
	private HexTile dest2;


	void Start () {
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
		tileCount = board.Tiles ().Count;
		source = GameObject.FindGameObjectWithTag ("source").GetComponent<HexTile>();
		dest1 = GameObject.FindGameObjectWithTag ("dest1").GetComponent<HexTile>();
		dest2 = GameObject.FindGameObjectWithTag ("dest2").GetComponent<HexTile>();
		source.SetToState (HexTile.TileState.FORTIFIED, player1, player1.FortifySprite (), true);
		player1.AddClaimedTile ();
		challengePanel.Initialise (0.5f);
	}

	public void OnChallengeAccepted()
	{
		challengePanel.Hide (() => {player1.StartTurn (); EventBus.INSTANCE.NotifyCoinFlip(player1);});
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
		return (player1.ClaimedTileCount() + player2.ClaimedTileCount() == tileCount);
	}

	private void DisplayEndOfGame()
	{
		List<HexTile> path1 = new PathTester ().findConnection (player1, source, dest1);
		List<HexTile> path2 = new PathTester ().findConnection (player1, source, dest2);

		if (path1 != null || path2 != null)
		{
			foreach (HexTile tile in GlobalContext.INSTANCE.getBoard().Tiles()) {
				if ((path1 != null && path1.Contains(tile)) || (path2 != null && path2.Contains(tile))) {
					tile.Activate(() => {});
				} else {
					tile.Deactivate(() => {});
				}
			}
			notificationPanel.Reveal (player1.PlayerName () + " WINS!", () => {}, 0.25f, 0.15f, true);
		} else
		{
			notificationPanel.Reveal (player2.PlayerName () + " WINS!", () => {}, 0f, 0.15f, true);
		}
	}

}
