using UnityEngine;

public abstract class Player : MonoBehaviour {

    private GameController gameController;

    private ActionRuleSet actionRuleSet;

    private Player opponent;

    [SerializeField]
    private Color color;

	[SerializeField]
	private Sprite playerSprite;

    [SerializeField]
    private Color fortifyColor;

	[SerializeField]
	private Sprite fortifySprite;

    private PlayerUI playerUI;

    private int claimedTileCount = 0;

	public Sprite PlayerSprite()
	{
		return playerSprite;
	}

    public Color PlayerColor()
    {
        return color;
    }

	public Sprite FortifySprite()
	{
		return fortifySprite;
	}

    public Color FortifyColor()
    {
        return fortifyColor;
    }

	public void SetOpponent(Player opponent) {
		this.opponent = opponent;
	}

	public virtual void InitialiseForGame(GameContext gameContext, PlayerUI playerUI) 
	{
		this.gameController = gameContext.getGameController ();
		this.actionRuleSet = gameContext.getRuleSet ();
		this.playerUI = playerUI;
	}

    public virtual void StartTurn()
    {
        playerUI.ShowTurnIndicator(true);
    }

    public abstract void OnTileSelected(HexTile tile);

    protected void PlayTile(HexTile tile)
    {
		actionRuleSet.PlayTile(this, tile, () => EndTurn());
    }

    public virtual void EndTurn()
    {
        playerUI.ShowTurnIndicator(false);
        gameController.OnEndTurn(this);
    }

    public Player Opponent()
    {
        return opponent;
    }

    public int ClaimedTileCount()
    {
        return claimedTileCount;
    }

    public void AddClaimedTile()
    {
        playerUI.DisplayScore(++claimedTileCount);
    }

    public void RemoveClaimedTile()
    {
        playerUI.DisplayScore(--claimedTileCount);
    }

}
