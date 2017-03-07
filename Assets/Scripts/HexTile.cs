using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour {

    [SerializeField]
    private HexTileSelectionObserver selectionObserver;

    private SpriteRenderer rend;
    private Player owner;
    private bool fortified;

    private HashSet<HexTile> neighbours = new HashSet<HexTile>();

    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        selectionObserver.NotifyPlayerOfTileSelection(this);
    }

    public void AddNeighbour(HexTile neighbour)
    {
        neighbours.Add(neighbour);
    }

    public void Claim(Player player)
    {
        ChangeOwner(player);
        foreach (HexTile neighbour in neighbours)
        {
            neighbour.OnNeighbourChange(this);
        }
    }

    public void ChangeOwner(Player player)
    {
        owner = player;
        rend.color = player.PlayerColor();
    }

    public void Fortify()
    {
        rend.color = owner.FortifyColor();
        fortified = true;
    }

    public void Sacrifice()
    {
        RemoveFortify();

        foreach (HexTile neighbour in neighbours)
        {
            if (neighbour.CurrentOwner() != owner)
            {
                neighbour.OnNeighbourChange(this);
            }
        }
    }

    public void RemoveFortify()
    {
        rend.color = owner.PlayerColor();
        fortified = false;
    }

    //This is where the core game rules can be changed around
    public void OnNeighbourChange(HexTile changedTile)
    {
        FortifyOrFlipRules(changedTile);
    }

    public Player CurrentOwner()
    {
        return owner;
    }

    public bool Available()
    {
        return owner == null;
    }

    public bool Fortified()
    {
        return fortified;
    }

	public HashSet<HexTile> Neighbours() 
	{
		return neighbours;
	}

    private void FortifyOrFlipRules(HexTile changedTile)
    {
        if (owner != null)
        {
            if (owner == changedTile.CurrentOwner())
            {
                Fortify();
            } else
            {
                if (!Fortified())
                {
                    ChangeOwner(changedTile.CurrentOwner());
                } else
                {
                    RemoveFortify();
                }
            }
        }
    }

}
