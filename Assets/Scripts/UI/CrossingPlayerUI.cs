using UnityEngine;
using UnityEngine.UI;

public class CrossingPlayerUI : MonoBehaviour, EndTurnListener, CoinFlipListener, BoardReadyListener {

	[SerializeField]
	private Color fadeOutColor;

	[SerializeField]
	private Text playerName;

	[SerializeField]
	private SpriteAnimator playerIcon;

    [SerializeField]
    private Text score;

	[SerializeField]
	private int islands = 2;

	private Player player;
	private HexBoard board;

	private bool fadedIn = true;

	public void InitialiseForPlayer(Player player) 
	{
		this.player = player;
		playerIcon.SetSprite (player.PlayerSprite ());
		playerName.text = player.PlayerName ();
		EventBus.INSTANCE.RegisterEndTurnListener (this);
		EventBus.INSTANCE.RegisterCoinFlipListener (this);
		EventBus.INSTANCE.RegisterBoardReadyListener (this);
	}

	public void OnBoardReady(HexBoard board)
	{
		this.board = board;
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
		int crossingScore = 0;
		for (int i = 1; i <= islands; i++) {
			int playerIslandScore = 0;
			int opponentIslandScore = 0;
			foreach (HexTile tile in board.Tiles()) {
				if (tile.tag == "island" + i) {
					if (tile.CurrentOwner () == player) {
						++playerIslandScore;
					} else if (tile.CurrentOwner () == player.Opponent ()) {
						++opponentIslandScore;
					}
				}
			}
			if (playerIslandScore > opponentIslandScore) {
				++crossingScore;
			}
		}

		this.score.text = crossingScore + "/" + islands;
    }
}
