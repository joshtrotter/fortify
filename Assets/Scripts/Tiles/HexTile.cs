using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

public class HexTile : MonoBehaviour {

    private enum TileState { AVAILABLE, CLAIMED, FORTIFIED };

    private Player owner;
	private TileAnimator tileAnimator;
    private TileState currentState;
    private HashSet<HexTile> neighbours = new HashSet<HexTile>();

    void Start()
    {
		tileAnimator = gameObject.GetComponent<TileAnimator> ();
        currentState = TileState.AVAILABLE;
    }

    void OnMouseDown()
    {
		EventBus.INSTANCE.NotifyTileSelection (this);        
    }

	public void Claim(Player player) 
	{
		Claim (player, null);
	}

    public void Claim(Player player, Action onComplete)
    {
		if (owner == player.Opponent()) {
			StartCoroutine(tileAnimator.FlipToSprite(player.PlayerSprite(), onComplete));
		} else {
			StartCoroutine(tileAnimator.ExpandToSprite(player.PlayerSprite(), onComplete));
		}

        owner = player;
        currentState = TileState.CLAIMED;

    }

	public void ApplyFortify() {
		ApplyFortify (null);
	}

	public void ApplyFortify(Action onComplete)
    {
        currentState = TileState.FORTIFIED;
		StartCoroutine(tileAnimator.ExpandToSprite(owner.FortifySprite(), onComplete));
    }

	public void RemoveFortify() {
		RemoveFortify (null);
	}
		    
	public void RemoveFortify(Action onComplete)
    {
        currentState = TileState.CLAIMED;
		StartCoroutine(tileAnimator.ExpandToSprite(owner.PlayerSprite(), onComplete));
    }

    public bool Available()
    {
        return currentState == TileState.AVAILABLE;
    }

    public bool Claimed()
    {
        return currentState == TileState.CLAIMED;
    }

    public bool Fortified()
    {
        return currentState == TileState.FORTIFIED;
    }

    public Player CurrentOwner()
    {
        return owner;
    }

    public void AddNeighbour(HexTile neighbour)
    {
        neighbours.Add(neighbour);
    }

    public HashSet<HexTile> Neighbours()
    {
        return neighbours;
    }



}
