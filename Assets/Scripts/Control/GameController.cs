using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Facebook.Unity;

public class GameController : MonoBehaviour, EndTurnListener, CoinFlipListener, BoardReadyListener {

	[SerializeField]
    private Player player1;

	[SerializeField]
    private Player player2;

	[SerializeField]
	private PlayerUI player1UI;

	[SerializeField]
	private PlayerUI player2UI;

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
		coin.Toss (player1, player2);
	}

	public void OnStartingPlayerChosen (Player player)
	{
		LogStartEvent ();
		player.StartTurn();
	}
    
    public void OnEndTurn(Player player)
    {
        if (CheckForEndOfGame())
        {
            DisplayEndOfGame();
			LogEndEvent ();
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
        if (player1.ClaimedTileScore() > player2.ClaimedTileScore())
        {
			notificationPanel.Reveal (player1.PlayerName () + " WINS!", () => {}, 0f, 0.15f, true);
        } else
        {
			notificationPanel.Reveal (player2.PlayerName () + " WINS!", () => {}, 0f, 0.15f, true);
        }
    }

	private void LogStartEvent()
	{
		if (player2.GetType() == typeof(AIPlayer)) {
			FB.LogAppEvent ("StartSinglePlayerGame");
		} else {
			FB.LogAppEvent ("StartMultiPlayerGame");
		}			
	}

	private void LogEndEvent()
	{
		Dictionary<string, object> eventParams = new Dictionary<string, object> ();
		if (player1.ClaimedTileScore () > player2.ClaimedTileScore ()) {
			eventParams ["Winner"] = player1.PlayerName();
		} else {
			eventParams ["Winner"] = player2.PlayerName();
		}
		
		if (player2.GetType() == typeof(AIPlayer)) {
			FB.LogAppEvent ("CompleteSinglePlayerGame", parameters: eventParams);
		} else {
			FB.LogAppEvent ("CompleteMultiPlayerGame", parameters: eventParams);
		}			
	}
	
}
