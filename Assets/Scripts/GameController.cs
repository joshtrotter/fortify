using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private Player player1;
    private Player player2;
    private HexBoard hexBoard;

    [SerializeField]
    private Text endOfGameMessage;

    private int tileCount;
	    
	public void Initialise() {
		player1 = GlobalContext.INSTANCE.getPlayer1 ();
		player2 = GlobalContext.INSTANCE.getPlayer2 ();
		hexBoard = GlobalContext.INSTANCE.getBoard ();
		tileCount = hexBoard.Tiles().Count;        
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
