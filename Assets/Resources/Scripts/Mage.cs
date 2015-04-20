using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mage  {
	public string Name;
	private MageClass MageClass;

	public Spell LeftHand, RightHand;
	public float _ActualHealth;
	public float _ActualMana;
	public Shield Shield;
    public List<Vial> LeftVialList;
    public List<Vial> RightVialList;
    public List<Vial> ActiveBonuses;// = new List<Vial>();



	public float ActualMana {
		get { return _ActualMana; }
		set { _ActualMana = value; }
	}
	public float ActualHealth {
		get { return _ActualHealth; }
		set { _ActualHealth = value; }
	}

	public int MaxHealth {
		get { return MageClass.StartingLife; }
	}

	public int MaxMana {
		get { return MageClass.StartingMana; }
	}

	public Mage(string name, MageClass mageClass) {
		Name = name;
		MageClass = mageClass;
		_ActualMana = mageClass.StartingMana;
		_ActualHealth = mageClass.StartingLife;
	}

	internal bool CanAfford(Spell Spell) {
		return Spell.Cost <= ActualMana;
	}

    internal bool CanAfford(Shield Shield)
    {
        return Shield.SetupCost <= ActualMana;
    }

    internal bool CanSustain(Shield Shield)
    {
        return Shield.SustainCost <= ActualMana;
    }


	internal void Update(float deltaTime) {

		RegenerateMana(deltaTime);
		RegenerateHealth(deltaTime);
	}

	private void RegenerateHealth(float deltaTime) {
		if (ActualHealth < MaxHealth) {
			ActualHealth += deltaTime * MageClass.LifeRegen ;
		} 
	}

	private void RegenerateMana(float deltaTime) {

		if (ActualMana < MaxMana) {
			ActualMana += deltaTime * MageClass.ManaRegen;
		} 
	}

	public bool IsDead() {
		return ActualHealth <= 0;
	}


    internal void UseVial(Vial vial) {
        if (vial.VialParams.ContainsKey(VialParam.LifeRegen)) {
            ActualHealth += vial.VialParams[VialParam.LifeRegen];
        }

        if (vial.VialParams.ContainsKey(VialParam.ManaRegen)) {
            ActualMana += vial.VialParams[VialParam.ManaRegen];
        }
      //  ActiveBonuses.Add(vial);
    }

    public void AddVial(Vial vial) {
        RightVialList.Add(vial);
    }
}


public class MageClass {
	public static readonly MageClass Creator = new MageClass(100, 100, 3, 1);
	public static readonly MageClass Thenacurviat = new MageClass(100, 100, 3, 2);
	
	public readonly int StartingLife;
	public readonly int StartingMana;
	public readonly int LifeRegen;
	public readonly float ManaRegen;

	public MageClass(int life, int mana, int lifeRegen, float manaRegen) {
		StartingLife = life;
		StartingMana = mana;
		LifeRegen = lifeRegen;
		ManaRegen = manaRegen;
	}
	
}
