using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spell  {
	public string Name;
	public int Cost;
	public Dictionary<FlyingParam, int> FlyingParams;
	private Dictionary<AfterHitParam, int> AfterHitParams;

	public Spell(string name, int cost, Dictionary<FlyingParam, int> flyingParams, Dictionary<AfterHitParam, int> afterHitParams) {
		Name = name;
		Cost = cost;
		FlyingParams = flyingParams;
		AfterHitParams = afterHitParams;
	}

}
