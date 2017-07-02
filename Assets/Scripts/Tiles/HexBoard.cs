using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scans all HexTiles that are children of the HexBoard and associates the tile with it's neighbours.
// Allows new board layouts to be created in the editor without the need to manually configure neighbours on each tile.
public class HexBoard : MonoBehaviour {

    private const float maxDistanceToNeighbour = 1.25f;

    private List<HexTile> tiles;

	public void Initialise() 
	{
		gameObject.SetActive (true);
		tiles = new List<HexTile>(gameObject.GetComponentsInChildren<HexTile>());
		ConfigureTileNeighbours();
		StartCoroutine (DrawInTiles ());
	}

	public IEnumerator DrawInTiles() {
		tiles.Sort (delegate(HexTile x, HexTile y) {
			Vector3 t1 = x.transform.position;
			Vector3 t2 = y.transform.position;

			return t1.y == t2.y ? (int)(t1.x - t2.x) : (int)(t2.y - t1.y); 
		});

		foreach (HexTile tile in tiles)
		{
			tile.Initialise ();
			yield return new WaitForSeconds (0.025f);
		}
		yield return new WaitForSeconds (0.15f);
		EventBus.INSTANCE.NotifyBoardReady (this);
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
