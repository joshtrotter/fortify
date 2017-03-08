using UnityEngine;

public abstract class Player : MonoBehaviour {

    [SerializeField]
    private ActionRuleSet actionRuleSet;

    [SerializeField]
    private Player opponent;

    [SerializeField]
    private Color color;

    [SerializeField]
    private Color fortifyColor;

    public Color PlayerColor()
    {
        return color;
    }

    public Color FortifyColor()
    {
        return fortifyColor;
    }

    public virtual void StartTurn()
    {
        GameController.Instance().turnIndicator.Claim(this);
    }

    public abstract void OnTileSelected(HexTile tile);

    protected void PlayTile(HexTile tile)
    {
        actionRuleSet.PlayTile(this, tile);
        EndTurn();
    }

    public virtual void EndTurn()
    {
        opponent.StartTurn();
    }

    public Player Opponent()
    {
        return opponent;
    }

}
