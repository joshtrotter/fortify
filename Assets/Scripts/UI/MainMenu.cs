using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void SinglePlayer() {		
		SceneManager.LoadScene ("SinglePlayer");
	}

	public void MultiPlayer() {		
		SceneManager.LoadScene ("MultiPlayer");
	}

	public void Tutorial() {	
		SceneManager.LoadScene ("Tutorial");
	}

	public void Challenges() {	
		SceneManager.LoadScene ("ChallengeMenu");
	}

}
