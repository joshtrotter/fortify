using System;
using UnityEngine;

public class PlayerConfig : MonoBehaviour
{
	[SerializeField]
	private Sprite playerSprite;

	[SerializeField]
	private Sprite fortifySprite; 

	public PlayerConfig (Sprite playerSprite, Sprite fortifySprite)
	{
		this.playerSprite = playerSprite;
		this.fortifySprite = fortifySprite;
	}

	public Sprite PlayerSprite()
	{
		return playerSprite;
	}
		
	public Sprite FortifySprite()
	{
		return fortifySprite;
	}		
}


