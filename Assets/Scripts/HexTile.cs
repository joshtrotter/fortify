using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

public class HexTile : MonoBehaviour {

    private enum TileState { AVAILABLE, CLAIMED, FORTIFIED_MINOR, FORTIFIED_MAJOR };

    private SpriteRenderer rend;
    private Player owner;
    private TileState currentState;
    private HashSet<HexTile> neighbours = new HashSet<HexTile>();

    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
        currentState = TileState.AVAILABLE;
    }

    void OnMouseDown()
    {
		EventBus.INSTANCE.NotifyTileSelection (this);        
    }

	public void Claim(Player player) 
	{
		Claim (player, null);
	}

    public void Claim(Player player, Action onComplete)
    {
		if (owner == player.Opponent()) {
			StartCoroutine(FlipToSprite(player.PlayerSprite(), onComplete));
		} else {
			StartCoroutine(ExpandToSprite(player.PlayerSprite(), onComplete));
		}

        owner = player;
        currentState = TileState.CLAIMED;

    }

	public void ApplyMinorFortify() {
		ApplyMinorFortify (null);
	}

	public void ApplyMinorFortify(Action onComplete)
    {
        currentState = TileState.FORTIFIED_MINOR;
		StartCoroutine(ExpandToSprite(owner.FortifySprite(), onComplete));
    }

	public void RemoveFortify() {
		RemoveFortify (null);
	}
		    
	public void RemoveFortify(Action onComplete)
    {
        currentState = TileState.CLAIMED;
		StartCoroutine(ExpandToSprite(owner.PlayerSprite(), onComplete));
    }

    public bool Available()
    {
        return currentState == TileState.AVAILABLE;
    }

    public bool Claimed()
    {
        return currentState == TileState.CLAIMED;
    }

    public bool FortifiedMinor()
    {
        return currentState == TileState.FORTIFIED_MINOR;
    }

    public bool FortifiedMajor()
    {
        return currentState == TileState.FORTIFIED_MAJOR;
    }

    public Player CurrentOwner()
    {
        return owner;
    }

    public void AddNeighbour(HexTile neighbour)
    {
        neighbours.Add(neighbour);
    }

    public HashSet<HexTile> Neighbours()
    {
        return neighbours;
    }

	private IEnumerator ExpandToSprite(Sprite sprite, Action onComplete, float initialScale = 0.33f, float duration = 0.20f) {
		HexTile placeholder = Instantiate (this, transform.parent);
		rend.sortingLayerName = "Overlay";
		rend.sprite = sprite;

		Vector3 initialExpansionSize = Vector3.one * initialScale;
		transform.localScale = initialExpansionSize;

		yield return Expand (initialExpansionSize, duration);

		Destroy (placeholder.gameObject);
		rend.sortingLayerName = "Default";

		if (onComplete != null) {
			onComplete ();
		}
	}

	private IEnumerator FlipToSprite(Sprite sprite, Action onComplete, float duration = 0.20f) {		
		yield return Flip (1f, 0f, duration / 2f);
		rend.sprite = sprite;
		yield return Flip (0f, 1f, duration / 2f);

		if (onComplete != null) {
			onComplete ();
		}
	}

	private IEnumerator ExpandToColor(Color color, Action onComplete, float initialScale = 0.33f, float duration = 0.20f) {
		HexTile placeholder = Instantiate (this, transform.parent);
		rend.sortingLayerName = "Overlay";
		rend.color = color;

		Vector3 initialExpansionSize = Vector3.one * initialScale;
		transform.localScale = initialExpansionSize;

		yield return Expand (initialExpansionSize, duration);

		Destroy (placeholder.gameObject);
		rend.sortingLayerName = "Default";

		if (onComplete != null) {
			onComplete ();
		}
	}

	private IEnumerator FlipToColor(Color color, Action onComplete, float duration = 0.20f) {		
		yield return Flip (1f, 0f, duration / 2f);
		rend.color = color;
		yield return Flip (0f, 1f, duration / 2f);

		if (onComplete != null) {
			onComplete ();
		}
	}

	private IEnumerator Expand(Vector3 startSize, float duration) {
		float elapsed = 0f;
		do {			
			yield return new WaitForEndOfFrame ();
			elapsed += Time.deltaTime;
			transform.localScale = Vector3.Lerp (startSize, Vector3.one, elapsed / duration);
		} while (elapsed < duration);
	}

	private IEnumerator Flip(float startSize, float endSize, float duration) {
		float elapsed = 0f;
		while (elapsed < duration) {			
			elapsed += Time.deltaTime;
			transform.localScale = new Vector3(Mathf.Lerp (startSize, endSize, elapsed / duration), transform.localScale.y);
			yield return new WaitForEndOfFrame ();
		} 
	}

	public void AddHighlight() {
		HexTile highlight = Instantiate (this, transform.parent);
		highlight.transform.localScale = Vector3.one * 1.1f;
		highlight.GetComponent<SpriteRenderer> ().sortingLayerName = "Underlay";
		highlight.GetComponent<SpriteRenderer> ().color = Color.green;
	}

}
