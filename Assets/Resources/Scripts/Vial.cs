using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vial {
    
	public string Name;
    public Dictionary<VialParam, int> VialParams;// = new Dictionary<VialParam,int>();
	public Sprite Sprite;

    public Vial(string name, Dictionary<VialParam, int> vialParam, Sprite sprite) {
		Name = name;
        VialParams = vialParam;
		Sprite = sprite;
	}
}

public enum VialParam {
    ManaAdd, HealthAdd, DmgBonus, Time
}
