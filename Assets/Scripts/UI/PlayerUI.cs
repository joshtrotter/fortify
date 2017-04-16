using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour, EndTurnListener, CoinFlipListener {

    [SerializeField]
    private Image turnIndicator;

	[SerializeField]
	private Image playerIcon;

    [SerializeField]
    private Text score;

	private Player player;

	public void InitialiseForPlayer(Player player) 
	{
		this.player = player;
		playerIcon.sprite = player.PlayerSprite ();
		EventBus.INSTANCE.RegisterEndTurnListener (this);
		EventBus.INSTANCE.RegisterCoinFlipListener (this);
	}

	public void OnStartingPlayerChosen (Player player)
	{
		turnIndicator.gameObject.SetActive (this.player == player);
	}

	public void OnEndTurn(Player player)
	{
		SwapTurnIndicator ();
		UpdateScore ();
	}

	private void SwapTurnIndicator()
    {
		turnIndicator.gameObject.SetActive(!turnIndicator.gameObject.activeSelf);
    }

    private void UpdateScore()
    {
		this.score.text = "" + player.ClaimedTileCount();
    }
}
