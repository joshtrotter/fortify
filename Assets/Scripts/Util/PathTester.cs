using System.Collections;
using System.Collections.Generic;

public class PathTester {

	private List<HexTile> visited = new List<HexTile> ();
	private List<HexTile> selected = new List<HexTile> ();

	public List<HexTile> findConnection(Player player, HexTile source, HexTile dest) 
	{
		selected.Add (source);
		visited.Add (source);
		if (source == dest) {
			return selected;
		} else {
			foreach (HexTile neighbour in source.Neighbours()) {
				if (IsValidForConnection (player, neighbour)) {
					List<HexTile> connection = findConnection (player, neighbour, dest);
					if (connection != null) {
						return connection;
					}					
				}
			}
			selected.Remove (source);
			return null;
		}
	}

	private bool IsValidForConnection(Player player, HexTile tile) {
		return tile.CurrentOwner () == player && !visited.Contains (tile);
	}

}
