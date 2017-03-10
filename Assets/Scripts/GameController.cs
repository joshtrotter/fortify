using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private static GameController INSTANCE;

    public Player player1;
    public Player player2;
    public HexBoard hexBoard;

    [SerializeField]
    private Text endOfGameMessage;

    private int tileCount;

    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }       
    }

    private void Start()
    {
        tileCount = hexBoard.Tiles().Count;
        player1.StartTurn();
    }

	public static GameController Instance()
    {
        return INSTANCE;
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
