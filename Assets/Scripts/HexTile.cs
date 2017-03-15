using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour {

    private enum TileState { AVAILABLE, ZONED, CLAIMED, FORTIFIED_MINOR, FORTIFIED_MAJOR };

    [SerializeField]
    private HexTileSelectionObserver selectionObserver;

    [SerializeField]
    private Color availableColor = Color.white;

    private SpriteRenderer rend;
    private Player owner;
    private TileState currentState;
    private HashSet<HexTile> neighbours = new HashSet<HexTile>();

    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
        currentState = TileState.AVAILABLE;
    }

    void OnMouseDown()
    {
        selectionObserver.NotifyPlayerOfTileSelection(this);
    }

    public void Zone(Player player)
    {
        owner = player;
        currentState = TileState.ZONED;
        rend.color = player.ZoneColor();
    }

    public void RemoveZone()
    {
        owner = null;
        currentState = TileState.AVAILABLE;
        rend.color = availableColor;
    }

    public void Claim(Player player)
    {
        owner = player;
        currentState = TileState.CLAIMED;
        rend.color = player.ClaimColor();
    }

    public void ApplyMinorFortify()
    {
        currentState = TileState.FORTIFIED_MINOR;
        rend.color = owner.FortifyColor();
    }

    public void ApplyMajorFortify()
    {
        currentState = TileState.FORTIFIED_MAJOR;
        rend.color = owner.FortifyColor();
    }

    public void RemoveFortify()
    {
        currentState = TileState.CLAIMED;
        rend.color = owner.ClaimColor();
    }

    public bool Available()
    {
        return currentState == TileState.AVAILABLE;
    }

    public bool Claimed()
    {
        return currentState == TileState.CLAIMED;
    }

    public bool FortifiedMinor()
    {
        return currentState == TileState.FORTIFIED_MINOR;
    }

    public bool FortifiedMajor()
    {
        return currentState == TileState.FORTIFIED_MAJOR;
    }

    public Player ZoneControlledBy()
    {
        if (currentState == TileState.ZONED)
        {
            return owner;
        }
        else
        {
            return null;
        }
    }

    public Player ClaimedBy()
    {
        if (currentState == TileState.ZONED)
        {
            return null;
        }
        else
        {
            return owner;
        }
    }

    public void AddNeighbour(HexTile neighbour)
    {
        neighbours.Add(neighbour);
    }

    public HashSet<HexTile> Neighbours()
    {
        return neighbours;
    }

}
