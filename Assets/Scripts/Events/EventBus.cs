using UnityEngine;
using System;
using System.Collections.Generic;

public class EventBus : MonoBehaviour
{
	public static EventBus INSTANCE;

	private HashSet<EndTurnListener> endTurnListeners = new HashSet<EndTurnListener> ();
	private HashSet<TileSelectionListener> tileSelectionListeners = new HashSet<TileSelectionListener> ();
	private HashSet<CoinFlipListener> coinFlipListeners = new HashSet<CoinFlipListener> ();

	void Awake () {
		if (INSTANCE == null) {
			INSTANCE = this;
			DontDestroyOnLoad (INSTANCE);
		}
	}		

	public void RegisterEndTurnListener(EndTurnListener listener) 
	{
		endTurnListeners.Add (listener);
	}

	public void DeregisterEndTurnListener(EndTurnListener listener)
	{
		endTurnListeners.Remove (listener);
	}

	public void NotifyEndTurn(Player player)
	{
		foreach (EndTurnListener listener in endTurnListeners) {
			listener.OnEndTurn (player);
		}
	}

	public void RegisterTileSelectionListener(TileSelectionListener listener) 
	{
		tileSelectionListeners.Add (listener);
	}

	public void DeregisterTileSelectionListener(TileSelectionListener listener)
	{
		tileSelectionListeners.Remove (listener);
	}

	public void NotifyTileSelection(HexTile tile)
	{
		foreach (TileSelectionListener listener in tileSelectionListeners) {
			listener.OnTileSelected (tile);
		}
	}

	public void RegisterCoinFlipListener(CoinFlipListener listener) 
	{
		coinFlipListeners.Add (listener);
	}

	public void DeregistercoinflipListener(CoinFlipListener listener)
	{
		coinFlipListeners.Remove (listener);
	}

	public void NotifyCoinFlip(Player winner)
	{
		foreach (CoinFlipListener listener in coinFlipListeners) {
			listener.OnStartingPlayerChosen (winner);
		}
	}
}

