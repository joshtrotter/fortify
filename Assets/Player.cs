using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Color color;
    public Color fortifyColor;

	public bool ai;
	public AI behaviour;

    public bool isTurn;

    public Color PlayerColor()
    {
        return color;
    }

    public Color FortifyColor()
    {
        return fortifyColor;
    }

    public bool IsTurn()
    {
        return isTurn;
    }

    public void SwitchTurns()
    {
        isTurn = !isTurn;
    }

	public bool IsAi() 
	{
		return ai;
	}

	public void HaveAiTurn()
	{
		HexTile chosenTile = behaviour.ChooseTile();
		if (chosenTile.Available ()) {
			chosenTile.ClaimForPlayer(this);
		} else {
			chosenTile.Fortify();
		}
	}


}
