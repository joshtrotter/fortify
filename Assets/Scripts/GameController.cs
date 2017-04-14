using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour, EndTurnListener {

    private Player player1;
    private Player player2;

	[SerializeField]
	private PlayerUI player1UI;

	[SerializeField]
	private PlayerUI player2UI;

    [SerializeField]
    private Text endOfGameMessage;

    private int tileCount;

	void Start () {
		Initialise ();
		StartGame ();
	}
	    
	public void Initialise() {
		player1 = GlobalContext.INSTANCE.getPlayer1 ();
		player2 = GlobalContext.INSTANCE.getPlayer2 ();
		tileCount = GlobalContext.INSTANCE.getBoard ().Tiles().Count;        

		player1UI.InitialiseForPlayer (player1);
		player2UI.InitialiseForPlayer (player2);
		EventBus.INSTANCE.RegisterEndTurnListener (this);
	}

	public void StartGame() 
	{
		player1.StartTurn();
	}
    
    public void OnEndTurn(Player player)
    {
        if (CheckForEndOfGame())
        {
            DisplayEndOfGame();
        } 
        else
        {
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
            endOfGameMessage.text = "Player 1 Wins!";
        } else
        {
            endOfGameMessage.text = "Player 2 Wins!";
        }
    }
	
}
