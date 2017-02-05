using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexBoard : MonoBehaviour {

    private const float maxDistanceToNeighbour = 1.05f;

    private List<HexTile> tiles;

	void Start () {
        tiles = new List<HexTile>(gameObject.GetComponentsInChildren<HexTile>());
        ConfigureTileNeighbours();	
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
