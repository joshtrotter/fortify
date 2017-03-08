using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour {

    private enum TileState { AVAILABLE, CLAIMED, FORTIFIED_MINOR, FORTIFIED_MAJOR };

    [SerializeField]
    private HexTileSelectionObserver selectionObserver;

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

    public void Claim(Player player)
    {
        owner = player;
        currentState = TileState.CLAIMED;
        rend.color = player.PlayerColor();
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
        rend.color = owner.PlayerColor();
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

    public Player CurrentOwner()
    {
        return owner;
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
