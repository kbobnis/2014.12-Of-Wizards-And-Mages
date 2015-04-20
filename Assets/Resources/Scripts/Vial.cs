using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vial {
    string Name;
    public Dictionary<VialParam, int> VialParams;// = new Dictionary<VialParam,int>();

    public Vial(string name, Dictionary<VialParam, int> vialParam) {
		Name = name;
        VialParams = vialParam ;
	}
}

public enum VialParam {
    ManaRegen, LifeRegen, DmgBonus, Time
}
