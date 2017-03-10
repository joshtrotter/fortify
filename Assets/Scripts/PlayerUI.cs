using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    [SerializeField]
    private Image turnIndicator;

    [SerializeField]
    private Text score;

	public void ShowTurnIndicator(bool show)
    {
        turnIndicator.gameObject.SetActive(show);
    }

    public void DisplayScore(int score)
    {
        this.score.text = "" + score;
    }
}
