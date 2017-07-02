using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfigSelector : MonoBehaviour {

	[SerializeField]
	private List<PlayerConfig> configs;

	[SerializeField]
	private PlayerConfigSelector excludedSelection;

	private PlayerConfig selectedConfig;

	public PlayerConfig GetSelectedConfig() {
		if (selectedConfig == null) {
			if (excludedSelection != null) {
				configs.Remove (excludedSelection.GetSelectedConfig ());
			}
			SelectConfig ();
		}
		return selectedConfig;
	}

	private void SelectConfig() {
		selectedConfig = configs [Random.Range (0, configs.Count)];	
	}
}
