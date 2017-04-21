using System;
using UnityEngine;

public class PlayerConfig : MonoBehaviour
{
	[SerializeField]
	private Sprite playerSprite;

	[SerializeField]
	private Sprite fortifySprite;

	[SerializeField]
	private String playerName;

	public PlayerConfig (Sprite playerSprite, Sprite fortifySprite, String playerName)
	{
		this.playerSprite = playerSprite;
		this.fortifySprite = fortifySprite;
		this.playerName = playerName;
	}

	public void SetPlayerName(String playerName)
	{
		this.playerName = playerName;
	}

	public Sprite PlayerSprite()
	{
		return playerSprite;
	}
		
	public Sprite FortifySprite()
	{
		return fortifySprite;
	}

	public String PlayerName()
	{
		return playerName;
	}
}


