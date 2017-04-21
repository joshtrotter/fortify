using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour, StartAnimationListener, EndAnimationListener {

	private bool waitingForAnimation = false;

	private bool restartQueued = false;

	private bool homeQueued = false;

	void Start() {
		EventBus.INSTANCE.RegisterStartAnimationListener (this);
		EventBus.INSTANCE.RegisterEndAnimationListener (this);
	}

	public void OnAnimationStart() {
		waitingForAnimation = true;
	}

	public void OnAnimationEnd(int runningAnimations) {
		if (runningAnimations == 0) {
			waitingForAnimation = false;
			if (restartQueued) {
				Restart ();
			}
			if (homeQueued) {
				Home ();
			}
		}
	}

	public void Restart() {
		if (!waitingForAnimation) {
			EventBus.INSTANCE.Reset ();
			GlobalContext.INSTANCE.Reset ();
			SceneManager.LoadScene ("Game");
		} else {
			restartQueued = true;
		}
	}

	public void Home() {
		if (!waitingForAnimation) {
			EventBus.INSTANCE.Reset ();
			GlobalContext.INSTANCE.Reset ();
			SceneManager.LoadScene ("MainMenu");
		} else {
			homeQueued = true;
		}
	}
}
