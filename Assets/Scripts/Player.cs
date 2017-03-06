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

	public IEnumerator HaveAiTurn()
	{
		HexTile chosenTile = behaviour.ChooseTile();
        chosenTile.GetComponent<SpriteRenderer>().color = Color.green;
        yield return new WaitForSeconds(2f);

        if (chosenTile.Available ()) {
			chosenTile.Claim(this);
		} else {
			chosenTile.Sacrifice();
		}
        GameController.Instance().EndCurrentTurn();
	}


}
