using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

	public void Restart() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public void Home() {
		SceneManager.LoadScene ("MainMenu");
	}

	public void TogglePause() {
		if (Time.timeScale == 1f) {
			TogglePause (true);
		} else {
			TogglePause (false);
		}
	}

	public void TogglePause(bool pause) {
		if (pause) {
			Time.timeScale = 0f;
		} else {
			Time.timeScale = 1f;
		}
	}
}
