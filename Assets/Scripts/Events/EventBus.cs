using UnityEngine;
using System;
using System.Collections.Generic;

public class EventBus : MonoBehaviour
{
	public static EventBus INSTANCE;

	private HashSet<EndTurnListener> endTurnListeners = new HashSet<EndTurnListener> ();
	private HashSet<TileSelectionListener> tileSelectionListeners = new HashSet<TileSelectionListener> ();
	private HashSet<CoinFlipListener> coinFlipListeners = new HashSet<CoinFlipListener> ();
	private HashSet<StartAnimationListener> startAnimationListeners = new HashSet<StartAnimationListener> ();
	private HashSet<EndAnimationListener> endAnimationListeners = new HashSet<EndAnimationListener> ();

	private int runningAnimationCount = 0;

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

	public void RegisterStartAnimationListener(StartAnimationListener listener) 
	{
		startAnimationListeners.Add (listener);
	}

	public void DeregisterStartAnimationListener(StartAnimationListener listener)
	{
		startAnimationListeners.Remove (listener);
	}

	public void NotifyStartAnimation()
	{
		runningAnimationCount++;
		foreach (StartAnimationListener listener in startAnimationListeners) {
			listener.OnAnimationStart ();
		}
	}

	public void RegisterEndAnimationListener(EndAnimationListener listener) 
	{
		endAnimationListeners.Add (listener);
	}

	public void DeregisterEndAnimationListener(EndAnimationListener listener)
	{
		endAnimationListeners.Remove (listener);
	}

	public void NotifyEndAnimation()
	{
		runningAnimationCount--;
		foreach (EndAnimationListener listener in endAnimationListeners) {
			listener.OnAnimationEnd (runningAnimationCount);
		}
	}

	public void Reset() 
	{
		endTurnListeners.Clear ();
		tileSelectionListeners.Clear ();
		coinFlipListeners.Clear ();
		startAnimationListeners.Clear ();
		endAnimationListeners.Clear ();
		runningAnimationCount = 0;
	}
}

