using UnityEngine;

public abstract class Player : MonoBehaviour {

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
        GameController.Instance().turnIndicator.ChangeOwner(this);
    }

    public abstract void OnTileSelected(HexTile tile);

    public virtual void EndTurn()
    {
        opponent.StartTurn();
    }

    protected void PlayTile(HexTile tile)
    {
        if (tile.Available())
        {
            tile.Claim(this);
        }
        else
        {
            tile.Sacrifice();
        }
        EndTurn();
    }
    


}
