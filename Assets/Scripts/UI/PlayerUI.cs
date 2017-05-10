using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour, EndTurnListener, CoinFlipListener {

	[SerializeField]
	private Color fadeOutColor;

	[SerializeField]
	private Text playerName;

	[SerializeField]
	private SpriteAnimator playerIcon;

    [SerializeField]
    private Text score;

	private Player player;

	private bool fadedIn = true;

	public void InitialiseForPlayer(Player player) 
	{
		this.player = player;
		playerIcon.SetSprite (player.PlayerSprite ());
		playerName.text = player.PlayerName ();
		EventBus.INSTANCE.RegisterEndTurnListener (this);
		EventBus.INSTANCE.RegisterCoinFlipListener (this);
	}

	public void OnStartingPlayerChosen (Player player)
	{
		if (this.player != player) {
			FadeOut ();
		}
	}

	public void OnEndTurn(Player player)
	{
		if (fadedIn) {
			FadeOut ();
		} else {
			FadeIn ();
		}
		UpdateScore ();
	}
		
	public void FadeIn()
	{
		fadedIn = true;
		playerName.color = Color.white;
		score.color = Color.white;
	}

	public void FadeOut() 
	{
		fadedIn = false;
		playerName.color = fadeOutColor;
		score.color = fadeOutColor;
	}

    public void UpdateScore()
    {
		this.score.text = "" + player.ClaimedTileCount();
    }
}
