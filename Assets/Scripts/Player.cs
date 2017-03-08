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
        GameController.Instance().turnIndicator.Claim(this);
    }

    public abstract void OnTileSelected(HexTile tile);

    protected void PlayTile(HexTile tile)
    {
        if (tile.Available())
        {
            Claim(tile);
        }
        else
        {
            Sacrifice(tile);
        }
        EndTurn();
    }

    public virtual void EndTurn()
    {
        opponent.StartTurn();
    }

    public void Claim(HexTile tile)
    {
        tile.Claim(this);
        foreach (HexTile neighbour in tile.Neighbours())
        {
            if (neighbour.CurrentOwner() == this)
            {
                InfluenceAllyTile(neighbour);
            }
            else if (neighbour.CurrentOwner() == opponent)
            {
                InfluenceOpponentTile(neighbour);
            }
        }
    }

    public void Sacrifice(HexTile tile)
    {
        tile.RemoveFortify();

        foreach (HexTile neighbour in tile.Neighbours())
        {
            if (neighbour.CurrentOwner() == opponent)
            {
                InfluenceOpponentTile(neighbour);
            }
        }
    }

    private void InfluenceAllyTile(HexTile ally)
    {
        if (ally.Claimed())
        {
            ally.ApplyMinorFortify();
        }
    }

    private void InfluenceOpponentTile(HexTile opponent)
    {
        if (opponent.Claimed())
        {
            opponent.Claim(this);
        } else if (opponent.FortifiedMinor())
        {
            opponent.RemoveFortify();
        }
    }   

}
