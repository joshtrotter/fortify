using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour {

    private enum TileState { AVAILABLE, CLAIMED, FORTIFIED, COMBO_FORTIFIED };

    [SerializeField]
    private HexTileSelectionObserver selectionObserver;

    private SpriteRenderer rend;
    private Player owner;
    private TileState currentState;
    private HashSet<HexTile> neighbours = new HashSet<HexTile>();
	private HashSet<HexTile> tilesInFortifiedCombo = new HashSet<HexTile>();

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

    public void Fortify()
    {
        currentState = TileState.FORTIFIED;
        rend.color = owner.FortifyColor();
    }

	public void ComboFortify(HashSet<HexTile> tiles)
    {
        currentState = TileState.COMBO_FORTIFIED;
		tilesInFortifiedCombo = tiles;
		rend.color = owner.ComboColor();
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

    public bool Fortified()
    {
        return currentState == TileState.FORTIFIED;
    }

    public bool ComboFortified()
    {
        return currentState == TileState.COMBO_FORTIFIED;
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

	public HashSet<HexTile> TilesInFortifiedCombo()
	{
		return tilesInFortifiedCombo;
	}
		
}
