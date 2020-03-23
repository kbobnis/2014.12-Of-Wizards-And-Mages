using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SpellCard  {

	public string Name;
	public int Cost;
	public Dictionary<AnimationType, Sprite> Animations;
	public Dictionary<EffectType, int> Effects;

	public SpellCard(string name, int cost, Dictionary<AnimationType, Sprite> animations, Dictionary<EffectType, int> effects) {
		Name = name;
		Cost = cost;
		Animations = animations;
		Effects = effects;
	}
}

public enum EffectType {
	Speed, Damage, Health

}
