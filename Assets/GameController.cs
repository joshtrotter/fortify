using System.Collections;
using System.Collections.Generic;
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
        UpdateTurnIndicator();
    }

	public static GameController Instance()
    {
        return INSTANCE;
    }

    public Player CurrentPlayer()
    {
        return player1.IsTurn() ? player1 : player2;
    }

    public void EndCurrentTurn()
    {
        player1.SwitchTurns();
        player2.SwitchTurns();
        UpdateTurnIndicator();
    }

    private void UpdateTurnIndicator()
    {
        turnIndicator.ClaimForPlayer(CurrentPlayer());
    }
	
	
}
