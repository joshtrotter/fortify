using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class SettingsMenu : MonoBehaviour {

	private const string SFX_KEY = "sfx";
	private const string DIFFICULTY_KEY = "difficulty";

	[SerializeField]
	private GameObject display;

	[SerializeField]
	private Toggle sfx;

	[SerializeField]
	private Slider difficulty;

	void Start () {
		if (PlayerPrefs.HasKey (SFX_KEY)) {
			sfx.isOn = PlayerPrefs.GetInt (SFX_KEY) == 1;
		}
		if (PlayerPrefs.HasKey (DIFFICULTY_KEY)) {
			difficulty.value = PlayerPrefs.GetInt (DIFFICULTY_KEY);
		}

		sfx.onValueChanged.AddListener (OnSfxToggle);
		difficulty.onValueChanged.AddListener (OnDifficultyChange);
	}
	
	void OnSfxToggle(bool isOn) {
		PlayerPrefs.SetInt (SFX_KEY, isOn ? 1 : 0);
		PlayerPrefs.Save ();
		EventBus.INSTANCE.NotifySfxToggle (isOn);
	}

	void OnDifficultyChange(float difficulty) {
		PlayerPrefs.SetInt (DIFFICULTY_KEY, (int)difficulty);
		PlayerPrefs.Save ();
		EventBus.INSTANCE.NotifyDifficultyChange ((int)difficulty);
		FB.LogAppEvent ("DifficultyChanged", difficulty);
	}

	public void ToggleDisplay() {
		display.SetActive (!display.activeSelf);
	}

}
