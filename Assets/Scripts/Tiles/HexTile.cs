﻿using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;


[RequireComponent(typeof(SpriteAnimator))]
public class HexTile : MonoBehaviour {

    public enum TileState { AVAILABLE, CLAIMED, FORTIFIED };

	[SerializeField]
	private Sprite defaultSprite;

	[SerializeField]
	private float baseValue = 0f;

	[SerializeField]
	private float randomValueVariance = 0f;

	[SerializeField]
	private int tileScore = 1;

    private Player owner;
	private SpriteAnimator tileAnimator;
	private TileState currentState = TileState.AVAILABLE;
    private HashSet<HexTile> neighbours = new HashSet<HexTile>();
	private bool activated = true;

    void Awake()
    {		
		tileAnimator = gameObject.GetComponent<SpriteAnimator> ();
    }

    void OnMouseDown()
    {
		if (Time.timeScale != 0f) {
			EventBus.INSTANCE.NotifyTileSelection (this);        
		}
    }

	public void Initialise()
	{		
		SoundController.INSTANCE.PlayClaim ();
		tileAnimator.AnimateExpand (Vector3.zero, Vector3.one, 0.2f, () => {GetComponent<PolygonCollider2D>().enabled = true;});
	}

	public void SetToState(TileState state, Player owner, Sprite sprite, bool activated)
	{
		this.currentState = state;
		this.owner = owner;
		this.activated = activated;
		tileAnimator.AnimateExpandToSprite (sprite, () => {});
	}

	public void Claim(Player player) 
	{
		Claim (player, null);
	}

    public void Claim(Player player, Action onComplete)
    {		
		if (owner == player.Opponent()) {
			SoundController.INSTANCE.PlayCapture ();
			StartCoroutine(tileAnimator.FlipToSprite(player.PlayerSprite(), onComplete));
		} else {
			SoundController.INSTANCE.PlayClaim ();
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
		SoundController.INSTANCE.PlayFortify ();
		StartCoroutine(tileAnimator.ExpandToSprite(owner.FortifySprite(), onComplete));
    }

	public void RemoveFortify() {
		RemoveFortify (null);
	}
		    
	public void RemoveFortify(Action onComplete)
    {
        currentState = TileState.CLAIMED;
		SoundController.INSTANCE.PlayDefortify ();
		StartCoroutine(tileAnimator.ExpandToSprite(owner.PlayerSprite(), onComplete));
    }

	public void Sacrifice() {
		Sacrifice (null);
	}

	public void Sacrifice(Action onComplete)
	{
		currentState = TileState.CLAIMED;
		SoundController.INSTANCE.PlaySacrifice ();
		StartCoroutine(tileAnimator.SacrificeAnimation(owner.PlayerSprite(), onComplete, 0.33f, 0.2f));
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

	public float TileValue()
	{
		return baseValue + UnityEngine.Random.Range (0f, randomValueVariance);
	}

	public int TileScore()
	{
		return tileScore;
	}

}
