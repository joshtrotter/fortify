﻿using UnityEngine;

public abstract class Player : MonoBehaviour {

	[SerializeField]
	private PlayerConfigSelector configSelector;

	private PlayerConfig playerConfig;

	[SerializeField]
	private Player opponent;

	[SerializeField]
	private ActionRuleSet actionRuleSet;

	[SerializeField]
	private string playerName;
   
    private int claimedTileCount = 0;

	private bool isSacrificing = false;
	private int sacrificeScore = 0;

	void Awake() {
		playerConfig = configSelector.GetSelectedConfig ();
		playerConfig.SetPlayerName (playerName);
	}

	public Sprite PlayerSprite()
	{
		return playerConfig.PlayerSprite();
	}
		    
	public Sprite FortifySprite()
	{
		return playerConfig.FortifySprite();
	}

	public string PlayerName()
	{
		return playerConfig.PlayerName ();
	}

	public void SetPlayerConfig(PlayerConfig playerConfig)
	{
		this.playerConfig = Instantiate(playerConfig, this.transform);
	}

	public abstract void StartTurn();    

    public abstract void OnTileSelected(HexTile tile);

    protected void PlayTile(HexTile tile)
    {		
		actionRuleSet.PlayTile(this, tile, () => EndTurn());
    }

    public virtual void EndTurn()
    {
		EventBus.INSTANCE.NotifyEndTurn (this);
    }

    public Player Opponent()
    {
		return opponent;
    }

    public int ClaimedTileCount()
    {
        return claimedTileCount;
    }

	public int SacrificeScore()
	{
		return sacrificeScore;
	}

    public void AddClaimedTile()
    {		
        ++claimedTileCount;
		if (isSacrificing) {
			++sacrificeScore;
		}
    }

    public void RemoveClaimedTile()
    {
        --claimedTileCount;
    }

	public void ToggleSacrificing(bool isSacrificing) {
		this.isSacrificing = isSacrificing;
	}

}
