using UnityEngine;

public abstract class Player : MonoBehaviour {

	[SerializeField]
	private PlayerConfig playerConfig;
   
    private int claimedTileCount = 0;

	public virtual void Initialise()
	{

	}

	public virtual void Reset()
	{
		this.claimedTileCount = 0;
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
		GlobalContext.INSTANCE.getActionRuleSet().PlayTile(this, tile, () => EndTurn());
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
