using UnityEngine;
using System;
using System.Collections.Generic;

public class EventBus : MonoBehaviour
{
	public static EventBus INSTANCE;

	private HashSet<EndTurnListener> endTurnListeners = new HashSet<EndTurnListener> ();
	private HashSet<TileSelectionListener> tileSelectionListeners = new HashSet<TileSelectionListener> ();
	private HashSet<CoinFlipListener> coinFlipListeners = new HashSet<CoinFlipListener> ();
	private HashSet<BoardReadyListener> boardReadyListeners = new HashSet<BoardReadyListener> ();
	private HashSet<SfxToggleListener> sfxToggleListeners = new HashSet<SfxToggleListener> ();
	private HashSet<DifficultyChangeListener> difficultyChangeListeners = new HashSet<DifficultyChangeListener> ();

	void Awake () {
		if (INSTANCE == null) {
			INSTANCE = this;
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

	public void RegisterBoardReadyListener(BoardReadyListener listener) 
	{
		boardReadyListeners.Add (listener);
	}

	public void DeregisterBoardReadyListener(BoardReadyListener listener)
	{
		boardReadyListeners.Remove (listener);
	}

	public void NotifyBoardReady(HexBoard board)
	{
		foreach (BoardReadyListener listener in boardReadyListeners) {
			listener.OnBoardReady (board);
		}
	}

	public void RegisterSfxToggleListener(SfxToggleListener listener) 
	{
		sfxToggleListeners.Add (listener);
	}

	public void DeregisterSfxToggleListener(SfxToggleListener listener)
	{
		sfxToggleListeners.Remove (listener);
	}

	public void NotifySfxToggle(bool sfxOn)
	{
		foreach (SfxToggleListener listener in sfxToggleListeners) {
			listener.OnSfxToggle (sfxOn);
		}
	}

	public void RegisterDifficultyChangeListener(DifficultyChangeListener listener) 
	{
		difficultyChangeListeners.Add (listener);
	}

	public void DeregisterDifficultyChangeListener(DifficultyChangeListener listener)
	{
		difficultyChangeListeners.Remove (listener);
	}

	public void NotifyDifficultyChange(int difficulty)
	{
		foreach (DifficultyChangeListener listener in difficultyChangeListeners) {
			listener.OnDifficultyChanged (difficulty);
		}
	}

}

