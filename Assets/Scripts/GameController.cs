using UnityEngine;

public class GameController : MonoBehaviour {

    private static GameController INSTANCE;

    public Player player1;
    public Player player2;
    public HexTile turnIndicator;

    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }       
    }

    private void Start()
    {
        player1.StartTurn();
    }

	public static GameController Instance()
    {
        return INSTANCE;
    }	
	
}
