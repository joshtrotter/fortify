using UnityEngine;
using UnityEngine.SceneManagement;

public class ChallengeMenu : MonoBehaviour {

	public void LoadChallenge(string challengeScene) {
		Debug.Log ("Load Challenge");
		SceneManager.LoadScene (challengeScene);
	}
}
