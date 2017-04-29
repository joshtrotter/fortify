using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	public int numFlips = 10;
	public float initialSpeed = 0.1f;
	public float speedIncrement = 0.05f;
	public float initialSize = 2f;
	public float sizeIncrement = 0.1f;

	[SerializeField]
	private NotificationPanel notificationPanel;

	private SpriteAnimator tileAnimator;

	void Start () 
	{
		tileAnimator = gameObject.GetComponent<SpriteAnimator> ();
	}
	
	public void Toss(Player player1, Player player2) 
	{
		tileAnimator.SetSprite (player1.PlayerSprite ());
		tileAnimator.AnimateExpand (Vector3.zero, Vector3.one, 0.2f, () => Flip(player2, player1));
	}

	private void Flip(Player to, Player from, int flipCount = 0) 
	{
		if (flipCount < numFlips) {
			FlipToPlayer (to, flipCount, () => FlipToHidden (flipCount + 1, () => Flip (from, to, flipCount + 2)));
		} else {
			FlipToPlayer (to, flipCount, () => RandomFinalFlip(to, from));
		}
	}

	private void RandomFinalFlip(Player currentWinner, Player alternative)
	{
		if (UnityEngine.Random.Range (0, 2) == 1) {
			FlipToHidden (numFlips + 1, () => FlipToPlayer (alternative, numFlips + 2, () => OnPlayerSelected (alternative)));
		} else {
			OnPlayerSelected (currentWinner);
		}
	}

	private void OnPlayerSelected(Player player)
	{		
		notificationPanel.Reveal (player.PlayerName () + " Goes First!", () => Finish (player), 0.25f, 0.15f, true);
	}

	private void Finish(Player player)
	{
		EventBus.INSTANCE.NotifyCoinFlip (player);
		tileAnimator.AnimateExpand(transform.localScale, Vector3.zero, 0.2f, () => gameObject.SetActive(false));
	}


	private void FlipToPlayer(Player player, int increment, Action onComplete)
	{
		GlobalContext.INSTANCE.getSoundController ().PlayCapture ();
		tileAnimator.SetSprite (player.PlayerSprite ());
		float startSize = initialSize + (sizeIncrement * increment);
		float endSize = startSize + sizeIncrement;
		float speed = initialSpeed + (speedIncrement * increment);			
		StartCoroutine(tileAnimator.ExpandingFlip (startSize, endSize, 87.5f, 0f, speed, onComplete));
	}

	private void FlipToHidden(int increment, Action onComplete)
	{		
		float startSize = initialSize + (sizeIncrement * increment);
		float endSize = startSize + sizeIncrement;
		float speed = initialSpeed + (speedIncrement * increment);
		StartCoroutine(tileAnimator.ExpandingFlip (startSize, endSize, 0f, 87.5f, speed, onComplete));
	}


}
