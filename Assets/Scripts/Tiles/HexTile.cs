using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

[RequireComponent(typeof(SpriteContainer))]
public class HexTile : MonoBehaviour {

    private enum TileState { AVAILABLE, CLAIMED, FORTIFIED };

	[SerializeField]
	private Sprite defaultSprite;

    private Player owner;
	private SpriteAnimator tileAnimator;
	private TileState currentState = TileState.AVAILABLE;
    private HashSet<HexTile> neighbours = new HashSet<HexTile>();
	private bool activated = true;

    void Start()
    {
		tileAnimator = gameObject.GetComponent<SpriteAnimator> ();
    }

    void OnMouseDown()
    {
		EventBus.INSTANCE.NotifyTileSelection (this);        
    }

	public void Reset()
	{
		currentState = TileState.AVAILABLE;
		tileAnimator.SetSprite (defaultSprite);
		neighbours.Clear ();
		owner = null;
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

	public void Deactivate(Action onComplete) 
	{
		StartCoroutine(tileAnimator.TweenColor (new Color (1f, 1f, 1f, 0.5f), 0.25f, onComplete));
		activated = false;
	}

	public void Activate(Action onComplete) 
	{
		StartCoroutine(tileAnimator.TweenColor (new Color (1f, 1f, 1f, 1f), 0.25f, onComplete));
		activated = true;
	}

	public bool IsActivated() 
	{
		return activated;
	}

}
