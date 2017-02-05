using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour {

    public bool allowDefortify = true;
    public bool allowMartyr = true;

    private GameController gc;
    private SpriteRenderer rend;
    private Player owner;
    private bool fortified;

    private HashSet<HexTile> neighbours = new HashSet<HexTile>();

    void Start()
    {
        gc = GameController.Instance();
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        if (Available())
        {
            ClaimForPlayer(gc.CurrentPlayer());
            foreach (HexTile neighbour in neighbours)
            {
                neighbour.OnNeighbourChange(this);
            }
            gc.EndCurrentTurn();
        } else if (CurrentOwner() == gc.CurrentPlayer() && Fortified() && allowMartyr)
        {
            Defortify();
            foreach (HexTile neighbour in neighbours)
            {
                if (neighbour.CurrentOwner() != owner)
                {
                    neighbour.OnNeighbourChange(this);
                }
            }
            gc.EndCurrentTurn();
        }
    }

    public void AddNeighbour(HexTile neighbour)
    {
        neighbours.Add(neighbour);
    }

    public void ClaimForPlayer(Player player)
    {
        owner = player;
        rend.color = player.PlayerColor();
    }

    public void Fortify()
    {
        rend.color = owner.FortifyColor();
        fortified = true;
    }

    public void Defortify()
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
                    ClaimForPlayer(changedTile.CurrentOwner());
                } else if (allowDefortify)
                {
                    Defortify();
                }
            }
        }
    }

    private void ConvertBasedOnNeighbourCountRules()
    {
        if (owner != null)
        {
            int currentPlayerStrength = 1;
            int otherPlayerStrength = 0;
            foreach (HexTile neighbour in neighbours)
            {
                Player neighbourOwner = neighbour.CurrentOwner();
                if (neighbourOwner != null)
                {
                    if (neighbourOwner == owner)
                    {
                        currentPlayerStrength += 1;
                    }
                    else
                    {
                        otherPlayerStrength += 1;
                    }
                }
            }
            if (otherPlayerStrength > currentPlayerStrength)
            {
                ClaimForPlayer(owner == gc.player1 ? gc.player2 : gc.player1);
            }
        }
    }
}
