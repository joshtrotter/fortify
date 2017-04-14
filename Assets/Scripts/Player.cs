using UnityEngine;

public abstract class Player : MonoBehaviour {

	private PlayerConfig playerConfig;
   
    private int claimedTileCount = 0;

	public Sprite PlayerSprite()
	{
		return playerConfig.PlayerSprite();
	}
		    
	public Sprite FortifySprite()
	{
		return playerConfig.FortifySprite();
	}

	public void SetPlayerConfig(PlayerConfig playerConfig)
	{
		this.playerConfig = playerConfig;
	}

	public abstract void StartTurn();    

    public abstract void OnTileSelected(HexTile tile);

    protected void PlayTile(HexTile tile)
    {
		GlobalContext.INSTANCE.getRuleSet().PlayTile(this, tile, () => EndTurn());
    }

    public virtual void EndTurn()
    {
		EventBus.INSTANCE.NotifyEndTurn (this);
    }

    public Player Opponent()
    {
		return GlobalContext.INSTANCE.getOpponentOf(this);
    }

    public int ClaimedTileCount()
    {
        return claimedTileCount;
    }

    public void AddClaimedTile()
    {
        ++claimedTileCount;
    }

    public void RemoveClaimedTile()
    {
        --claimedTileCount;
    }

}
