using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour, EndTurnListener, CoinFlipListener, BoardReadyListener {

    private Player player1;
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
		Initialise ();
	}
	    
	public void Initialise() {
		GlobalContext.INSTANCE.Initialise ();
		player1 = GlobalContext.INSTANCE.getPlayer1 ();
		player2 = GlobalContext.INSTANCE.getPlayer2 ();
		tileCount = GlobalContext.INSTANCE.getBoard ().Tiles().Count;        

		player1UI.InitialiseForPlayer (player1);
		player2UI.InitialiseForPlayer (player2);

		EventBus.INSTANCE.RegisterEndTurnListener (this);
		EventBus.INSTANCE.RegisterCoinFlipListener (this);
		EventBus.INSTANCE.RegisterBoardReadyListener (this);
	}
		
	public void OnBoardReady()
	{
		coin.Toss (player1, player2);
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
        return (player1.ClaimedTileCount() + player2.ClaimedTileCount() == tileCount);
    }

    private void DisplayEndOfGame()
    {
        if (player1.ClaimedTileCount() > player2.ClaimedTileCount())
        {
			notificationPanel.Reveal (player1.PlayerName () + " Wins!", () => {}, 0f, 0.15f, true);
        } else
        {
			notificationPanel.Reveal (player2.PlayerName () + " Wins!", () => {}, 0f, 0.15f, true);
        }
    }
	
}
