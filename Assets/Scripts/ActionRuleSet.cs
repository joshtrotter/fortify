using System;
using UnityEngine;

public abstract class ActionRuleSet : MonoBehaviour {

    public abstract void PlayTile(Player player, HexTile tile, Action onComplete); 

}
