﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Facebook.Unity;

public class TutorialController : MonoBehaviour, EndTurnListener, BoardReadyListener {

	[SerializeField]
	private Player player1;

	[SerializeField]
	private Player player2;

	[SerializeField]
	private PlayerUI player1UI;

	[SerializeField]
	private PlayerUI player2UI;

	[SerializeField]
	private TutorialPanel tutorialPanel;

	[SerializeField]
	private NotificationPanel gamePanel;

	[SerializeField]
	private TileHighlight step1Highlight;

	[SerializeField]
	private TileHighlight step3Highlight;

	[SerializeField]
	private TileHighlight step5Highlight;

	[SerializeField]
	private TileHighlight step7Highlight;

	private HexBoard board;

	private int tutorialState = 0;

	void Start () {
		EventBus.INSTANCE.RegisterEndTurnListener (this);
		EventBus.INSTANCE.RegisterBoardReadyListener (this);

		Initialise ();
	}

	public void Initialise() {	
		player1UI.InitialiseForPlayer (player1);
		player2UI.InitialiseForPlayer (player2);
	}

	public void OnBoardReady(HexBoard board)
	{
		this.board = board;       
		StartTutorial ();
	}

	private void StartTutorial() 
	{
		LogTutorialEvent ("Started");
		tutorialPanel.DisplayStep1 ();
	}
		
	public void OkClicked() 
	{
		if (tutorialState == 0) {
			tutorialState = 1;
			tutorialPanel.HideButton (() => Step1 ());
		} else if (tutorialState == 1) {
			tutorialState = 2;
			tutorialPanel.HideButton (() => Step2 ());
		} else if (tutorialState == 3) {
			tutorialPanel.HideButton (() => Step3 ());
		} else if (tutorialState == 5) {
			tutorialPanel.HideButton (() => Step5 ());
		} else if (tutorialState == 6) {
			tutorialPanel.HideButton (() => Step6 ());
		} else if (tutorialState == 9) {
			tutorialPanel.HideButton (() => Step9 ());
		} else if (tutorialState == 10) {
			SceneManager.LoadScene ("MainMenu");
		}
	}


	private void Step1()
	{
		gamePanel.Reveal ("Click to claim the tile!", () => Step1WaitForInput(), 0f, 0.15f, true);
		foreach (HexTile tile in board.Tiles()) {
			if (tile.name != "Step1") {				
				tile.Deactivate (() => {});
			}
		}
	}

	private void Step1WaitForInput() {
		step1Highlight.gameObject.SetActive(true);
		EventBus.INSTANCE.NotifyCoinFlip (player1);
		player1.StartTurn ();
	}

	private void Step1Complete() {
		foreach (HexTile tile in board.Tiles()) {
			tile.Activate (() => {});
		}
		step1Highlight.gameObject.SetActive(false);
		gamePanel.Hide (() => {});
		tutorialPanel.DisplayStep2 ();
		LogTutorialEvent ("Step1Complete");
	}

	private void Step2() {
		foreach (HexTile tile in board.Tiles()) {
			if (tile.name == "Step2") {
				tile.Activate (() => player2.StartTurn ());
			}
			else if (tile.name != "Step1") {				
				tile.Deactivate (() => {});
			}
		}
	}

	private void Step2Complete() {
		foreach (HexTile tile in board.Tiles()) {
			tile.Activate (() => {});
		}
		tutorialState = 3;
		tutorialPanel.DisplayStep3 ();
		LogTutorialEvent ("Step2Complete");
	}

	private void Step3()
	{
		gamePanel.Reveal ("Click to capture tiles!", () => Step3WaitForInput(), 0f, 0.15f, true);
		foreach (HexTile tile in board.Tiles()) {
			if (tile.name != "Step1" && tile.name != "Step2" && tile.name != "Step3") {				
				tile.Deactivate (() => {});
			}
		}
	}

	private void Step3WaitForInput() {
		step3Highlight.gameObject.SetActive(true);
		player1.StartTurn ();
	}

	private void Step3Complete() {
		tutorialState = 4;
		foreach (HexTile tile in board.Tiles()) {
			tile.Activate (() => {});
		}
		step3Highlight.gameObject.SetActive(false);
		gamePanel.Hide (() => StartCoroutine(Step4Delay(() => Step4())));
		LogTutorialEvent ("Step3Complete");
	}

	private IEnumerator Step4Delay(Action onComplete) {
		yield return new WaitForSeconds (1.5f);
		onComplete ();
	}

	private void Step4() {
		foreach (HexTile tile in board.Tiles()) {
			if (tile.name == "Step4") {
				tile.Activate (() => player2.StartTurn ());
			}
			else if (tile.name != "Step1" && tile.name != "Step2" && tile.name != "Step3") {				
				tile.Deactivate (() => {});
			}
		}
	}

	private void Step4Complete() {
		foreach (HexTile tile in board.Tiles()) {
			tile.Activate (() => {});
		}
		tutorialState = 5;
		tutorialPanel.DisplayStep5 ();
		LogTutorialEvent ("Step4Complete");
	}

	private void Step5() {
		gamePanel.Reveal ("Click to fortify a tile!", () => Step5WaitForInput(), 0f, 0.15f, true);
		foreach (HexTile tile in board.Tiles()) {
			if (tile.name != "Step1" && tile.name != "Step2" && tile.name != "Step3" && tile.name != "Step4" && tile.name != "Step5") {				
				tile.Deactivate (() => {});
			}
		}
	}

	private void Step5WaitForInput() {
		step5Highlight.gameObject.SetActive(true);
		player1.StartTurn ();
	}

	private void Step5Complete() {
		foreach (HexTile tile in board.Tiles()) {
			tile.Activate (() => {});
		}
		step5Highlight.gameObject.SetActive(false);
		gamePanel.Hide (() => {});
		tutorialState = 6;
		tutorialPanel.DisplayStep6 ();
		LogTutorialEvent ("Step5Complete");
	}

	private void Step6() {
		foreach (HexTile tile in board.Tiles()) {
			if (tile.name == "Step6") {
				tile.Activate (() => player2.StartTurn ());
			}
			else if (tile.name != "Step1" && tile.name != "Step2" && tile.name != "Step3" && tile.name != "Step4" && tile.name != "Step5") {				
				tile.Deactivate (() => {});
			}
		}
	}

	private void Step6Complete() {
		foreach (HexTile tile in board.Tiles()) {
			tile.Activate (() => {});
		}
		tutorialState = 7;
		StartCoroutine(Step7Delay(() => Step7()));
		LogTutorialEvent ("Step6Complete");
	}

	private IEnumerator Step7Delay(Action onComplete) {
		yield return new WaitForSeconds (1.5f);
		onComplete ();
	}

	private void Step7() {
		gamePanel.Reveal ("Click to re-fortify the tile!", () => Step7WaitForInput(), 0f, 0.15f, true);
		foreach (HexTile tile in board.Tiles()) {
			if (tile.name != "Step1" && tile.name != "Step2" && tile.name != "Step3" && tile.name != "Step4" && tile.name != "Step5" && tile.name != "Step6" && tile.name != "Step7") {				
				tile.Deactivate (() => {});
			}
		}
	}

	private void Step7WaitForInput() {
		step7Highlight.gameObject.SetActive(true);
		player1.StartTurn ();
	}

	private void Step7Complete() {
		tutorialState = 8;
		foreach (HexTile tile in board.Tiles()) {
			tile.Activate (() => {});
		}
		step7Highlight.gameObject.SetActive(false);
		gamePanel.Hide (() => StartCoroutine(Step8Delay(() => Step8())));
		LogTutorialEvent ("Step7Complete");
	}

	private IEnumerator Step8Delay(Action onComplete) {
		yield return new WaitForSeconds (1.5f);
		onComplete ();
	}

	private void Step8() {
		foreach (HexTile tile in board.Tiles()) {
			if (tile.name == "Step8") {
				tile.Activate (() => player2.StartTurn ());
			}
			else if (tile.name != "Step1" && tile.name != "Step2" && tile.name != "Step3" && tile.name != "Step4" && tile.name != "Step5" && tile.name != "Step6" && tile.name != "Step7") {				
				tile.Deactivate (() => {});
			}
		}
	}

	private void Step8Complete() {
		foreach (HexTile tile in board.Tiles()) {
			tile.Activate (() => {});
		}
		tutorialState = 9;
		tutorialPanel.DisplayStep9 ();
		LogTutorialEvent ("Step8Complete");
	}

	private void Step9() {
		gamePanel.Reveal ("Click to sacrifice the tile!", () => Step9WaitForInput(), 0f, 0.15f, true);
		foreach (HexTile tile in board.Tiles()) {
			if (tile.name != "Step1" && tile.name != "Step2" && tile.name != "Step3" && tile.name != "Step4" && tile.name != "Step5" && tile.name != "Step6" && tile.name != "Step7" && tile.name != "Step8") {				
				tile.Deactivate (() => {});
			}
		}
	}

	private void Step9WaitForInput() {
		step1Highlight.gameObject.SetActive(true);
		player1.StartTurn ();
	}

	private void Step9Complete() {
		tutorialState = 10;
		foreach (HexTile tile in board.Tiles()) {
			tile.Activate (() => {});
		}
		step1Highlight.gameObject.SetActive(false);
		gamePanel.Hide (() => Step10());
		LogTutorialEvent ("Finished");
	}

	private void Step10() {
		tutorialPanel.DisplayStep10 (() => Step10PlayOn());
	}

	private void Step10PlayOn() {
		player2.StartTurn ();
	}
		
	public void OnEndTurn(Player player)
	{
		if (tutorialState == 1) {
			Step1Complete ();
		} else if (tutorialState == 2) {
			Step2Complete ();
		} else if (tutorialState == 3) {
			Step3Complete ();
		}
		else if (tutorialState == 4) {
			Step4Complete ();
		}
		else if (tutorialState == 5) {
			Step5Complete ();
		}
		else if (tutorialState == 6) {
			Step6Complete ();
		}
		else if (tutorialState == 7) {
			Step7Complete ();
		}
		else if (tutorialState == 8) {
			Step8Complete ();
		}
		else if (tutorialState == 9) {
			Step9Complete ();
		}
		else if (CheckForEndOfGame())
		{
			DisplayEndOfGame();
		} 
		else
		{			
			player.Opponent().StartTurn();
		}
	}	

	private bool CheckForEndOfGame()
	{
		return (player1.ClaimedTileScore() + player2.ClaimedTileScore() == board.Tiles().Count);
	}

	private void DisplayEndOfGame()
	{
		if (player1.ClaimedTileScore() > player2.ClaimedTileScore())
		{
			gamePanel.Reveal (player1.PlayerName () + " WINS!", () => {}, 0f, 0.15f, true);
		} else
		{
			gamePanel.Reveal (player2.PlayerName () + " WINS!", () => {}, 0f, 0.15f, true);
		}
	}

	private void LogTutorialEvent(string stepName)
	{
		Dictionary<string, object> eventParams = new Dictionary<string, object> ();
		eventParams ["Step"] = stepName;
		FB.LogAppEvent ("TutorialStep", parameters: eventParams);				
	}
}
