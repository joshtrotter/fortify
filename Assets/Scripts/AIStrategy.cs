using UnityEngine;

public abstract class AIStrategy : MonoBehaviour {

	public Player player;
	public HexBoard board;

	public abstract void ChooseTile();

}