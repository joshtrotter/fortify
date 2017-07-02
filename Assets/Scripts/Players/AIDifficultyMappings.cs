using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDifficultyMappings : MonoBehaviour {

	[SerializeField]
	private List<AIStrategy> strategies;

	[SerializeField]
	private AIStrategy defaultStrategy;

	public AIStrategy StrategyForDifficulty(int difficulty) {
		if (strategies == null || strategies.Count == 0) {
			return defaultStrategy;
		} else {
			return strategies [difficulty];
		}
	}
}
