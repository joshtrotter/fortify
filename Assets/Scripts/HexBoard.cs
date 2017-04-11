using System.Collections.Generic;
using UnityEngine;

// Scans all HexTiles that are children of the HexBoard and associates the tile with it's neighbours.
// Allows new board layouts to be created in the editor without the need to manually configure neighbours on each tile.
public class HexBoard : MonoBehaviour {

    private const float maxDistanceToNeighbour = 1.25f;

    private List<HexTile> tiles;

	void Start () {
        tiles = new List<HexTile>(gameObject.GetComponentsInChildren<HexTile>());
        ConfigureTileNeighbours();	
	}

    public List<HexTile> Tiles()
    {
        return tiles;
    }

    private void ConfigureTileNeighbours()
    {
        foreach (HexTile tile in tiles)
        {
            foreach (HexTile candidate in tiles)
            {
                if (AreTilesNeighbours(tile, candidate))
                {
                    tile.AddNeighbour(candidate);
                }
            }
        }
    }

    private bool AreTilesNeighbours(HexTile tile1, HexTile tile2)
    {
        if (tile1.Equals(tile2))
        {
            return false;
        }
        return Vector2.Distance(tile1.transform.position, tile2.transform.position) <= maxDistanceToNeighbour;
    }
	 
}
